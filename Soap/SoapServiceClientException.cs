using System;
using System.Collections.Generic;
using System.Linq;

namespace be.axa.customer.reglogext.infrastructure.Soap;

internal class SoapServiceClientException : Exception
{
    public SoapServiceClientException(Exception exception, IEnumerable<KeyValuePair<string, string>> headers) : base(
        headers.Select(w => w.Key + ":" + w.Value + System.Environment.NewLine).ToString(), exception)
    {
    }
}