using Microsoft.AspNetCore.Http;
using Polly.CircuitBreaker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.WebApp.MVC.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(CustomHttpResponseException ex)
            {
                HandlerRequestExceptionAsync(httpContext, ex);
            }
            catch(BrokenCircuitException)
            {
                HandleCircuitBreakerExceptionAsync(httpContext);
            }
        }

        private static void HandlerRequestExceptionAsync(HttpContext context,CustomHttpResponseException httpRequestException)
        {
            if(httpRequestException.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                context.Response.Redirect($"/login?ResultUrl={context.Request.Path}");
                return;
            }

            context.Response.StatusCode = (int)httpRequestException.StatusCode;
        }

        private static void HandleCircuitBreakerExceptionAsync( HttpContext context)
        {
            context.Response.Redirect("/sistema-indisponivel");
        }
    }
}
