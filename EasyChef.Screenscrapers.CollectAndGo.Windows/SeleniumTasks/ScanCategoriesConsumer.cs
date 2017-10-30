using System;
using System.Collections.Generic;
using System.Net.Http;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Shared.RestClients;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class ScanCategoriesConsumer
    {
        public IWebDriver Driver { get; set; }

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
                while (parentCategories.Count == 0)
                {
                    System.Threading.Thread.Sleep(3000);
                    Page<NavigationPage>(driver).NavigateTo(Navigation.ShoppingPage);
                    parentCategories = Page<ShoppingPage>(driver).ListAllParentCategories();
                }

                for (int i = 0; i < parentCategories.Count; i++)
                {
                    var result = categoryRestClient.Post(parentCategories[i]).Result;
                    parentCategories[i] = result.Content;

                    ScanChildCategories(categoryRestClient, parentCategories[i], driver);
                }
            }
            catch (Exception ex)
            {
                // log exception
                throw ex;
            }
            finally
            {
                driver.Close();
                driver.Dispose();
            }
        }

        private void ScanChildCategories(CategoryRestClient categoryRestClient, CategoryDTO parent, IWebDriver driver)
        {
            Page<ShoppingPage>(driver).OpenCategory(parent);

            var childCategories = Page<ShoppingPage>(driver).ListAllChildCategories(parent);

            for (int j = 0; j < childCategories.Count; j++)
            {
                if (childCategories[j].Id == 0)
                {
                    var result = categoryRestClient.Post(childCategories[j]).Result;
                    childCategories[j] = result.Content;
                }
            }
        }
    }
}
