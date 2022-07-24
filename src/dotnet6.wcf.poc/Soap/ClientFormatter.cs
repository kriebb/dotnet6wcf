using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace dotnet6.wcf.poc.Soap;

internal class ClientFormatter : IClientMessageFormatter
{
    private readonly IClientMessageFormatter _argFormatter;

    public ClientFormatter(IClientMessageFormatter argFormatter)
    {
        _argFormatter = argFormatter;
    }

    public object DeserializeReply(Message message, object[] parameters)
    {
        return _argFormatter.DeserializeReply(message, parameters);
    }

    public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
    {
        var message = _argFormatter.SerializeRequest(messageVersion, parameters);
        return message;
    }
}