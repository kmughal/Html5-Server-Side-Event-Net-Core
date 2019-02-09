namespace Server.Side.Events.Services
{
    using System.Threading.Tasks;
    using System.Collections.Concurrent;
    using System;
    using Server.Side.Events.Models;
    using System.Collections.Generic;
    using System.Net.Http;

    public class ServerSideService : IServerSideService
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public ServerSideService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private readonly ConcurrentDictionary<Guid, ServerSideClient> _clients = 
            new ConcurrentDictionary<Guid, ServerSideClient>();

        public Guid Add(ServerSideClient client)
        {
            var clientId = Guid.NewGuid();
            _clients.TryAdd(clientId, client);
            return clientId;
        }

        private async Task<HttpResponseMessage> GetCircleLineStatus()
        {
            var url = "https://api.tfl.gov.uk/Line/circle/Status?detail=true";
            return await _httpClientFactory.CreateClient().GetAsync(url);
        }

        public async Task NotifyAllClients(string message)
        {
            var idsForAllClients = string.Join(",", _clients.Keys);
            var circleLineStatus = GetCircleLineStatus();
            circleLineStatus.Result.EnsureSuccessStatusCode();
            var messageToSend = await circleLineStatus.Result.Content.ReadAsStringAsync();
            var tasks = new List<Task>();
            foreach (var client in _clients)
            {
                var task = client.Value.SendMessage(messageToSend, client.Key.ToString());
                tasks.Add(task);
            }
            Task.WaitAll(tasks.ToArray());
        }

        public void RemoveClient(Guid clientId)
        {
            _clients.TryRemove(clientId, out ServerSideClient result);
        }
    }
}