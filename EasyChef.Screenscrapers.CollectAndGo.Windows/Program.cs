using System;
using System.Diagnostics;
using EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks;
using MassTransit;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows
{
    public static class Config
    {
        public const string API_URL = "http://localhost:63262/";
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                // one endpoint to receive them all else, we will have concurrency shit
                sbc.ReceiveEndpoint(host, "scrapingjobs_queue", ep =>
                {
                    ep.Consumer<ScanCategoriesConsumer>();
                    ep.Consumer<ScanProductsForCategoryConsumer>();
                    ep.Consumer<SyncCurrentShoppingCartConsumer>();
                    ep.Consumer<AddItemsToShoppingCartConsumer>();
                });

            });

            bus.Start();
        }
    }
}
