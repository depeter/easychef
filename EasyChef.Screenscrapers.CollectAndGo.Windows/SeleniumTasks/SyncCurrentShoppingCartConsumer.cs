using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages;
using EasyChef.Shared.Messages;
using EasyChef.Shared.RestClients;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class SyncCurrentShoppingCartConsumer : SeleniumTask<SyncCurrentShoppingCartRequest>
    {
        public IWebDriver Driver { get; set; }
        
        public T Page<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Driver);
        }

        public async Task Consume()
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

            try
            {
                var shoppingCart = new ShoppingCartDTO()
                {
                    UserId = 1
                };

                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.ShoppingCart);
                shoppingCart.Products = Page<ShoppingCartPage>().GetProducts();

                var shoppingCartRestClient = new ShoppingCartRestClient(new HttpClient(), Config.API_URL);
                await shoppingCartRestClient.Post(shoppingCart);
            }
            catch (Exception)
            {
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
