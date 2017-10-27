using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using System.Linq;
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

        public IList<Category> ScanCategories()
        {
            return null;
        }

        public bool OpenCategory(string category)
        {
            return true;
        }

        public IList<Product> ScanProducts()
        {
            return null;   
        }

    public IList<Category> ListAllParentCategories()
    {
        var categories = new List<Category>();
        var liItems = _driver.FindElements(By.CssSelector(".nav__branch.branch ul.tree li"));
        foreach(var liItem in liItems)
        {
            var innerLink = liItem.FindElement(By.TagName("a"));

            var c = new Category();
            c.ExternalId = long.Parse(liItem.GetAttribute("id"));
            c.Link = innerLink.GetAttribute("href");
            c.Name = innerLink.Text;
            c.Parent = null;

            categories.Add(c);
        }
        return categories;
    }

        public void OpenCategory(Category parentCategory)
        {
            _driver.Navigate().GoToUrl(parentCategory.Link);
        }

        public IList<Category> ListAllChildCategories(Category parentCategory)
        {
            var categories = new List<Category>();
            var liItems = _driver.FindElements(By.CssSelector(".nav__branch.branch ul.tree li"));
            foreach (var liItem in liItems)
            {
                var innerLink = liItem.FindElement(By.TagName("a"));

                var c = new Category();
                c.ExternalId = long.Parse(liItem.GetAttribute("id"));
                c.Link = innerLink.GetAttribute("href");
                c.Name = innerLink.Text;
                c.Parent = parentCategory;
                c.HasProducts = liItem.GetAttribute("class").Contains("leaf");

                categories.Add(c);
            }
            return categories;
        }
    }
}
