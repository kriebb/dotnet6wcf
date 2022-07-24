using System.Net;
using HttpRequestHeader = System.Net.HttpRequestHeader;

namespace dotnet6.wcf.poc.Soap;

internal class ContentTypeXmlCharSetUtfHeaderApplier : ISoapHeaderApplier
{
    public KeyValuePair<string,string> Apply(WebHeaderCollection headers)
    {
        var pair = new KeyValuePair<string, string>(HttpRequestHeader.ContentType.ToString(), Consts.ContentTypes.XML_UTF8);
        headers.Add(HttpRequestHeader.ContentType, Consts.ContentTypes.XML_UTF8);
        return pair;
    }
}

internal class Consts
{
    internal class ContentTypes
    {
        public const string XML_UTF8 = "text/xml; charset=utf-8";
    }

    internal class HttpRequestHeader
    {
        public const string SOAPAction = "SOAPAction";
       
    }
}