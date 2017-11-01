using System.Collections.Generic;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Pages
{
    public class ShoppingCartPage : Page
    {
        public ShoppingCartPage(IWebDriver driver) : base(driver)
        {
        }

        public IList<string> GetProductSkus() {
            var productDivs = _driver.FindElements(By.CssSelector("#articles .product"));

            var response = new List<string>();
            foreach (var div in productDivs)
            {
                response.Add(div.FindElement(By.CssSelector("input[type='hidden'][name='viewArticleNumber']")).GetAttribute("value"));
            }
            return response;
        }
    }
}
