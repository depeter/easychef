using EasyChef.Shared.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                var host = sbc.Host(new Uri("rabbitmq://localhost"), h =>
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
    }

    public class FetchCurrentShoppingCartConsumer : IConsumer<FetchCurrentShoppingCartRequest>
    {
        Task IConsumer<FetchCurrentShoppingCartRequest>.Consume(ConsumeContext<FetchCurrentShoppingCartRequest> context)
        {
            return SeleniumWorkerFactory.StartWorker(context.Message);
        }
    }
}
