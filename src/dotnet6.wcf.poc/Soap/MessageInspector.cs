using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace dotnet6.wcf.poc.Soap;

internal class MessageInspector : IDispatchMessageInspector
{
    public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
    {
        return null;
    }

    public void BeforeSendReply(ref Message reply, object correlationState)
    {
    }
}