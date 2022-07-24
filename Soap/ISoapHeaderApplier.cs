using System.Collections.Generic;
using System.Net;
using System.ServiceModel.Channels;

namespace be.axa.customer.reglogext.infrastructure.Soap;

public interface ISoapHeaderApplier
{
    KeyValuePair<string,string> Apply(WebHeaderCollection headers);

}