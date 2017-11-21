using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Pages.LibelleLekker
{
    public class IndexPage : Page
    {
        public IndexPage(IWebDriver driver) : base(driver)
        {
        }

        public void OpenRecepiesPage()
        {
            _driver.Navigate().GoToUrl("https://www.libelle-lekker.be/zoeken/recepten");
        }

        public void NextPage()
        {
            var nextPageLink = _driver.FindElement(By.CssSelector("div.pager ul li:nth-child(6) a"));
            if (nextPageLink != null && nextPageLink.Text == ">")
                nextPageLink.ClickWithJs();
        }

        public IEnumerable<string> GetRecepiesOnPage()
        {
            var links = _driver.FindElements(By.CssSelector(".products .products__item .products__inner a"));
            return links.Select(x => x.Text);
        }
    }
}
