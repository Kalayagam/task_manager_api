using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Core.Exceptions;

namespace TaskManagerApi.Middlewares
{   
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            var taskManagerException = exception as TaskManagerException;
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var message = exception.Message;
            var ErrorNumber = 0;

            if (taskManagerException != null)
            {
                message = taskManagerException.Message;
                ErrorNumber = taskManagerException.ErrorNumber;
                statusCode = GetStatusCode(taskManagerException.ErrorNumber);

            }

            response.ContentType = "application/json";
            response.StatusCode = statusCode;
            await response.WriteAsync(JsonConvert.SerializeObject(new
            {
                Message = message,
                ErrorCode = ErrorNumber
            }));
        }

        private static int GetStatusCode(int errorNumber)
        {
            switch (errorNumber)
            {
                case ErrorCodes.ProjectNotFoundResponse:
                    return (int)HttpStatusCode.NotFound;
                case ErrorCodes.ProjectBadRequestResponse:
                    return (int)HttpStatusCode.BadRequest;
                case ErrorCodes.ProjectInternalServerResponse:
                    return (int)HttpStatusCode.InternalServerError;
                default:
                    return (int)HttpStatusCode.BadGateway;

            }
        }
    }
}
