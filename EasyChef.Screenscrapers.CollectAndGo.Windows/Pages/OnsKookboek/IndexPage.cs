using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Pages.OnsKookboek
{
    public class IndexPage : Page
    {
        public IndexPage(IWebDriver driver) : base(driver)
        {
        }

        public IList<string> GetLinks()
        {
            _driver.Navigate().GoToUrl("http://www.onskookboek.be/alfabetische-index");
            var links = _driver.FindElements(By.CssSelector("ul.links li a"));
            return links.Select(x => x.GetAttribute("href")).ToList();
        }
    }
}
