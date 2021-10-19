using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> tryRetry()
        {
            var retry = HttpPolicyExtensions
               .HandleTransientHttpError()
               .WaitAndRetryAsync(new[]
               {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(3),
                    TimeSpan.FromSeconds(5)
               },
               (outcome, timespan, retryCount, context) =>
               {
                   Console.ForegroundColor = ConsoleColor.Blue;
                   Console.WriteLine($"Tentando pela {retryCount} vez!");
                   Console.ForegroundColor = ConsoleColor.White;
               });

            return retry;
        }
    }
}
