using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Project_GeoService.Middleware
{
    public class RequestLog
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public RequestLog(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<RequestLog>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            finally
            {
                _logger.LogInformation($"{DateTime.Now} - {context.Request?.Method} {context.Request?.Path} {context.Response?.StatusCode}");
            }
        }
    }
}
