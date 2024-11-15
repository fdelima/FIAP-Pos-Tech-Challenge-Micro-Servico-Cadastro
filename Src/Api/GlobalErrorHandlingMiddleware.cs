﻿using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Messages;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using System.Net;
using System.Text.Json;

namespace FIAP.Pos.Tech.Challenge.Api
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder AddGlobalErrorHandler(this IApplicationBuilder applicationBuilder)
            => applicationBuilder.UseMiddleware<GlobalErrorHandlingMiddleware>();
    }

    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {

            string stackTrace = (exception.StackTrace ?? "");
            stackTrace = stackTrace.Length > 200 ? stackTrace.IndexOf(" in ") > -1 ? stackTrace.Substring(0, stackTrace.IndexOf(" in ")) : stackTrace.Substring(0, 200) : stackTrace;
            stackTrace += " ...";

            ModelResult m = ModelResultFactory.Error(exception.Message, stackTrace);
            m.AddMessage(ErrorMessages.InternalServerError);

            string exceptionResult = JsonSerializer.Serialize(m);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
