using Microsoft.AspNetCore.SignalR;

namespace Notifications.Sample.Web.Hubs
{
    public class NotificiationHub : Hub
    {
        public void Notify(string message)
        {
            //Clients.All.InvokeAsync("showNotifications", message);
            Clients.Client(Context.ConnectionId).InvokeAsync("showNotifications", message);
        }
    }
}
