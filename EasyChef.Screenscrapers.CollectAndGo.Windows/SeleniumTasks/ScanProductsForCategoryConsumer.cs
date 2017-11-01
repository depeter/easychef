using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages;
using EasyChef.Shared.Messages;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class ScanProductsForCategoryConsumer : SeleniumTask
    {
        public async Task Consume()
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

            try
            {
                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.Home);

                var categoryRestClient = new CategoryRestClient(new HttpClient(), Config.API_URL);
                var productRestClient = new ProductRestClient(new HttpClient(), Config.API_URL);

                var result = await categoryRestClient.List(true);
                if (result.HttpStatus != HttpStatusCode.OK)
                    return;

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
                        await productRestClient.Post(product);
                    }
                }
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
