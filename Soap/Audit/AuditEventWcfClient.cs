// Decompiled with JetBrains decompiler
// Type: Audit.Wcf.Client.AuditEventWcfClient
// Assembly: Audit.Wcf.Client, Version=19.1.4.0, Culture=neutral, PublicKeyToken=571d6b80b242c87e
// MVID: 81125518-9F85-4C2B-953C-51CABA8D821A
// Assembly location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.dll
// XML documentation location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.xml

using Audit.Core;

namespace be.axa.customer.reglogext.infrastructure.Soap.Audit
{
  public class AuditEventWcfClient : AuditEvent
  {
    /// <summary>Gets or sets the WCF action details.</summary>
    public WcfClientAction WcfClientEvent { get; set; } = null!;
  }
}
