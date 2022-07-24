using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace dotnet6.wcf.poc.Soap;

internal class ClientMessageInspector : IClientMessageInspector
{
    public void AfterReceiveReply(ref Message reply, object correlationState)
    {
    }

    public object BeforeSendRequest(ref Message request, IClientChannel channel)
    {
        request = request.CreateBufferedCopy(int.MaxValue).TransformMessage2(request);

        return null;
    }
}