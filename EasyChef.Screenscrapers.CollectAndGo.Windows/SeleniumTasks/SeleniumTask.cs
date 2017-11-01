using System;
using OpenQA.Selenium;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public abstract class SeleniumTask
    {
        public IWebDriver Driver { get; set; }

        public T Page<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Driver);
        }
    }
}
