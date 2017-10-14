using EasyChef.Shared.Messages;
using MassTransit;
using System;
using System.Threading.Tasks;
using EasyChef.Shared.RestClients;
using System.Net.Http;
using EasyChef.Shared.Models;

namespace EasyChef.Screenscrapers.CollectAndGo
{
    public class FetchCurrentShoppingCartConsumer : SeleniumTask<FetchCurrentShoppingCartRequest>, IConsumer<FetchCurrentShoppingCartRequest>
    {
        async Task IConsumer<FetchCurrentShoppingCartRequest>.Consume(ConsumeContext<FetchCurrentShoppingCartRequest> context)
        {
            try
            {
                var shoppingCart = new ShoppingCart()
                {
                    UserId = 1
                };

                Page<LoginPage>().Login("peter.meir@gmail.com", "collect&go");
                Page<NavigationPage>().NavigateTo(Navigation.ShoppingCart);
                shoppingCart.Products = Page<ShoppingCartPage>().GetProducts();

                var shoppingCartRestClient = new ShoppingCartRestClient(new HttpClient(), "http://localhost:63262/");
                var result = await shoppingCartRestClient.Post(shoppingCart);
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
