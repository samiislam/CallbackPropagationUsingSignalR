using System;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;

namespace HttpClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide a client name as the only argument");
                return;
            }

            Console.Title = args[0];
            var connectionURL = $"http://localhost:5000/feedbacknotifierhub?subid={args[0]}";

            var connection = new HubConnectionBuilder()
                .WithUrl(connectionURL)
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                })
                .Build();

            try
            {
                await connection.StartAsync();
            }
            catch(HttpRequestException)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("############################");
                Console.WriteLine("FeedbackNotifierHub is not responding");
                Console.WriteLine("############################");
                return;
            }

            Console.WriteLine("Starting connection. Press Ctrl-C to close.");
            var cts = new CancellationTokenSource();
            Console.CancelKeyPress += (sender, a) =>
            {
                a.Cancel = true;
                cts.Cancel();
            };

            connection.Closed += e =>
            {
                if (e != null)
                {
                    Console.WriteLine("Connection closed with error: {0}", e);
                }

                cts.Cancel();
                return Task.CompletedTask;
            };

            // ReceiveFeedback
            connection.On<string>("ReceiveFeedback", (feedback) =>
            {
                Console.WriteLine(feedback);
            });

            Console.WriteLine("Press any key to close.");
            Console.ReadKey();

            await connection.StopAsync();
        }
    }
}
