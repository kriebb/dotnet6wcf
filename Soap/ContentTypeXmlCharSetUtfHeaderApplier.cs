using System.Net;
using be.axa.customer.reglogext.infrastructure.Http;
using HttpRequestHeader = System.Net.HttpRequestHeader;

namespace be.axa.customer.reglogext.infrastructure.Soap;

internal class ContentTypeXmlCharSetUtfHeaderApplier : ISoapHeaderApplier
{
    public KeyValuePair<string,string> Apply(WebHeaderCollection headers)
    {
        var pair = new KeyValuePair<string, string>(HttpRequestHeader.ContentType.ToString(), Consts.ContentTypes.XML_UTF8);
        headers.Add(HttpRequestHeader.ContentType, Consts.ContentTypes.XML_UTF8);
        return pair;
    }
}