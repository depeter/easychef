using EasyChef.Shared.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;
using EasyChef.Shared.RestClients;
using System.Net.Http;
using EasyChef.Shared.Models;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks
{
    public class FetchCurrentShoppingCartConsumer : SeleniumTask<FetchCurrentShoppingCartRequest>, IConsumer<FetchCurrentShoppingCartRequest>
    {
        public IWebDriver Driver { get; set; }

        public FetchCurrentShoppingCartConsumer()
        {
            
        }

        public T Page<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Driver);
        }

        async Task IConsumer<FetchCurrentShoppingCartRequest>.Consume(ConsumeContext<FetchCurrentShoppingCartRequest> context)
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

            try
            {
                var shoppingCart = new ShoppingCart()
                {
                    UserId = 1
                };

                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.ShoppingCart);
                shoppingCart.Products = Page<ShoppingCartPage>().GetProducts();

                var shoppingCartRestClient = new ShoppingCartRestClient(new HttpClient(), "http://localhost:63262/");
                var result = await shoppingCartRestClient.Post(shoppingCart);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                await context.CompleteTask;
                Driver.Close();
                Driver.Dispose();
            }
        }
    }
}
