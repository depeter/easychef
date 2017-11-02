using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Messages;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages;
using EasyChef.Shared.RestClients;
using MassTransit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class AddItemsToShoppingCartConsumer : SeleniumTask, IConsumer<AddItemsToShoppingCartMessage>
    {

        public Task Consume(ConsumeContext<AddItemsToShoppingCartMessage> context)
        {
            try
            {
                Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.ShoppingPage);

                foreach (var sku in context.Message.SkuNumbers)
                {
                    Page<ShoppingPage>().SearchForSku(sku);
                    var products = Page<ShoppingPage>().ScanProducts();
                    if (products.Count > 0)
                    {
                        Page<ShoppingPage>().AddProductToCart(sku);
                    }
                }

                context.Publish(new ScrapingJobResultMessage() {Success = true, MessageId = context.MessageId });

                return Console.Out.WriteLineAsync("Skus added to shoppingcart of user " + context.Message.UserId);
            }
            catch (Exception)
            {
                context.Publish(new ScrapingJobResultMessage(){ Success = false, MessageId = context.MessageId });

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
