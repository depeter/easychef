using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Messages;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages;
using MassTransit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class ScanProductsForCategoryConsumer : SeleniumTask, IConsumer<ScanProductsForCategoryMessage>
    {
        public Task Consume(ConsumeContext<ScanProductsForCategoryMessage> context)
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

            try
            {
                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.Home);

                var categoryRestClient = new CategoryRestClient(new HttpClient(), Config.API_URL);
                var productRestClient = new ProductRestClient(new HttpClient(), Config.API_URL);

                var result = categoryRestClient.List(true).Result;
                if (result.HttpStatus != HttpStatusCode.OK)
                    return Task.FromException(new ApplicationException("Failure fetching categories from rest service."));

                foreach (var category in result.Content)
                {
                    if (category.Link.StartsWith("http"))
                        Driver.Navigate().GoToUrl(category.Link);
                    else
                        Page<ShoppingPage>().NavigateAndOpenChildCategory(category, categoryRestClient);

                    var products = Page<ShoppingPage>().ScanProducts(category.Id);
                    foreach (var product in products)
                    {
                        product.Category = category;
                        product.CategoryId = category.Id;
                        productRestClient.Post(product).Wait();
                    }
                }

                context.Publish(new ScrapingJobResultMessage() { Success = true, MessageId = context.MessageId });

                return Console.Out.WriteLineAsync("All products scanned for all categories with products");
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
