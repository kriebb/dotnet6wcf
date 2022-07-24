using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace dotnet6.wcf.poc.Soap;

internal class UseNamespacesPrefixes : IContractBehavior
{
    public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint,
        BindingParameterCollection bindingParameters)
    {
    }

    public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint,
        ClientRuntime clientRuntime)
    {
        clientRuntime.ClientOperations.Select(z => z.Formatter = new ClientFormatter(z.Formatter));
        clientRuntime.ClientMessageInspectors.Add(new ClientMessageInspector());
    }

    public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint,
        DispatchRuntime dispatchRuntime)
    {
        dispatchRuntime.MessageInspectors.Add(new MessageInspector());
    }

    public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
    {
    }
}