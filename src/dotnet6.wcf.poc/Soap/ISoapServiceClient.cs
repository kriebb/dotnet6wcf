namespace dotnet6.wcf.poc.Soap;

public interface ISoapServiceClient
{
    Task<TResult?> ExecuteAsync<TService, Tinput, TResult>(Func<TService, Tinput, Task<TResult?>> f, Tinput inputParam,
        string url);
}