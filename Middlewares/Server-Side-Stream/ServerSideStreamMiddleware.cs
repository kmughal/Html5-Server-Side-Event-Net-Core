namespace Server.Side.Events.Middlewares.Server_Side_Stream
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;
    using Server.Side.Events.Services;
    using Server.Side.Events.Models;

    public class ServerSideStreamMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IServerSideService _serverSideService;
        public ServerSideStreamMiddleware(RequestDelegate next, IServerSideService serverSideService)
        {
            _next = next;
            _serverSideService = serverSideService;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Headers["Accept"] == "text/event-stream")
            {
                SetHeaders(context.Response);
                ServerSideClient serverSideClient = new ServerSideClient(context.Response);
                var clientId = _serverSideService.Add(serverSideClient);
                await _serverSideService.NotifyAllClients("New client alert:");
                // cancel
                //context.RequestAborted.WaitHandle.WaitOne();
                _serverSideService.RemoveClient(clientId);
                return;
            }

            await _next(context);
        }

        private void SetHeaders(HttpResponse response)
        {
            response.Headers.Add("Content-Type", "text/event-stream");
            response.Headers.Add("Cache-Control", "no-cache");
            response.Headers.Add("Connection", "keep-alive");
        }
    }
}