using EasyChef.Shared.Messages;
using MassTransit;
using System;
using MassTransit.RabbitMqTransport;
using Microsoft.Extensions.DependencyInjection;
using EasyChef.Shared.RestClients;
using System.Net.Http;
using EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public static class Config
    {
        public const string API_URL = "http://localhost:63262/";
    }

    public class Program
    {
        public static ServiceProvider ServiceProvider;

        public static void Main(string[] args)
        {
            ServiceProvider = new ServiceCollection()
            .AddLogging()
            .AddScoped<HttpClient>()
            .AddScoped<IShoppingCartRestClient, ShoppingCartRestClient>()
            .BuildServiceProvider();

            const string rabbitMqServerUrl = "rabbitmq://localhost";

            try
            {
                var x = new ScanCategoriesConsumer();
                x.Consume();


                //var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
                //{

                //    var host = sbc.Host(new Uri(rabbitMqServerUrl), h =>
                //    {
                //        h.Username("guest");
                //        h.Password("guest");
                //    });

                //    sbc.ReceiveEndpoint(host, "test_queue", ep =>
                //    {
                //        ep.Consumer<ScanCategoriesConsumer>();
                //    });

                //});

                //bus.Start();

                //bus.Publish(new ScanCategoriesRequest {  });

                //Console.WriteLine("Press enter key to exit");
                //Console.WriteLine("Press any other key to send another message");
                //while (Console.ReadKey().Key != ConsoleKey.Enter)
                //{
                //    bus.Publish(new ScanCategoriesRequest { });
                //    Console.WriteLine("Message sent.");
                //}

                //bus.Stop();
            }
            catch (RabbitMqConnectionException)
            {
                Console.WriteLine("RabbitMQ server not found at {0}", rabbitMqServerUrl);
            }

            Console.ReadKey();
        }
    }
}
