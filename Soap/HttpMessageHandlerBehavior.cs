using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace be.axa.customer.reglogext.infrastructure.Soap;

public class HttpMessageHandlerBehavior : IEndpointBehavior
{
    public HttpMessageHandlerBehavior()
    {
    }

    public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
    {
        bindingParameters.Add(new Func<HttpClientHandler, HttpMessageHandler>(GetHttpMessageHandler));
    }

    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
    {
    }

    public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
    {
    }

    public void Validate(ServiceEndpoint endpoint)
    {
    }

    public HttpMessageHandler GetHttpMessageHandler(HttpClientHandler httpClientHandler)
    {
        return new InterceptingHttpMessageHandler(httpClientHandler, this);
    }

    public Func<HttpRequestMessage, CancellationToken, Task<HttpResponseMessage>> OnSendingAsync { get; set; }
    public Func<HttpResponseMessage, CancellationToken, Task<HttpResponseMessage>> OnSentAsync { get; set; }
}

