using EasyChef.Screenscrapers.CollectAndGo.Pages;
using EasyChef.Shared.Messages;
using EasyChef.Shared.Models;
using EasyChef.Shared.RestClients;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EasyChef.Screenscrapers.CollectAndGo.SeleniumTasks
{
    public class AddItemsToShoppingCartConsumer : SeleniumTask<AddItemsToShoppingCartRequest>, IConsumer<AddItemsToShoppingCartRequest>
    {
        async Task IConsumer<AddItemsToShoppingCartRequest>.Consume(ConsumeContext<AddItemsToShoppingCartRequest> context)
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
                await context.CompleteTask;
                Driver.Close();
                Driver.Dispose();
            }
        }
    }
}
