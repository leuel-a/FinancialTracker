using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ApiGateway.Middlewares;

public class RefreshAccessTokenMiddleware
{
    private readonly RequestDelegate _next;

    public RefreshAccessTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("Happens before each request");
        await _next(context);
    }
}