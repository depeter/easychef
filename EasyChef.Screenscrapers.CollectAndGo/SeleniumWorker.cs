using EasyChef.Shared.Infrastructure;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Threading.Tasks;

namespace EasyChef.Screenscrapers.CollectAndGo
{

    public class SeleniumWorker<TRequest> where TRequest : MessageBusMessage
    {
        public IWebDriver Driver { get; set; }

        public SeleniumWorker()
        {
            Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory);
        }

        public virtual TRequest Start(TRequest message) {
            return null;
        }

        public T Page<T>()
        {
            return (T)Activator.CreateInstance(typeof(T), Driver);
        }
    }
}