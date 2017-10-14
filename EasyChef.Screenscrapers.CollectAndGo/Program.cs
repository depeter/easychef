using EasyChef.Shared.Messages;
using MassTransit;
using System;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using EasyChef.Shared.RestClients;
using System.Net.Http;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public class Program
    {
        public static ServiceProvider ServiceProvider;

        public static void Main(string[] args)
        {
            ServiceProvider = new ServiceCollection()
            .AddLogging()
            .AddScoped<IConsumer<FetchCurrentShoppingCartRequest>, FetchCurrentShoppingCartConsumer>()
            .AddScoped<HttpClient>()
            .AddScoped<IShoppingCartRestClient, ShoppingCartRestClient>()
            .BuildServiceProvider();

            const string rabbitMqServerUrl = "rabbitmq://localhost";

            try
            {
                var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
                {

                    var host = sbc.Host(new Uri(rabbitMqServerUrl), h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });

                    sbc.ReceiveEndpoint(host, "test_queue", ep =>
                    {
                        ep.Consumer<FetchCurrentShoppingCartConsumer>();
                    });

                });

                bus.Start();

                bus.Publish(new FetchCurrentShoppingCartRequest { UserId = 1 });

                Console.WriteLine("Press enter key to exit");
                Console.WriteLine("Press any other key to send another message");
                while (Console.ReadKey().Key != ConsoleKey.Enter)
                {
                    bus.Publish(new FetchCurrentShoppingCartRequest { UserId = new Random().Next(1, 100) });
                    Console.WriteLine("Message sent.");
                }

                bus.Stop();
            }
            catch (RabbitMqConnectionException)
            {
                Console.WriteLine("RabbitMQ server not found at {0}", rabbitMqServerUrl);
            }
        }
    }
}
