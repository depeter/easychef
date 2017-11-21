using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure
{
    public static class SeleniumExtensions
    {
        public static IWebElement TryFindElement(this IWebDriver driver, By by)
        {
            try
            {
                return driver.FindElement(by);
            }
            catch (Exception)
            {
                // ignore
            }
            return null;
        }

        public static void SendKeysJS(this IWebElement element, string text)
        {
            ((IJavaScriptExecutor)GetDriverFromElement(element)).ExecuteScript("arguments[0].setAttribute('value', '" + text + "')", element);
        }

        private static IWebDriver GetDriverFromElement(IWebElement e)
        {
            return ((IWrapsDriver)e).WrappedDriver;
        }

        public static IWebElement WaitUntilElementVisible(this IWebDriver driver, By elementSelector, Action<IWebElement> thenAction = null, bool sleep = true)
        {
            WaitUntilLoadingFinished(driver, true, sleep: sleep);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            IWebElement foundElement = null;
            while (foundElement == null || !foundElement.Displayed)
            {
                var foundElements = driver.FindElements(elementSelector);
                if (foundElements.Count > 0)
                    foundElement = foundElements[0];

                if (stopwatch.ElapsedMilliseconds > (60000 * 10))
                    throw new TimeoutException("Selenium is taking longer than 5 minutes to wait until an element becomes visible on the page.");
                if (foundElement == null)
                    Thread.Sleep(500);
            }

            if (sleep)
                Thread.Sleep(200);

            if (thenAction != null)
                thenAction.Invoke(foundElement);

            return foundElement;
        }

        public static IWebElement TryWaitUntilElementVisible(this IWebDriver driver, By elementSelector, int maxWaitTimeInMilliSeconds)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            IWebElement foundElement = null;
            while (foundElement == null || !foundElement.Displayed)
            {
                var foundElements = driver.FindElements(elementSelector);
                if (foundElements.Count > 0)
                    foundElement = foundElements[0];

                if (stopwatch.ElapsedMilliseconds > maxWaitTimeInMilliSeconds)
                    return null;
                if (foundElement == null)
                    Thread.Sleep(500);
            }
            
            return foundElement;
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");
        }

        public static void ScrollToBottomSlowly(this IWebDriver driver, int times, int milliWait)
        {
            foreach (var x in Enumerable.Range(0, times))
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("window.scrollBy(0, 300);");
                Thread.Sleep(milliWait);
            }
            
        }

        public static IList<IWebElement> WaitUntilElementsVisible(this IWebDriver driver, By elementSelector, Action<IList<IWebElement>> thenAction = null)
        {
            WaitUntilLoadingFinished(driver, true);

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var foundElements = driver.FindElements(elementSelector);
            while (!foundElements.Any(e => e.Displayed))
            {
                if (stopwatch.ElapsedMilliseconds > 60000 * 10)
                    throw new TimeoutException("Selenium is taking longer than 5 minutes to wait until at least one element of a collection becomes visible on the page.");
                Thread.Sleep(500);
            }

            Thread.Sleep(500);

            if (thenAction != null)
                thenAction.Invoke(foundElements);

            return foundElements;
        }

        public static void WaitUntilUrlMatches(this IWebDriver driver, Func<string, bool> condition)
        {
            int maxRemainingTime = 15000;
            while (maxRemainingTime > 0)
            {
                if (condition(driver.Url))
                {
                    return;
                }
                Thread.Sleep(500);
                maxRemainingTime -= 500;
            }

            throw new TimeoutException("Url did not change in reasonable time");
        }

        /// <summary>
        /// Hold execution until the page is done loading
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="waitUntilDomIsStable">Use this on pages with complex javascript (such as aurelia)</param>
        /// <returns></returns>
        public static bool WaitUntilLoadingFinished(this IWebDriver driver, bool waitUntilDomIsStable = false, bool sleep = true)
        {
            // standard wait is one second
            if (sleep)
                Thread.Sleep(1000);

            var timeSlept = new TimeSpan();
            var js = (IJavaScriptExecutor)driver;
            while (!js.ExecuteScript("return document.readyState").Equals("complete"))
            {
                Thread.Sleep(200);
                timeSlept = timeSlept.Add(new TimeSpan(0, 0, 0, 0, 200));
                if (timeSlept > TimeSpan.FromSeconds(30))
                    throw new TimeoutException("Selenium is taking too long to load the page.");
            }

            if (waitUntilDomIsStable)
                WaitUntilDomIsStable(driver, 14000, 500);

            return true;
        }

        /// <summary>
        /// This method validates that there are no changes to the html document (the DOM)
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="maxWaitMillis">The max wait time, if this is exceeded we just give up and return</param>
        /// <param name="pollDelimiter">The time between DOM comparison checks</param>
        private static void WaitUntilDomIsStable(IWebDriver driver, int maxWaitMillis, int pollDelimiter)
        {
            var totalTimeWaited = new Stopwatch();
            while (totalTimeWaited.ElapsedMilliseconds < maxWaitMillis)
            {
                var prevState = driver.PageSource;
                Thread.Sleep(pollDelimiter); // <-- would need to wrap in a try catch
                if (prevState.Equals(driver.PageSource))
                {
                    return;
                }
            }
        }

        public static void SetInputValue(this IWebDriver driver, string id, string text)
        {
            ((IJavaScriptExecutor)driver).ExecuteScript("var exampleInput = document.getElementById('" + id + "'); exampleInput.value = '" + text + "'; exampleInput.dispatchEvent(new Event('change'));");
        }
    }

    public static class WebElementExtensions
    {
        public static IWebElement TryFindElement(this IWebElement element, By by)
        {
            try
            {
                return element.FindElement(by);
            }
            catch (Exception)
            {
                // ignore
            }
            return null;
        }

        public static void SendKeysSafe(this IWebElement element, string keys)
        {
            foreach (var key in keys)
            {
                element.SendKeys(key.ToString());
                Thread.Sleep(50);
            }
        }
        

        public static void ClickWithJs(this IWebElement element)
        {
            IJavaScriptExecutor ex = (IJavaScriptExecutor)GetDriverFromElement(element);
            ex.ExecuteScript("arguments[0].click();", element);
        }

        private static IWebDriver GetDriverFromElement(IWebElement element)
        {
            if (element.GetType().ToString() == "OpenQA.Selenium.Support.PageObjects.WebElementProxy")
            {
                var propertyInfo = element.GetType().GetProperty("WrappedElement");
                if (propertyInfo != null)
                    return ((IWrapsDriver)propertyInfo.GetValue(element, null)).WrappedDriver;
            }
            return ((IWrapsDriver)element).WrappedDriver;
        }

        public static void ScrollIntoView(this IWebElement element)
        {
            if (element == null)
                throw new ApplicationException("Can't scroll an element into view that doesn't exist, element == null");

            ((IJavaScriptExecutor)GetDriverFromElement(element)).ExecuteScript("arguments[0].scrollIntoView()", element);
        }
    }
}
