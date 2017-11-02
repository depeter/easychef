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

                sbc.ReceiveEndpoint(host, "scan_categories_queue", ep =>
                {
                    ep.Consumer<ScanCategoriesConsumer>();
                });

                sbc.ReceiveEndpoint(host, "scan_products_for_category_queue", ep =>
                {
                    ep.Consumer<ScanProductsForCategoryConsumer>();
                });

                sbc.ReceiveEndpoint(host, "sync_shoppingcart_for_user_queue", ep =>
                {
                    ep.Consumer<SyncCurrentShoppingCartConsumer>();
                });

                sbc.ReceiveEndpoint(host, "sync_shoppingcart_for_user_queue", ep =>
                {
                    ep.Consumer<AddItemsToShoppingCartConsumer>();
                });
            });

            bus.Start();
        }
    }
}
