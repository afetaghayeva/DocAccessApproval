using DocAccessApproval.Domain.Exceptions;
using DocAccessApproval.Domain.Exceptions.DocumentExceptions;
using DocAccessApproval.Domain.Exceptions.UserExceptions;
using System.Net;

namespace DocAccessApproval.WebApi.Middlewares;

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
            await _next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        if (exception.GetType() == typeof(DocumentException)) return CreateDocumentException(context, exception);
        if (exception.GetType() == typeof(UserException)) return CreateUserException(context, exception);
        
        return CreateInternalException(context, exception);
    }

    private Task CreateDocumentException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return context.Response.WriteAsync(new DocumentProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "document",
            Title = "Document exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateUserException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.BadRequest);

        return context.Response.WriteAsync(new UserProblemDetails
        {
            Status = StatusCodes.Status400BadRequest,
            Type = "user",
            Title = "User exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }

    private Task CreateInternalException(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = Convert.ToInt32(HttpStatusCode.InternalServerError);

        return context.Response.WriteAsync(new InternalProblemDetails
        {
            Status = StatusCodes.Status500InternalServerError,
            Type = "internal",
            Title = "Internal exception",
            Detail = exception.Message,
            Instance = ""
        }.ToString());
    }
}