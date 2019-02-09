namespace Server.Side.Events.Models
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    public class ServerSideClient
    {
        private readonly HttpResponse _response;

        internal ServerSideClient(HttpResponse response)
        {
            _response = response;
        }

        //public HttpResponse Response {get {return _response;}}
        public async Task SendMessage(string message, string id)
        {
            await WriteResponseField(_response, "type", "TEST_EVENT");
            await WriteResponseField(_response, "data", message);
            await WriteResponseField(_response, "id", id);
            await _response.Body.FlushAsync();
        }

        private async Task WriteResponseField(HttpResponse response, string field, string data)
        {
            await response.WriteAsync($"{field}:{data}\r\r");
        }
    }
}