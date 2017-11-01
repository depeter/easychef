using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Pages
{
    public class NavigationPage : Page
    {
        public NavigationPage(IWebDriver driver) : base(driver)
        {
        }

        public void NavigateTo(Navigation navigation)
        {
            switch (navigation)
            {
                case Navigation.Home:
                case Navigation.ShoppingPage:
                    _driver.Navigate().GoToUrl("https://colruyt.collectandgo.be/cogo/nl/home");
                    break;
                case Navigation.ShoppingCart:
                    _driver.Navigate().GoToUrl("https://colruyt.collectandgo.be/cogo/nl/mijn-winkelwagen");
                    break;
                default:
                    break;
            }
            _driver.WaitUntilLoadingFinished();
        }
    }

    public enum Navigation
    {
        Home = 0,
        ShoppingCart = 1,
        ShoppingPage = 2
    }
}
