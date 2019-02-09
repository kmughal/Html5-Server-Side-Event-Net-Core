using System;
using System.Threading.Tasks;
using Server.Side.Events.Models;

namespace Server.Side.Events.Services
{
    public interface IServerSideService
    {
        Guid Add(ServerSideClient client);
        Task NotifyAllClients(string message);
        void RemoveClient(Guid clientId);
    }
}