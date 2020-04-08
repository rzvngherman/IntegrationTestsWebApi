using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Api.Models.Error;

namespace WebApplication1.Api.Middleware
{
    class GlobalExceptionMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                httpContext.Response.Clear();
                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                httpContext.Response.ContentType = JsonContentType;

                var validationResultModel = new InternalErrorResultModel(e);
                string json = JsonConvert.SerializeObject(validationResultModel);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }

    static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }

    internal class InternalErrorResultModel : ErrorResultBaseModel
    {
        public InternalErrorResultModel(Exception ex)
        {
            Errors = new List<ErrorResultModel>
            {
                ex.InnerException != null
                    ? new ErrorResultModel("", ex.InnerException.Message)
                    : new ErrorResultModel("", ex.Message)
            };

            Message = "Internal Server Error";
        }
    }
}