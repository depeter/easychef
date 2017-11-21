using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Pages.LibelleLekker
{
    public class RecepyPage : Page
    {
        public RecepyPage(IWebDriver driver) : base(driver)
        {
        }

        public RecepyDTO GetRecepy(string link)
        {
            _driver.Navigate().GoToUrl(link);

            _driver.WaitUntilLoadingFinished();


        }

        private string SaveImageFromUrlToBase64(string url)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(url))
                    return null;

                using (WebClient client = new WebClient())
                {
                    var bytes = client.DownloadData(url);
                    return Convert.ToBase64String(bytes);
                }
            }
            catch (WebException)
            {
                return null;
            }
            catch (NotSupportedException)
            {
                return null;
            }
        }
    }
}
