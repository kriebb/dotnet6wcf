using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Channels;
using HttpRequestHeader = be.axa.customer.reglogext.infrastructure.Http.HttpRequestHeader;

namespace be.axa.customer.reglogext.infrastructure.Soap;

internal class SoapActionHeaderApplier : ISoapHeaderApplier
{
    public KeyValuePair<string, string> Apply(WebHeaderCollection headers)
    {
        var pair = new KeyValuePair<string, string>(HttpRequestHeader.SOAPAction.ToString(), string.Empty);

        headers.Add(HttpRequestHeader.SOAPAction.ToString(), string.Empty);
        return pair;
    }


}