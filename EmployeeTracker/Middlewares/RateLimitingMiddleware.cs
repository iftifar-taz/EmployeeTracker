using System.Collections.Concurrent;
using System.Text.Json;

namespace EmployeeTracker.Middlewares
{
    public class RateLimitingMiddleware(RequestDelegate next, ILogger<RateLimitingMiddleware> logger)
    {
        private readonly RequestDelegate _next = next;
        private readonly ILogger<RateLimitingMiddleware> _logger = logger;
        private readonly ConcurrentDictionary<string, (DateTime Timestamp, int RequestCount)> _clients = new();
        private readonly TimeSpan _timeWindow = TimeSpan.FromSeconds(60);
        private readonly int _maxRequests = 30;

        public async Task InvokeAsync(HttpContext context)
        {
            var clientKey = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

            var now = DateTime.UtcNow;
            var (Timestamp, RequestCount) = _clients.GetOrAdd(clientKey, _ => (now, 0));

            if (now - Timestamp > _timeWindow)
            {
                _clients[clientKey] = (now, 1);
            }
            else if (RequestCount < _maxRequests)
            {
                _clients[clientKey] = (Timestamp, RequestCount + 1);
            }
            else
            {
                _logger.LogError("Rate limiting occurred");
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.ContentType = "application/json";
                var response = new { message = "Rate limit exceeded. Try again later." };
                await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                return;
            }

            await _next(context);
        }
    }
}
