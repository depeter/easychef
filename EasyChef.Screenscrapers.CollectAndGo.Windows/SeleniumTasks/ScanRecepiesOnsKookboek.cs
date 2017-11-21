using System;
using System.Net.Http;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Messages;
using EasyChef.Contracts.Shared.RestClients;
using EasyChef.Screenscrapers.CollectAndGo.Windows.Pages.OnsKookboek;
using MassTransit;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class ScanRecepiesOnsKookboekConsumer : SeleniumTask, IConsumer<ScanRecepiesOnsKookboekMessage>
    {
        public Task Consume(ConsumeContext<ScanRecepiesOnsKookboekMessage> context)
        {
            try
            {
                Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

                var recepyRestClient = new RecepyRestClient(new HttpClient(), Config.API_URL);

                var links = Page<IndexPage>().GetLinks();
                foreach (var link in links)
                {
                    var recepy = Page<RecepyPage>().GetRecepy(link);

                    recepyRestClient.Post(recepy).Wait();
                }
                
                context.Publish(new ScrapingJobResultMessage() { Success = true, MessageId = context.MessageId });

                return Console.Out.WriteLineAsync("Recepies scanned for OnsKookboek");
            }
            catch (Exception)
            {
                context.Publish(new ScrapingJobResultMessage() { Success = false, MessageId = context.MessageId });

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
