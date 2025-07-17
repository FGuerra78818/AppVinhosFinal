using Microsoft.AspNetCore.SignalR;

namespace AppVinhosFinal.Hubs
{
    public class NotificationHub : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var user = Context.User;
            var httpContext = Context.GetHttpContext();
            // Agrupa Admins
            if (user.IsInRole("Admin"))
                await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");

            // Agrupa Users pela sua QuintaId
            var claim = user.FindFirst("QuintaId")?.Value;
            if (int.TryParse(claim, out var qid))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, $"Quinta-{qid}");
            }

            await base.OnConnectedAsync();
        }
    }
}
