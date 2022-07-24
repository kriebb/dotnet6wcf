namespace be.axa.customer.reglogext.infrastructure.Soap;

public class InterceptingHttpMessageHandler : DelegatingHandler
{
    private readonly HttpMessageHandlerBehavior _parent;

    public InterceptingHttpMessageHandler(HttpMessageHandler innerHandler, HttpMessageHandlerBehavior parent)
    {
        InnerHandler = innerHandler;
        _parent = parent;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        HttpResponseMessage response;
        if (_parent.OnSendingAsync != null)
        {
            response = await _parent.OnSendingAsync(request, cancellationToken);
            if (response != null)
                return response;
        }

        response = await base.SendAsync(request, cancellationToken);

        if (_parent.OnSentAsync != null)
        {
            return await _parent.OnSentAsync(response, cancellationToken);
        }

        return response;
    }
}