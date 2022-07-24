
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace dotnet6.wcf.poc.Soap;

internal class SoapServiceClient : ISoapServiceClient
{
    private readonly IOptions<GeneralBasicHttpBindingSettings> _generalBasicHttpOptions;
    private readonly ILogger<SoapServiceClient> _logger;
    private readonly IEnumerable<ISoapHeaderApplier> _soapHeaderAppliers;

    public SoapServiceClient(
        IEnumerable<ISoapHeaderApplier> soapHeaderAppliers,
        IOptions<GeneralBasicHttpBindingSettings> generalBasicHttpOptions,
        ILogger<SoapServiceClient> logger)
    {
        _soapHeaderAppliers = soapHeaderAppliers ?? throw new ArgumentNullException(nameof(soapHeaderAppliers));
        _generalBasicHttpOptions =
            generalBasicHttpOptions ?? throw new ArgumentNullException(nameof(generalBasicHttpOptions));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<TResult?> ExecuteAsync<TService, Tinput, TResult>(Func<TService, Tinput, Task<TResult?>> f,
        Tinput inputParam, string url)
    {
        var serviceChannel = CreateServiceChannel<TService>(url);
        var fromResult = Task.FromResult(default(TResult));
        List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
        try
        {
            using (var scope = new OperationContextScope(serviceChannel as IContextChannel))
            {
                var requestProp = new HttpRequestMessageProperty();

                foreach (var headerApplier in _soapHeaderAppliers)
                    keyValuePairs.Add(headerApplier.Apply(requestProp.Headers));


                OperationContext.Current.OutgoingMessageProperties[HttpRequestMessageProperty.Name] = requestProp;

                fromResult = f.Invoke(serviceChannel, inputParam);
            }
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Something went wrong while trying to call a a soap action");
            throw new SoapServiceClientException(ex, keyValuePairs);
        }


        return fromResult;
    }

    private TService CreateServiceChannel<TService>(string url)
    {
        var address = new EndpointAddress(url);
        var factory = new ChannelFactory<TService>(_generalBasicHttpOptions.Value.HttpBinding, address);


        factory.Endpoint.EndpointBehaviors.Add(new HttpMessageHandlerBehavior());
        //factory.Endpoint.Contract.ContractBehaviors.Add(new UseNamespacesPrefixes());

        return factory.CreateChannel();
    }
}