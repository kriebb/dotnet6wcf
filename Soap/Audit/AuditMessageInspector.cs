// Decompiled with JetBrains decompiler
// Type: Audit.Wcf.Client.AuditMessageInspector
// Assembly: Audit.Wcf.Client, Version=19.1.4.0, Culture=neutral, PublicKeyToken=571d6b80b242c87e
// MVID: 81125518-9F85-4C2B-953C-51CABA8D821A
// Assembly location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.dll
// XML documentation location: C:\Users\DKRI327\.nuget\packages\audit.wcf.client\19.1.4\lib\net45\Audit.Wcf.Client.xml

using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Audit.Core;

namespace be.axa.customer.reglogext.infrastructure.Soap.Audit
{
    /// <summary>
    /// Message inpector to intercept and log requests and responses for WCF client calls
    /// </summary>
    public class AuditMessageInspector : IClientMessageInspector
    {
        private readonly string _eventType = "{action}";
        private readonly bool _includeRequestHeaders;
        private readonly bool _includeResponseHeaders;

        public AuditMessageInspector()
        {
        }

        public AuditMessageInspector(
            string eventType,
            bool includeRequestHeaders,
            bool includeResponseHeaders)
        {
            this._eventType = eventType ?? "{action}";
            this._includeRequestHeaders = includeRequestHeaders;
            this._includeResponseHeaders = includeResponseHeaders;
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            WcfClientAction wcfClientAction = this.CreateWcfClientAction(request);
            string str = this._eventType.Replace("{action}", wcfClientAction.Action);
            AuditEventWcfClient auditEventWcfClient = new AuditEventWcfClient()
            {
                WcfClientEvent = wcfClientAction
            };
            return (object) AuditScope.Create(new AuditScopeOptions()
            {
                EventType = str,
                AuditEvent = (AuditEvent) auditEventWcfClient,
                SkipExtraFrames = 8
            });
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (!(correlationState is AuditScope auditScope))
                return;
            try
            {
                WcfClientAction? wcfClientEvent = (auditScope.Event as AuditEventWcfClient)?.WcfClientEvent;
                if (wcfClientEvent == null) return;

                wcfClientEvent.IsFault = reply.IsFault;
                wcfClientEvent.ResponseAction = reply.Headers?.Action ?? "No action defined in the header";
                wcfClientEvent.ResponseBody = reply.ToString();
                if (reply.Properties.ContainsKey("httpResponse"))
                {
                    HttpResponseMessageProperty? property =
                        reply.Properties["httpResponse"] as HttpResponseMessageProperty;
                    if (property == null) return;
                    
                    wcfClientEvent.ResponseStatuscode = property?.StatusCode;
                    wcfClientEvent.ResponseHeaders = this._includeResponseHeaders
                        ? GetHeaders(property?.Headers?.ToString() ?? "")
                        : new Dictionary<string, string>();
                }
            }
            finally
            {
                auditScope.Dispose();
            }
        }

        private Dictionary<string, string> GetHeaders(string? headers)
        {
            if (headers == null)
                return new Dictionary<string,string>();
            return ((IEnumerable<string>) headers.Split(new char[2]
            {
                '\r',
                '\n'
            }, StringSplitOptions.RemoveEmptyEntries)).Select<string, KeyValuePair<string, string>>(
                (Func<string, KeyValuePair<string, string>>) (h =>
                {
                    string[] array = ((IEnumerable<string>) h.Split(':')).ToArray<string>();
                    return array.Length > 1
                        ? new KeyValuePair<string, string>(array[0], array[1].TrimStart())
                        : new KeyValuePair<string, string>(array[0], "");
                })).ToDictionary<KeyValuePair<string, string>, string, string>(
                (Func<KeyValuePair<string, string>, string>) (k => k.Key),
                (Func<KeyValuePair<string, string>, string>) (v => v.Value));
        }

        private WcfClientAction CreateWcfClientAction(Message request)
        {
            WcfClientAction wcfClientAction = new WcfClientAction()
            {
                Action = request.Headers?.Action ?? "No action found in the header",
                IsFault = request.IsFault,
                RequestBody = request.ToString(),
                MessageId = request.Headers?.MessageId?.ToString() ?? "No messageid defined in the header"
            };
            if (request.Properties.ContainsKey("httpRequest"))
            {
                HttpRequestMessageProperty? property = request.Properties["httpRequest"] as HttpRequestMessageProperty;
                wcfClientAction.HttpMethod = property?.Method ?? "no httpmethod defined";
                wcfClientAction.RequestHeaders = this._includeRequestHeaders
                    ? this.GetHeaders(property?.Headers?.ToString() ?? "")
                    : new Dictionary<string, string>();
            }

            return wcfClientAction;
        }
    }
}