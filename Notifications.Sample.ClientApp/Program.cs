using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Notifications.Sample.ClientApp
{
    class Program
    {
        private static HubConnection _connection;

        static void Main(string[] args)
        {
            StartConnectionAsync();

            _connection.On<string>("showNotifications", (message) =>
            {
                Console.WriteLine($"{message}");
            });

            Console.ReadLine();
            DisposeAsync();
        }

        public static async Task StartConnectionAsync()
        {
            _connection = new HubConnectionBuilder()
                 .WithUrl("http://localhost:52859/notify")
                 .WithConsoleLogger()
                 .Build();

            await _connection.StartAsync();
        }

        public static async Task DisposeAsync()
        {
            await _connection.DisposeAsync();
        }
    }
}
