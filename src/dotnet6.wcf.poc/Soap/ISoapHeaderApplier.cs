using System.Net;

namespace dotnet6.wcf.poc.Soap;

public interface ISoapHeaderApplier
{
    KeyValuePair<string,string> Apply(WebHeaderCollection headers);

}