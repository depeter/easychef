using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public static class SeleniumExtensions
    {
        public static void SendKeysJS(this IWebElement element, string text)
        {
            ((IJavaScriptExecutor)GetDriverFromElement(element)).ExecuteScript("arguments[0].setAttribute('value', '" + text + "')", element);
        }

        private static IWebDriver GetDriverFromElement(IWebElement e)
        {
            return ((IWrapsDriver)e).WrappedDriver;
        }
    }
}
