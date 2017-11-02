using EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Pages
{
    public class LoginPage : Page
    {
        public LoginPage(IWebDriver driver) : base(driver)
        {

        }

        public bool Login(string email, string password)
        {
            _driver.Navigate().GoToUrl("https://www.collectandgo.be/cogo/nl/aanmelden");
            _driver.SwitchTo().Frame(0);
            var inputLogin = _driver.FindElement(By.Id("loginName"));
            inputLogin.SendKeys(email);

            var inputPassword = _driver.FindElement(By.Id("password"));
            inputPassword.SendKeys(password);

            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            return _driver.TryFindElement(By.CssSelector(".logout")) != null;
        }
    }
}