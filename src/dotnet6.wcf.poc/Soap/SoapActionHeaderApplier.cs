using System.Net;

namespace dotnet6.wcf.poc.Soap;

internal class SoapActionHeaderApplier : ISoapHeaderApplier
{
    public KeyValuePair<string, string> Apply(WebHeaderCollection headers)
    {
        var pair = new KeyValuePair<string, string>(Consts.HttpRequestHeader.SOAPAction, string.Empty);

        headers.Add(Consts.HttpRequestHeader.SOAPAction, string.Empty);
        return pair;
    }


}

