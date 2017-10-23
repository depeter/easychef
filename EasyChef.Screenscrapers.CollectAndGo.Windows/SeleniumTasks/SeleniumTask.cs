using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks
{
    public abstract class SeleniumTask<TRequest>
    {
        public IWebDriver Driver { get; set; }

        public SeleniumTask()
        {
            
        }

        public virtual TRequest Start(TRequest message)
        {
            return default(TRequest);
        }

        
    }
}
