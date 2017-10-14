using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public abstract class SeleniumTask<TRequest>
    {
        public IWebDriver Driver { get; set; }

        public SeleniumTask()
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });
        }

        public virtual TRequest Start(TRequest message)
        {
            return default(TRequest);
        }

        public T Page<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Driver);
        }
    }
}
