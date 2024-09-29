using BlogApp.Infrastructure.WebSockets;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlogApp.Infrastructure.Ioc;

public static class DependencyInjectionWebSocket
{
    public static IServiceCollection AddWebSocketConfiguration(this IServiceCollection services)
    {
        return services;
    }

    public static IApplicationBuilder UseWebSocketConfiguration(this IApplicationBuilder app)
    {
        var webSocketOptions = new WebSocketOptions()
        {
            KeepAliveInterval = TimeSpan.FromMinutes(2)
        };

        app.UseWebSockets(webSocketOptions);

        app.Use(async (context, next) =>
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    var webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    await WebSocketHandler.HandleWebSocketAsync(context, webSocket); // Aqui você chama seu handler para processar a conexão
                }
                else
                {
                    context.Response.StatusCode = 400; // Retorna 400 se não for uma requisição WebSocket
                }
            }
            else
            {
                await next();
            }
        });

        return app;
    }
}
