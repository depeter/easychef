using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public abstract class Page
    {
        public IWebDriver _driver;

        public Page(IWebDriver driver)
        {
            this._driver = driver;
        }
    }
}