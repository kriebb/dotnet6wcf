using System;
using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Channels;
using be.axa.customer.reglogext.infrastructure.Security;

namespace be.axa.customer.reglogext.infrastructure.Soap;

internal class DevelopmentAxaContextContextSoapHeaderProvider : ISoapHeaderApplier
{
    private readonly IAxaContextTokenProvider _axaContextTokenProvider;

    public DevelopmentAxaContextContextSoapHeaderProvider(IAxaContextTokenProvider axaContextTokenProvider)
    {
        _axaContextTokenProvider = axaContextTokenProvider ??
                                    throw new ArgumentNullException(nameof(axaContextTokenProvider));
    }

    public KeyValuePair<string, string> Apply(WebHeaderCollection headers)
    {
        var jwtToken = _axaContextTokenProvider.GenerateJWTToken();
        if (jwtToken == null)
            throw new InvalidOperationException(
                "No Axa Context token has been generated. Null came back from the GenerateJwtToken method");
        var pair = new KeyValuePair<string, string>("X-Axa-Context",jwtToken);

        headers.Add("X-Axa-Context", jwtToken);
        return pair;
    }


}