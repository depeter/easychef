using EasyChef.Screenscrapers.CollectAndGo.Pages;
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
    public class ScanProductsForCategoryConsumer : SeleniumTask<ScanProductsForCategoryRequest>, IConsumer<ScanProductsForCategoryRequest>
    {
        public IWebDriver Driver { get; set; }

        public ScanProductsForCategoryConsumer()
        {
            
        }

        public T Page<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Driver);
        }

        async Task IConsumer<ScanProductsForCategoryRequest>.Consume(ConsumeContext<ScanProductsForCategoryRequest> context)
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

            try
            {
                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.Home);

                Page<ShoppingPage>().OpenCategory(context.Message.Category);

                Page<ShoppingPage>().ScanProducts();

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
