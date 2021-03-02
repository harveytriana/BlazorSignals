using System;
using System.Threading;
using System.Threading.Tasks;
using BlazorSignals.Shared;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging;
using static System.Console;

namespace TransmitterUnit
{
    class Program
    {
        static void Main()
        {
            new SensorSimulator().Run();
        }
    }

    class SensorSimulator : IDisposable
    {
        readonly string _HubUri = "https://localhost:44398"; // BlazorSignals.Server 
        readonly string _HubPath = "/SensorHub";
        HubConnection _hubConnection;

        public void Run()
        {
            WriteLine("BROADCAST SENSOR\n");
            WriteLine("URL {0}\n", _HubUri + _HubPath);

            if (_HubUri.Contains(":")) {
                WriteLine("Press Enter key when server is ready");
                ReadKey();
            }

            var loggerFactory = LoggerFactory.Create(builder => {
                builder.AddConsole();
            });
            ILogger logger = loggerFactory.CreateLogger<Program>();

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            try {
                Task.Run(() => MainAsync(logger, cancellationToken).Wait());
            }
            catch (Exception exception) {
                WriteLine($"Exception: {exception.Message}");
            }

            WriteLine("\nPress Enter to Exit ...");
            ReadKey();

            // cancel the thread
            cancellationTokenSource.Cancel();
        }

        async Task MainAsync(ILogger logger, CancellationToken cancellationToken)
        {
            _hubConnection = new HubConnectionBuilder()
                 .WithUrl(_HubUri + _HubPath)
                 .Build();

            try {
                await _hubConnection.StartAsync(cancellationToken);
            }
            catch {
                WriteLine("Can not start connection to hub.");
                return;
            }
            // start
            logger.LogInformation("Start Thread");
            // Initialize a new Random Number Generator:
            var rnd = new Random();

            var value = 0.0;

            var pulse = new Pulse();

            while (true) {
                if (cancellationToken.IsCancellationRequested) {
                    logger.LogInformation("IsCancellationRequested");
                    await _hubConnection.DisposeAsync();
                    return;
                }
                await Task.Delay(250);
                // Generate the value to Broadcast to Clients:
                value = Math.Min(Math.Max(value + (0.1 - rnd.NextDouble() / 5.0), -1), 1);

                // Set the Measurement with a Timestamp assigned:
                pulse.Timestamp = DateTime.UtcNow;
                pulse.Value = value;
                // report
                logger.LogInformation(pulse.ToString());

                // Finally send the value:
                await _hubConnection.SendAsync("Broadcast", "Sensor", pulse, cancellationToken);
            }
        }

        public void Dispose()
        {
            if (_hubConnection != null) {
                Task.Run(async () => await _hubConnection.DisposeAsync());
            }
        }

    }
}


