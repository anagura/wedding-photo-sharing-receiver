using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using LineMessaging;

static readonly string ChannelSecret = Environment.GetEnvironmentVariable("ChannelSecret", EnvironmentVariableTarget.Process);

public static async Task<string> Run(HttpRequestMessage req, TraceWriter log)
{
    var webhookRequest = new LineWebhookRequest(ChannelSecret, req);
    var valid = await webhookRequest.IsValid();
    if (!valid)
    {
        log.Error("request is invalid.");
        return null;
    }

    return await webhookRequest.GetContentJson();
}
