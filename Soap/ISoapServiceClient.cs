using System;
using System.Threading.Tasks;

namespace be.axa.customer.reglogext.infrastructure.Soap;

public interface ISoapServiceClient
{
    Task<TResult?> ExecuteAsync<TService, Tinput, TResult>(Func<TService, Tinput, Task<TResult?>> f, Tinput inputParam,
        string url);
}