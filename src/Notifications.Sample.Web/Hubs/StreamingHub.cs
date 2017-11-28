using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace Notifications.Sample.Web.Hubs
{
    public class StreamingHub : Hub
    {
        public void SendStreamInit()
        {
            Clients.All.InvokeAsync("streamStarted");
        }

        public IObservable<string> StartStreaming()
        {
            return Observable.Create(
                async (IObserver<string> observer) =>
                {
                    for (int i = 0; i < 10; i++)
                    {
                        observer.OnNext($"sending...{i}");
                        await Task.Delay(1000);
                    }
                });
        }
    }
}
