using System;
using System.Net;
using System.Text;
using System.Text.Json;
using API.Errors;

namespace API.Middleware;

public class ExceptionMiddleware(IHostEnvironment env, RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)   // InvokeAsync method has to be in the middleware, request is excepting this method
    {
        try
        {
            await next(context);           // if there is a logic, do it and then PASS it to the NEXT middleware
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, env);  // if there is an exception, handle it using HandleExceptionAsync method
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex, IHostEnvironment env)
    {
        context.Response.ContentType = "application/json";                                             // specify the response body type 
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = env.IsDevelopment()
            ? new ApiErrorResponse(context.Response.StatusCode, ex.Message, ex.StackTrace)
            : new ApiErrorResponse(context.Response.StatusCode, ex.Message, "Internal Server Error");

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };   // Creating JSON response to send to the client
        var json = JsonSerializer.Serialize(response, options);                                         // converting to a JSON object
        return context.Response.WriteAsync(json);
    }
}
