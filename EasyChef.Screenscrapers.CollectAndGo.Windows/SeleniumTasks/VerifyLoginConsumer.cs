using System;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Messages;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using MassTransit;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class VerifyLoginMessage
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class VerifyLoginConsumer : SeleniumTask, IConsumer<VerifyLoginMessage>

    {
        public Task Consume(ConsumeContext<VerifyLoginMessage> context)
        {
            try
            {
                Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

                if(!Page<LoginPage>().Login(context.Message.Email, context.Message.Password))
                    context.Publish(new ScrapingJobResultMessage() { Success = false, MessageId = context.MessageId });

                context.Publish(new ScrapingJobResultMessage() { Success = true, MessageId = context.MessageId });

                return Console.Out.WriteLineAsync("Verified login for user " + context.Message.Email);
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
