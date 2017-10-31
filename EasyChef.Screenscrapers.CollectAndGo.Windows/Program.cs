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
            //var y = new ScanCategoriesConsumer();
            //y.Consume();

            //var x = new ScanProductsForCategoryConsumer();
            //x.Consume().Wait();

            var z = new SyncCurrentShoppingCartConsumer();
            z.Consume().Wait();
        }
    }
}
