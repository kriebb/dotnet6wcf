// Decompiled with JetBrains decompiler
// Type: Audit.Wcf.Client.WcfClientAction
// Assembly: Audit.Wcf.Client, Version=19.1.4.0, Culture=neutral, PublicKeyToken=571d6b80b242c87e
// MVID: 81125518-9F85-4C2B-953C-51CABA8D821A
// Assembly location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.dll
// XML documentation location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.xml

using System.Collections.Generic;
using System.Net;

namespace be.axa.customer.reglogext.infrastructure.Soap.Audit
{
  public class WcfClientAction
  {
    public WcfClientAction()
    {
      Action = string.Empty;
      RequestBody = string.Empty;
      RequestHeaders = new Dictionary<string, string>();
      HttpMethod = string.Empty;
      ResponseAction = string.Empty;
      MessageId = string.Empty;
      ResponseStatuscode = null;
      ResponseBody = string.Empty;
      ResponseHeaders = new Dictionary<string, string>();
      IsFault = false;
    }

    /// <summary>Request action URL</summary>
    public string Action { get; set; }

    /// <summary>Request body XML</summary>
    public string RequestBody { get; set; }

    /// <summary>Request headers</summary>
    public Dictionary<string, string> RequestHeaders { get; set; }

    /// <summary>HTTP method</summary>
    public string HttpMethod { get; set; }

    /// <summary>Response action</summary>
    public string ResponseAction { get; set; }

    /// <summary>Message ID</summary>
    public string MessageId { get; set; }

    /// <summary>Response HTTP status code</summary>
    public HttpStatusCode? ResponseStatuscode { get; set; }

    /// <summary>Response body XML</summary>
    public string ResponseBody { get; set; }

    /// <summary>Response headers</summary>
    public Dictionary<string, string> ResponseHeaders { get; set; }

    /// <summary>
    /// Value that indicates whether this message generates any SOAP faults.
    /// </summary>
    public bool IsFault { get; set; }
  }
}
