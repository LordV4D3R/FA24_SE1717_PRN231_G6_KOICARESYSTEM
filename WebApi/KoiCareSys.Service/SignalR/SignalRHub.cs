using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace KoiCareSys.Service.SignalR
{
    public class SignalRHub : Hub
    {
        private readonly IHubContext<SignalRHub> _context;
        private readonly ILogger<SignalRHub> _logger;

        public SignalRHub(IHubContext<SignalRHub> context, ILogger<SignalRHub> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task SendMessage(string message)
        {
            await _context.Clients.All.SendAsync("ReceiveMessage", message);
        }

        public async Task SendMessageWithData(object data)
        {
            await _context.Clients.All.SendAsync("ReceiveMessageWithData", data);
        }

        public override Task OnConnectedAsync()
        {
            _logger.LogInformation("Client connected: {ConnectionId}", Context.ConnectionId);
            return base.OnConnectedAsync();
        }
    }
}
