using System;
using System.Collections.Generic;
using System.Text;
using EasyChef.Contracts.Shared.Models;
using OpenQA.Selenium;
using EasyChef.Shared.Models;

namespace EasyChef.Screenscrapers.CollectAndGo.Pages
{
    public class ShoppingCartPage : Page
    {
        public ShoppingCartPage(IWebDriver driver) : base(driver)
        {
        }

        public IList<ProductDTO> GetProducts() {
            var productDivs = _driver.FindElements(By.CssSelector("#articles .product"));

            var response = new List<ProductDTO>();
            foreach (var div in productDivs)
            {
                var p = new ProductDTO();
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
