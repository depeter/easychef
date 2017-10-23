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
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks
{
    public class ScanCategoriesConsumer
    {
        public IWebDriver Driver { get; set; }

        public ScanCategoriesConsumer()
        {
            
        }

        public T Page<T>(IWebDriver driver = null)
        {
            return (T)Activator.CreateInstance(typeof(T), driver ?? Driver);
        }

        public void Consume()
        {
            var driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
            try
            {
                var categoryRestClient = new CategoryRestClient(new HttpClient(), Config.API_URL);

                Page<LoginPage>(driver).Login();
                
                Page<NavigationPage>(driver).NavigateTo(Navigation.ShoppingPage);

                var parentCategories = Page<ShoppingPage>(driver).ListAllParentCategories();
                for (int i = 0; i < parentCategories.Count; i++)
                {
                    var result = categoryRestClient.Post(parentCategories[i]).Result;
                    parentCategories[i] = result.Content;

                    parentCategories[i].Children = ScanChildCategories(categoryRestClient, parentCategories[i], driver);

                    categoryRestClient.Post(parentCategories[i]).Wait();
                }
            }
            catch (Exception)
            {
                // log exception
                throw;
            }
            finally
            {
                driver.Close();
                driver.Dispose();
            }
        }

        private IList<Category> ScanChildCategories(CategoryRestClient categoryRestClient, Category parent, IWebDriver driver)
        {
            Page<ShoppingPage>(driver).OpenCategory(parent);
            
            var childCategories = Page<ShoppingPage>(driver).ListAllChildCategories(parent);

            for (int j = 0; j < childCategories.Count; j++)
            {
                var result = categoryRestClient.Post(childCategories[j]).Result;
                childCategories.Add(result.Content);
            }
            return childCategories;
        }
    }
}
