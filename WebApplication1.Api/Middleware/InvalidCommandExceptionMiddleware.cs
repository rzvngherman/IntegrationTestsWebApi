using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Api.Models.Error;
using WebApplication1.Api.Models.Error.SeedWork;

namespace WebApplication1.Api.Middleware
{
    class InvalidCommandExceptionMiddleware
    {
        private const string JsonContentType = "application/json";
        private readonly RequestDelegate _next;

        public InvalidCommandExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (InvalidCommandException ice)
            {
                var validationResult = new ValidationResultModel(ice);

                httpContext.Response.Clear();
                httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
                httpContext.Response.ContentType = JsonContentType;

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(validationResult));
            }
        }
    }

    static class InvalidCommandExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseInvalidCommandExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<InvalidCommandExceptionMiddleware>();
        }
    }

    class ValidationResultModel : ErrorResultBaseModel
    {
        private const string ValidationErrorMessage = "Validation error";

        public ValidationResultModel(InvalidCommandException ice)
        {
            Message = ValidationErrorMessage;
            Errors = ice.Errors.Keys.SelectMany(key => DoValidation(key, ice.Errors[key])).ToList();
        }

        private IEnumerable<ErrorResultModel> DoValidation(string key, IEnumerable<string> validationMessage)
        {
            return validationMessage.Select(message => new ErrorResultModel(key, message));
        }
    }
}
