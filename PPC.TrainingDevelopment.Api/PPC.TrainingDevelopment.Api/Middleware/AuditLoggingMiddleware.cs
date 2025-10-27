using PPC.TrainingDevelopment.Api.Models;
using PPC.TrainingDevelopment.Api.Services.Interfaces;
using System.Diagnostics;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace PPC.TrainingDevelopment.Api.Middleware
{
    public class AuditLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditLoggingMiddleware> _logger;

        public AuditLoggingMiddleware(RequestDelegate next, ILogger<AuditLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, IAuditLogService auditLogService)
        {
            // Skip audit logging for certain paths
            if (ShouldSkipAuditing(context.Request.Path))
            {
                await _next(context);
                return;
            }

            var stopwatch = Stopwatch.StartNew();
            var originalResponseBodyStream = context.Response.Body;

            // Create a new memory stream for the response
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // Read request body
            var requestBody = await ReadRequestBodyAsync(context.Request);

            var auditLog = new AuditLog
            {
                UserId = GetUserId(context),
                UserName = GetUserName(context),
                HttpMethod = context.Request.Method,
                RequestPath = context.Request.Path,
                QueryString = context.Request.QueryString.ToString(),
                Controller = GetControllerName(context),
                Action = GetActionName(context),
                RequestBody = requestBody,
                Timestamp = DateTime.UtcNow,
                IpAddress = GetClientIpAddress(context),
                UserAgent = context.Request.Headers["User-Agent"].ToString(),
                AdditionalInfo = GetAuthenticationInfo(context)
            };

            Exception? exception = null;
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                exception = ex;
                auditLog.ExceptionDetails = $"{ex.Message}\n{ex.StackTrace}";
                throw;
            }
            finally
            {
                stopwatch.Stop();

                // Read response body
                responseBody.Seek(0, SeekOrigin.Begin);
                var responseBodyText = await new StreamReader(responseBody).ReadToEndAsync();

                // Copy response body back to original stream
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalResponseBodyStream);

                // Complete audit log
                auditLog.StatusCode = context.Response.StatusCode;
                auditLog.DurationMs = stopwatch.ElapsedMilliseconds;

                // Only log response body for certain status codes and if it's not too large
                if (ShouldLogResponseBody(context.Response.StatusCode) && responseBodyText.Length < 10000)
                {
                    auditLog.ResponseBody = responseBodyText;
                }

                // Log the audit entry
                await auditLogService.LogAsync(auditLog);
            }
        }

        private static bool ShouldSkipAuditing(string path)
        {
            var pathsToSkip = new[]
            {
                "/swagger",
                "/health",
                "/metrics",
                "/favicon.ico",
                "/api/auditlogs" // Don't audit the audit log endpoint itself
            };

            return pathsToSkip.Any(skipPath => path.StartsWith(skipPath, StringComparison.OrdinalIgnoreCase));
        }

        private static async Task<string?> ReadRequestBodyAsync(HttpRequest request)
        {
            if (!request.ContentLength.HasValue || request.ContentLength.Value == 0)
                return null;

            // Only read body for certain content types and if it's not too large
            var contentType = request.ContentType?.ToLowerInvariant();
            var isJsonOrForm = contentType?.Contains("application/json") == true ||
                              contentType?.Contains("application/x-www-form-urlencoded") == true;

            if (!isJsonOrForm || request.ContentLength > 50000)
                return null;

            request.EnableBuffering();
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var requestBody = Encoding.UTF8.GetString(buffer);
            request.Body.Position = 0;

            return requestBody;
        }

        private static string GetUserId(HttpContext context)
        {
            var user = context.User;
            if (user?.Identity?.IsAuthenticated != true)
                return "Anonymous";

            return user.FindFirst(ClaimTypes.NameIdentifier)?.Value ??
                   user.FindFirst("sub")?.Value ??
                   user.FindFirst("user_id")?.Value ??
                   "Anonymous";
        }

        private static string GetUserName(HttpContext context)
        {
            var user = context.User;
            if (user?.Identity?.IsAuthenticated != true)
                return "Anonymous";

            return user.FindFirst(ClaimTypes.Name)?.Value ??
                   user.FindFirst("name")?.Value ??
                   user.FindFirst("username")?.Value ??
                   GetUserId(context);
        }

        private static string GetAuthenticationInfo(HttpContext context)
        {
            var user = context.User;
            if (user?.Identity?.IsAuthenticated != true)
                return "IsAuthenticated=false";

            var claimsCount = user.Claims?.Count() ?? 0;
            var authType = user.Identity.AuthenticationType ?? "Unknown";
            return $"IsAuthenticated=true, ClaimsCount={claimsCount}, AuthType={authType}";
        }

        private static string GetControllerName(HttpContext context)
        {
            var controllerName = context.Request.RouteValues["controller"]?.ToString();
            return controllerName ?? "Unknown";
        }

        private static string GetActionName(HttpContext context)
        {
            var actionName = context.Request.RouteValues["action"]?.ToString();
            return actionName ?? "Unknown";
        }

        private static string? GetClientIpAddress(HttpContext context)
        {
            // Check for forwarded IP first (in case behind proxy/load balancer)
            var forwardedFor = context.Request.Headers["X-Forwarded-For"].FirstOrDefault();
            if (!string.IsNullOrEmpty(forwardedFor))
            {
                return forwardedFor.Split(',')[0].Trim();
            }

            var realIp = context.Request.Headers["X-Real-IP"].FirstOrDefault();
            if (!string.IsNullOrEmpty(realIp))
            {
                return realIp;
            }

            return context.Connection.RemoteIpAddress?.ToString();
        }

        private static bool ShouldLogResponseBody(int statusCode)
        {
            // Log response body for errors and successful operations
            return statusCode >= 200 && statusCode < 300 || statusCode >= 400;
        }
    }
}