using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages;
using EasyChef.Shared.Messages;
using EasyChef.Shared.RestClients;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class AddItemsToShoppingCartConsumer : SeleniumTask
    {
        public void Consume()
        {
            try
            {
                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.Home);

                
                var shoppingCartRestClient = new ShoppingCartRestClient(new HttpClient(), Config.API_URL);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Driver.Close();
                Driver.Dispose();
            }
        }
    }
}
