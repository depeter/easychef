using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Pages
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
                    _driver.FindElement(By.ClassName("shoppingcart")).Click();
                    break;
                default:
                    break;
            }
        }
    }

    public enum Navigation
    {
        Home = 0,
        ShoppingCart = 1,
        ShoppingPage = 2
    }
}
