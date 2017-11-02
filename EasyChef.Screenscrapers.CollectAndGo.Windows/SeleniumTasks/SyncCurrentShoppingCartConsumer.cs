using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Messages;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages;
using EasyChef.Shared.RestClients;
using MassTransit;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class SyncCurrentShoppingCartConsumer : SeleniumTask, IConsumer<SyncCurrentShoppingCartMessage>
    {

        public Task Consume(ConsumeContext<SyncCurrentShoppingCartMessage> context)
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

            var shoppingCartRestClient = new ShoppingCartRestClient(new HttpClient(), Config.API_URL);
            var productRestClient = new ProductRestClient(new HttpClient(), Config.API_URL);

            try
            {
                var currentUserId = 1;
                var shoppingCart = shoppingCartRestClient.GetByUser(currentUserId).Result.Content;

                if (shoppingCart == null)
                {
                    shoppingCart = new ShoppingCartDTO()
                    {
                        UserId = currentUserId
                    };
                }
                shoppingCart.ShoppingCartProducts = new List<ShoppingCartProductDTO>();

                Page<LoginPage>().Login(context.Message.Email, context.Message.Password);
                Page<NavigationPage>().NavigateTo(Navigation.ShoppingCart);
                var skus = Page<ShoppingCartPage>().GetProductSkus();
                foreach (var sku in skus)
                {
                    var product = productRestClient.GetBySku(sku).Result.Content;
                    if (product != null)
                    {
                        shoppingCart.ShoppingCartProducts.Add(new ShoppingCartProductDTO()
                        {
                            ShoppingCart = shoppingCart,
                            ProductId = product.Id
                        });
                    }
                }

                if (shoppingCart.Id == 0)
                    shoppingCartRestClient.Post(shoppingCart).Wait();
                else
                    shoppingCartRestClient.Put(shoppingCart).Wait();

                context.Publish(new ScrapingJobResultMessage() { Success = true, MessageId = context.MessageId });

                return Console.Out.WriteLineAsync("Shoppingcart synched for user " + context.Message.UserId);
            }
            catch (Exception)
            {
                context.Publish(new ScrapingJobResultMessage() { Success = false, MessageId = context.MessageId });
                throw;
            }
            finally
            {
                Driver.Close();
                Driver.Dispose();
            }
        }
    }
}
