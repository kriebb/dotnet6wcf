using System.ServiceModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace dotnet6.wcf.poc.Soap;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSoapClient(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddOptions<GeneralBasicHttpBindingSettings>()
            .Bind(configuration.GetSection("GeneralBasicHttpBinding")).PostConfigure(options =>
                {
                    if (options.HttpBinding == null)
                    {
                        var HttpBinding = new BasicHttpBinding
                        {
                            MaxReceivedMessageSize = 2147483647,
                            MaxBufferSize = 2147483647,
                            MaxBufferPoolSize = 2147483647
                        };
                        HttpBinding.ReaderQuotas.MaxBytesPerRead = 2147483647;
                        HttpBinding.ReaderQuotas.MaxStringContentLength = 2147483647;
                        HttpBinding.ReaderQuotas.MaxArrayLength = 2147483647;
                        HttpBinding.ReaderQuotas.MaxNameTableCharCount = 2147483647;

                        //For Https
                        HttpBinding.Security.Mode = BasicHttpSecurityMode.Transport;
                        HttpBinding.TransferMode = TransferMode.Buffered;
                        HttpBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.None;
                        //End For Https

                        //Extend the timeout
                        var span = new TimeSpan(0, 0, 2, 0, 0);
                        HttpBinding.CloseTimeout = span;
                        HttpBinding.OpenTimeout = span;
                        HttpBinding.ReceiveTimeout = span;
                        HttpBinding.SendTimeout = span;
                        HttpBinding.UseDefaultWebProxy = true;

                        options.HttpBinding = HttpBinding;
                    }
                }
            );
        serviceCollection.AddSingleton<ISoapHeaderApplier, SoapActionHeaderApplier>();
        serviceCollection.AddSingleton<ISoapHeaderApplier, ContentTypeXmlCharSetUtfHeaderApplier>();

        serviceCollection.AddSingleton<ISoapServiceClient, SoapServiceClient>();
        return serviceCollection;
    }
}