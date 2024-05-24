using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using ft.user_management.Application.Contracts.Infrastructure;

namespace ft.user_management.WebApi.Middlewares
{
    public class RefreshAccessTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public RefreshAccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ITokenService tokenService)
        {
            if (context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader) &&
                context.Request.Headers.TryGetValue("x-refresh", out var refreshTokenHeader))
            {
                var accessToken = authorizationHeader.ToString().Split(" ")[1];
                var refreshToken = refreshTokenHeader.ToString();

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    var isAccessTokenExpired = tokenService.IsTokenExpired(accessToken);
                    var isRefreshTokenExpired = tokenService.IsTokenExpired(refreshToken);
                    
                    Console.WriteLine(isAccessTokenExpired);
                    
                    if (isAccessTokenExpired && !isRefreshTokenExpired)
                    {
                        var newAccessToken = await tokenService.RefreshAccessToken(accessToken, refreshToken);
                        if (newAccessToken != null)
                        {
                            context.Request.Headers.Authorization = $"Bearer {newAccessToken}";
                            context.Response.Headers["x-access-token"] = newAccessToken;
                        }
                    }
                }
            }
            await _next(context);
        }
    }
}