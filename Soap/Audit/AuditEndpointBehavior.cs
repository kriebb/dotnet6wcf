// Decompiled with JetBrains decompiler
// Type: Audit.Wcf.Client.AuditEndpointBehavior
// Assembly: Audit.Wcf.Client, Version=19.1.4.0, Culture=neutral, PublicKeyToken=571d6b80b242c87e
// MVID: 81125518-9F85-4C2B-953C-51CABA8D821A
// Assembly location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.dll
// XML documentation location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.xml

using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace be.axa.customer.reglogext.infrastructure.Soap.Audit
{
  public class AuditEndpointBehavior : IEndpointBehavior
  {
    /// <summary>
    /// The event type format string, default is "{action}"
    /// Can contain the following placeholder:
    /// - {action}: Replaced by the request action URL
    /// </summary>
    public string EventType { get; set; }

    /// <summary>
    /// Value indicating whether to include the request headers, default is false
    /// </summary>
    public bool IncludeRequestHeaders { get; set; }

    /// <summary>
    /// Value indicating whether to include the response headers, default is false
    /// </summary>
    public bool IncludeResponseHeaders { get; set; }

    public AuditEndpointBehavior():this("",false,false)
    {
    }

    // ReSharper disable once MemberCanBePrivate.Global
    public AuditEndpointBehavior(
      string eventType,
      bool includeRequestHeaders,
      bool includeResponseHeaders)
    {
      this.IncludeRequestHeaders = includeRequestHeaders;
      this.IncludeResponseHeaders = includeResponseHeaders;
      this.EventType = eventType;
    }

    public void AddBindingParameters(
      ServiceEndpoint endpoint,
      BindingParameterCollection bindingParameters)
    {
    }

    public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime) => clientRuntime.ClientMessageInspectors.Add((IClientMessageInspector) new AuditMessageInspector(this.EventType, this.IncludeRequestHeaders, this.IncludeResponseHeaders));

    public void ApplyDispatchBehavior(
      ServiceEndpoint endpoint,
      EndpointDispatcher endpointDispatcher)
    {
    }

    public void Validate(ServiceEndpoint endpoint)
    {
    }
  }
}
