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
    public class ScanCategoriesConsumer : SeleniumTask<ScanCategoriesRequest>, IConsumer<ScanCategoriesRequest>
    {
        async Task IConsumer<ScanCategoriesRequest>.Consume(ConsumeContext<ScanCategoriesRequest> context)
        {
            try
            {
                Page<LoginPage>().Login();
                Page<NavigationPage>().NavigateTo(Navigation.ShoppingPage);

                var parentCategories = Page<ShoppingPage>().ListAllParentCategories();

                foreach (var parentCategory in parentCategories)
                {
                    Page<ShoppingPage>().OpenCategory(parentCategory);

                    parentCategory.Children = Page<ShoppingPage>().ListAllChildCategories(parentCategory);
                }

                

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
