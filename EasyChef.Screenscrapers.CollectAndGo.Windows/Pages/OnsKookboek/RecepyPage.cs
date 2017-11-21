using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using EasyChef.Contracts.Shared.Models;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Infrastructure;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.Pages.OnsKookboek
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

            if (_driver.TryFindElement(By.ClassName("easyrecipe")) != null)
            {
                return GetRecepyEasy();
            }
            return GetRecepyListBased();
        }

        private RecepyDTO GetRecepyListBased()
        {
            var recepy = new RecepyDTO();
            var ingredients = new List<IngredientDTO>();
            var preparationSteps = new List<RecepyPreparationDTO>();

            var ingredientElements = _driver.FindElements(By.CssSelector(".content article ul li"));
            foreach (var ingredientElement in ingredientElements)
            {
                ingredients.Add(new IngredientDTO()
                {
                    Text = ingredientElement.Text
                });
            }
            recepy.Ingredients = ingredients;

            var stepsElements = _driver.FindElements(By.CssSelector(".content article ol li"));
            foreach (var stepElement in stepsElements)
            {
                preparationSteps.Add(new RecepyPreparationDTO()
                {
                    Explanation = stepElement.Text,
                    Step = stepsElements.IndexOf(stepElement) + 1
                });
            }
            recepy.RecepyPreparations = preparationSteps;

            _driver.ScrollToBottomSlowly(10, 100);

            var image = _driver.TryWaitUntilElementVisible(By.CssSelector(".content p img[data-lazy-loaded='true']"), 5000);
            if (!string.IsNullOrWhiteSpace(image?.GetAttribute("src")))
            {
                recepy.Base64Image = SaveImageFromUrlToBase64(image.GetAttribute("src"));
            }

            var descriptionElements = _driver.FindElements(By.CssSelector(".entry-content p"));
            foreach (var descriptionElement in descriptionElements)
            {
                if(descriptionElement.Text.Split(' ').Length > 1)
                    recepy.Description += descriptionElement.Text + "\r\n\r\n";
            }

            var titleElement = _driver.TryFindElement(By.CssSelector(".content article .entry-title"));
            if (titleElement != null)
                recepy.Title = titleElement.Text;

            return recepy;
        }

        private RecepyDTO GetRecepyEasy()
        {
            var recepy = new RecepyDTO();
            var ingredients = new List<IngredientDTO>();
            var preparationSteps = new List<RecepyPreparationDTO>();

            var ingredientElements = _driver.FindElements(By.CssSelector(".content ul li"));
            foreach (var ingredientElement in ingredientElements)
            {
                ingredients.Add(new IngredientDTO()
                {
                    Text = ingredientElement.Text
                });
            }
            recepy.Ingredients = ingredients;

            var stepsElements = _driver.FindElements(By.CssSelector(".content ol li"));
            foreach (var stepElement in stepsElements)
            {
                preparationSteps.Add(new RecepyPreparationDTO()
                {
                    Explanation = stepElement.Text,
                    Step = stepsElements.IndexOf(stepElement) + 1
                });
            }
            recepy.RecepyPreparations = preparationSteps;

            _driver.ScrollToBottomSlowly(10, 100);

            var image = _driver.TryWaitUntilElementVisible(By.CssSelector(".content p img[data-lazy-loaded='true']"), 5000);
            if (!string.IsNullOrWhiteSpace(image?.GetAttribute("src")))
            {
                recepy.Base64Image = SaveImageFromUrlToBase64(image.GetAttribute("src"));
            }

            var descriptionElements = _driver.FindElements(By.CssSelector(".entry-content p"));
            foreach (var descriptionElement in descriptionElements)
            {
                recepy.Description += descriptionElement.Text + "\r\n\r\n";
            }

            var cookingDurationElement = _driver.TryFindElement(By.CssSelector("time[itemprop='cookTime']"));
            if (cookingDurationElement != null)
                recepy.CookingDuration = cookingDurationElement.Text;

            var prepDurationElement = _driver.TryFindElement(By.CssSelector("time[itemprop='prepTime']"));
            if (prepDurationElement != null)
                recepy.WorkDuration = prepDurationElement.Text;

            var totalDurationElement = _driver.TryFindElement(By.CssSelector("time[itemprop='totalTime']"));
            if (totalDurationElement != null)
                recepy.TotalDuration = totalDurationElement.Text;

            var titleElement = _driver.TryFindElement(By.CssSelector(".content article .entry-title"));
            if (titleElement != null)
                recepy.Title = titleElement.Text;


            return recepy;
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
