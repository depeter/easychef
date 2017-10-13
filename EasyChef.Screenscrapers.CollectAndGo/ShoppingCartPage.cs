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

        public IEnumerable<Product> GetProducts() {
            var productDivs = _driver.FindElements(By.CssSelector("#articles .product"));

            foreach (var div in productDivs)
            {
                yield return new Product{
                    Name = div.FindElement(By.ClassName("product__name")).Text,
                    Description = div.FindElement(By.ClassName("product__description")).Text,
                    Weight = div.FindElement(By.ClassName("product__weight")).Text,
                    SKU = div.FindElement(By.CssSelector("input[type='hidden'][name='viewArticleNumber']")).GetAttribute("value"),
                };
            } 
        }
    }
}
