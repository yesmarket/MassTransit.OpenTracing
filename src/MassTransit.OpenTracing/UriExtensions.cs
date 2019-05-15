using System;

namespace MassTransit.OpenTracing
{
    public static class UriExtensions
    {
        public static string GetExchangeName(this Uri value)
        {
            var exchange = value.LocalPath;
            var messageType = exchange.Substring(exchange.LastIndexOf('/') + 1);
            return messageType;
        }
    }
}