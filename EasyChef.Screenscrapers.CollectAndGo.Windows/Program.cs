using EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows
{
    public static class Config
    {
        public const string API_URL = "http://localhost:63262/";
    }

    class Program
    {
        static void Main(string[] args)
        {
            var y = new ScanCategoriesConsumer();
            y.Consume();

            var x = new ScanProductsForCategoryConsumer();
            x.Consume().Wait();

        }
    }
}
