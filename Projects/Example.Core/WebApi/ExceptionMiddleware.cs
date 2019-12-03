using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Example.Core.WebApi
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ExceptionMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex, logger);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception, ILogger logger)
        {
            var code = HttpStatusCode.InternalServerError;

            if (IsTypeOfException<ValidationException>(exception, out var outEx))
                code = HttpStatusCode.BadRequest;
            else if (IsTypeOfException<KeyNotFoundException>(exception, out outEx))
                code = HttpStatusCode.NotFound;
            else if (IsTypeOfException<UnauthorizedAccessException>(exception, out outEx))
                code = HttpStatusCode.Unauthorized;

            if (outEx == null)
                outEx = exception;

            var errorMessage = GetErrorMessageFromException(outEx);
            var result = JsonConvert.SerializeObject(new
            {
                error = errorMessage,
                stacktrace = GetStackTraceFromException(outEx)
            });

            if (code == HttpStatusCode.InternalServerError)
                logger.LogError(outEx, "Http Exception Middleware - Code: {Code} - Message: {eMessage}", code.ToString("G"), errorMessage);
            else
                logger.LogWarning(outEx, "Http Exception Middleware - Code: {Code} - Message: {eMessage}", code.ToString("G"), errorMessage);

            context.Response.Clear();
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }

        private static bool IsTypeOfException<T>(Exception ex, out Exception outEx) where T : Exception
        {
            outEx = null;
            if (!(ex is T exception))
                return ex.InnerException != null && IsTypeOfException<T>(ex.InnerException, out outEx);
            outEx = exception;
            return true;
        }

        private static string GetErrorMessageFromException(Exception ex)
        {
            var msg = new StringBuilder();
            while (ex != null)
            {
                msg.AppendFormat("{0}: {1}\r\n", ex.Source, ex.Message);
                ex = ex.InnerException;
            }
            return msg.ToString();
        }

        private string GetStackTraceFromException(Exception ex)
        {
            if (ex == null)
                return "";

            var counter = 0;
            var messageStringBuilder = new StringBuilder();
            var stackTraceStringBuilder = new StringBuilder();

            while (ex != null)
            {
                counter++;
                messageStringBuilder.AppendLine(
                    $"[{counter.ToString().PadLeft(2, '0')}] Message : {ex.Message ?? $"{ex.GetType()}"}");
                if (!string.IsNullOrEmpty(ex.StackTrace))
                {
                    using (var reader = new StringReader(ex.StackTrace))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                            if (line.Contains(".cs:line"))
                            {
                                messageStringBuilder.AppendLine($"             : {line}");
                                break;
                            }
                    }

                    stackTraceStringBuilder.AppendLine(
                        $"[{counter.ToString().PadLeft(2, '0')}] StackTrace: {ex.StackTrace}");
                    stackTraceStringBuilder.AppendLine("--- --- ---");
                }

                ex = ex.InnerException;
            }

            return $@"{messageStringBuilder}

------------------

{stackTraceStringBuilder}".Trim();
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
