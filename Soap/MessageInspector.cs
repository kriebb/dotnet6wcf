using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace be.axa.customer.reglogext.infrastructure.Soap;

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