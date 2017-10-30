using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using System.Linq;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Shared.Models;

namespace EasyChef.Screenscrapers.CollectAndGo.Pages
{
    public class ShoppingPage : Page
    {
        public ShoppingPage(IWebDriver driver) : base(driver)
        {
        }

        public void SearchFor(string productName)
        {

        }

        public IList<CategoryDTO> ScanCategories()
        {
            return null;
        }

        public bool OpenCategory(string category)
        {
            return true;
        }

        public IList<ProductDTO> ScanProducts(int categoryId)
        {
            var products = new List<ProductDTO>();
            var productElements = _driver.FindElements(By.CssSelector("product a.product__body"));
            foreach (var prdEl in productElements)
            {
                products.Add(new ProductDTO()
                {
                    CategoryId = categoryId,
                    Description = prdEl.FindElement(By.ClassName("product__description")).Text,
                    Name = prdEl.FindElement(By.ClassName("product__name")).Text,
                    Weight = prdEl.FindElement(By.ClassName("product__weight")).Text,
                    Price = prdEl.FindElement(By.ClassName("displayPrice1")).Text,
                    UnitPrice = prdEl.FindElement(By.ClassName("displayUnitPrice1")).Text,
                    Unit = prdEl.FindElement(By.ClassName("product__unit")).Text,
                });
            }
            return products;
        }

    public IList<CategoryDTO> ListAllParentCategories()
    {
        var categories = new List<CategoryDTO>();
        var liItems = _driver.FindElements(By.CssSelector(".nav__branch.branch ul.tree li"));
        foreach(var liItem in liItems)
        {
            var innerLink = liItem.FindElement(By.TagName("a"));

            var c = new CategoryDTO();
            c.ExternalId = long.Parse(liItem.GetAttribute("id"));
            c.Link = innerLink.GetAttribute("href");
            c.Name = innerLink.Text;
            c.Parent = null;

            categories.Add(c);
        }
        return categories;
    }

        public void OpenCategory(CategoryDTO parentCategoryDto)
        {
            _driver.Navigate().GoToUrl(parentCategoryDto.Link);
        }

        public IList<CategoryDTO> ListAllChildCategories(CategoryDTO parentCategoryDto)
        {
            var categories = new List<CategoryDTO>();
            var liItems = _driver.FindElements(By.CssSelector(".nav__branch.branch ul.tree li"));
            foreach (var liItem in liItems)
            {
                var innerLink = liItem.FindElement(By.TagName("a"));

                var c = new CategoryDTO();
                c.ExternalId = long.Parse(liItem.GetAttribute("id"));
                c.Link = innerLink.GetAttribute("href");
                c.Name = innerLink.Text;
                c.Parent = parentCategoryDto;
                c.HasProducts = liItem.GetAttribute("class").Contains("leaf");

                categories.Add(c);
            }
            return categories;
        }
    }
}
