using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Text;

namespace BlogApp.Infrastructure.WebSockets
{
    public static class WebSocketHandler
    {
        private static readonly List<WebSocket> _connectedClients = new List<WebSocket>();

        public static async Task HandleWebSocketAsync(HttpContext context, WebSocket webSocket)
        {
            _connectedClients.Add(webSocket);

            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result;

            do
            {
                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.CloseStatus.HasValue)
                {
                    _connectedClients.Remove(webSocket);
                    await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
                }
            }
            while (!result.CloseStatus.HasValue);
        }

        public static async Task NotifyAllClientsAsync(string message)
        {
            foreach (var client in _connectedClients)
            {
                if (client.State == WebSocketState.Open)
                {
                    var messageBytes = Encoding.UTF8.GetBytes(message);
                    await client.SendAsync(new ArraySegment<byte>(messageBytes, 0, messageBytes.Length),
                                          WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }
    }
}
