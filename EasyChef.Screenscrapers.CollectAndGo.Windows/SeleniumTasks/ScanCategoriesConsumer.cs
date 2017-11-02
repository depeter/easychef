using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Messages;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Shared.RestClients;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages;
using MassTransit;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class ScanCategoriesConsumer : SeleniumTask, IConsumer<RequestScanCategoriesMessage>
    {
        public Task Consume(ConsumeContext<RequestScanCategoriesMessage> context)
        {
            var driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            try
            {
                var categoryRestClient = new CategoryRestClient(new HttpClient(), Config.API_URL);

                Page<LoginPage>().Login();

                driver.WaitUntilLoadingFinished();

                Page<NavigationPage>().NavigateTo(Navigation.ShoppingPage);

                var parentCategories = Page<ShoppingPage>().ListAllParentCategories();
                while (parentCategories.Count == 0)
                {
                    System.Threading.Thread.Sleep(3000);
                    Page<NavigationPage>().NavigateTo(Navigation.ShoppingPage);
                    parentCategories = Page<ShoppingPage>().ListAllParentCategories();
                }

                for (int i = 0; i < parentCategories.Count; i++)
                {
                    var result = categoryRestClient.Post(parentCategories[i]).Result;
                    parentCategories[i] = result.Content;

                    ScanChildCategories(categoryRestClient, parentCategories[i]);
                }

                context.Publish(new ScrapingJobResultMessage() { Success = true, MessageId = context.MessageId });

                return Console.Out.WriteLineAsync("All categories scanned.");
            }
            catch (Exception)
            {

                context.Publish(new ScrapingJobResultMessage() { Success = false, MessageId = context.MessageId });

                // log exception
                throw;
            }
            finally
            {
                driver.Close();
                driver.Dispose();
            }
        }

        private void ScanChildCategories(CategoryRestClient categoryRestClient, CategoryDTO parent)
        {
            Page<ShoppingPage>().OpenCategory(parent);

            var childCategories = Page<ShoppingPage>().ListAllChildCategories(parent);

            foreach (CategoryDTO category in childCategories)
            {
                if (category.Id == 0)
                {
                    if (category.HasChildren) { 
                        // scan all child child categories
                        Page<ShoppingPage>().ScanAllChildChildCategories(category);
                    }

                    var result = categoryRestClient.Post(category).Result;
                    category.Id = result.Content.Id;
                }
            }
        }
    }
}
