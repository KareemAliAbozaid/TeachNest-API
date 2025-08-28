using Microsoft.Extensions.Caching.Memory;
using System.Net;
using TechNest.API.Helper;

namespace TechNest.API.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _environment;
        private readonly IMemoryCache _memoryCache;
        private readonly TimeSpan _ratelimitWindow = TimeSpan.FromSeconds(30);

        public ExceptionsMiddleware(RequestDelegate next, IMemoryCache memoryCache, IHostEnvironment environment)
        {
            _next = next;
            _memoryCache = memoryCache;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                if (!IsRequstAllowed(context))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;
                    context.Response.ContentType = "application/json";
                    var response = new ApiException((int)HttpStatusCode.TooManyRequests, "Too many requests. Please try again later.");
                    await context.Response.WriteAsJsonAsync(response);
                }

                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var response = _environment.IsDevelopment()
                    ? new ApiException(500, ex.Message, ex.StackTrace)
                    : new ApiException(500, ex.Message);
                var json = System.Text.Json.JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

        private bool IsRequstAllowed(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();
            var cacheKey = $"RateLimit_{ipAddress}";
            var dateNow = DateTime.Now;

            var (timestamp, requestCount) = _memoryCache.GetOrCreate(cacheKey, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _ratelimitWindow;
                return (timestamp: dateNow, requestCount: 0);
            });

            if (dateNow - timestamp < _ratelimitWindow)
            {
                if (requestCount >= 8)
                {
                    return false;
                }
                _memoryCache.Set(cacheKey, (timestamp, requestCount + 1), _ratelimitWindow);
            }
            else
            {
                _memoryCache.Set(cacheKey, (timestamp, requestCount), _ratelimitWindow);
            }

            return true;
        }
    }
}
