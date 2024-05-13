using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ft.user_management.WebApi.Middlewares;

public class RefreshAccessTokenMiddleware
{
    private readonly RequestDelegate _next;

    public RefreshAccessTokenMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("--> Runs before each request");
        await _next(context);
        Console.WriteLine("--> Runs after each request");

        var responseHeaders = context.Response.Headers;
        foreach (var header in responseHeaders)
        {
            Console.WriteLine($"{header.Key}: ${header.Value}");
        }
        
        // Example: You can also modify response headers if needed
        // Ensure you don't modify headers after the response has started sending
        // if (!context.Response.HasStarted)
        // {
        //     context.Response.Headers["X-Custom-Header"] = "CustomValue";
        // }
    }
}