using EasyChef.Shared.Messages;
using EasyChef.Shared.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading.Tasks;

namespace EasyChef.Screenscrapers.CollectAndGo.Tasks
{
    public class FetchCurrentShoppingCart : SeleniumWorker<FetchCurrentShoppingCartRequest>
    {
        public FetchCurrentShoppingCart()
        {

        }

        public override FetchCurrentShoppingCartRequest Start(FetchCurrentShoppingCartRequest message)
        {
            
        }
    }
}
