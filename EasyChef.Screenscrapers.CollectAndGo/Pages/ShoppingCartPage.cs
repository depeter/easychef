using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using EasyChef.Shared.Models;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public class ShoppingCartPage : Page
    {
        public ShoppingCartPage(IWebDriver driver) : base(driver)
        {
        }

        public IList<Product> GetProducts() {
            var productDivs = _driver.FindElements(By.CssSelector("#articles .product"));

            var response = new List<Product>();
            foreach (var div in productDivs)
            {
                var p = new Product();
                p.Name = div.FindElement(By.ClassName("product__name")).Text;
                p.Description = div.FindElement(By.ClassName("product__description")).Text;
                p.Weight = div.FindElement(By.ClassName("product__weight")).Text;
                p.SKU = div.FindElement(By.CssSelector("input[type='hidden'][name='viewArticleNumber']")).GetAttribute("value");
                response.Add(p);
            }
            return response;
        }
    }
}
