using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows;
using EasyChef.Shared.Messages;
using EasyChef.Shared.Models;
using EasyChef.Shared.RestClients;
using MassTransit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks
{
    public class AddItemsToShoppingCartConsumer : IConsumer<AddItemsToShoppingCartRequest>
    {
        public IWebDriver Driver { get; set; }

        public AddItemsToShoppingCartConsumer()
        {
            
        }

        public T Page<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Driver);
        }

        async Task IConsumer<AddItemsToShoppingCartRequest>.Consume(ConsumeContext<AddItemsToShoppingCartRequest> context)
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

            try
            {
                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.Home);

                
                var shoppingCartRestClient = new ShoppingCartRestClient(new HttpClient(), Config.API_URL);
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
