using System;
using System.Collections.Generic;
using System.Linq;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Pages
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
            var productElements = _driver.FindElements(By.CssSelector(".product"));
            foreach (var productElement in productElements)
            {
                var product = new ProductDTO();
                product.SKU = productElement.GetAttribute("data-productskuid");

                var productBody = productElement.FindElement(By.ClassName("product__body"));

                product.CategoryId = categoryId;
                product.Description = productBody.TryFindElement(By.ClassName("product__description"))?.Text;
                product.Name = productBody.TryFindElement(By.ClassName("product__name"))?.Text;
                product.Weight = productBody.TryFindElement(By.ClassName("product__weight"))?.Text;
                product.Price = productBody.TryFindElement(By.ClassName("displayPrice1"))?.Text;
                product.UnitPrice = productBody.TryFindElement(By.ClassName("displayUnitPrice1"))?.Text;
                product.Unit = productBody.TryFindElement(By.ClassName("product__unit"))?.Text;

                products.Add(product);
            }
            return products;
        }

        public IList<CategoryDTO> ListAllParentCategories()
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
                c.Parent = null;

                categories.Add(c);
            }
            return categories;
        }

        public void OpenCategory(CategoryDTO parentCategoryDto)
        {
            _driver.Navigate().GoToUrl(parentCategoryDto.Link);
        }

        public IList<CategoryDTO> ListAllChildCategories(CategoryDTO parentCategory)
        {
            var liItems = _driver.FindElements(By.CssSelector(".nav__branch.branch > .treewrapper > #treebody ul.tree > li"));
            return ScanChildCategories(parentCategory, liItems);
        }

        private IList<CategoryDTO> ScanChildCategories(CategoryDTO parentCategoryDto, System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> liItems)
        {
            var categories = new List<CategoryDTO>();
            foreach (var liItem in liItems)
            {
                var innerLink = liItem.FindElement(By.TagName("a"));

                var c = new CategoryDTO();
                c.ExternalId = long.Parse(liItem.GetAttribute("id"));
                c.Link = innerLink.GetAttribute("href");
                c.Name = innerLink.Text;
                c.Parent = parentCategoryDto;
                c.ParentId = parentCategoryDto?.Id;
                c.HasProducts = liItem.GetAttribute("class").Contains("leaf");
                c.HasChildren = liItem.GetAttribute("class").Contains("branch");
                c.AvailableOnUrl = _driver.Url;

                categories.Add(c);
            }
            return categories;
        }

        public IList<CategoryDTO> ScanAllChildChildCategories(CategoryDTO parent)
        {
            try
            {
                var parentLi = _driver.FindElement(By.Id(parent.ExternalId.ToString()));
                if (parentLi == null)
                    return new List<CategoryDTO>();

                var link = parentLi.FindElement(By.TagName("a"));
                if (link == null)
                    return new List<CategoryDTO>();

                link.ClickWithJs();

                parent.Children = ScanChildCategories(parent, parentLi.FindElements(By.CssSelector("ul>li")));

                foreach (var child in parent.Children.Where(x => x.HasChildren))
                    ScanAllChildChildCategories(child);

                return parent.Children;
            }
            catch (Exception)
            {
                // ignored
            }
            return new List<CategoryDTO>();
        }

        public void NavigateAndOpenChildCategory(CategoryDTO category, ICategoryRestClient categoryRestClient)
        {
            // fetch the parent until we find one with a valid link
            CategoryDTO parent = category;
            while (parent != null && !parent.Link.StartsWith("http"))
            {
                if (!parent.ParentId.HasValue)
                    return;

                var oldParent = parent;
                parent = categoryRestClient.Get(parent.ParentId.Value).Result.Content;
                parent.Children = new List<CategoryDTO>() { oldParent };
            }

            if (parent == null)
                return;

            // open the category url
            OpenCategory(parent);

            // open child categories until we reach the one that holds the products
            var child = parent.Children.First();
            while (!child.HasProducts)
            {
                var childLi = _driver.FindElement(By.Id(child.ExternalId.ToString()));
                var link = childLi.FindElement(By.TagName("a"));
                link.ClickWithJs();

                child = child.Children.First();
            }

            var finalLi = _driver.FindElement(By.Id(child.ExternalId.ToString()));
            var finalLink = finalLi.FindElement(By.TagName("a"));
            finalLink.ClickWithJs();
        }
    }
}
