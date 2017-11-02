using System;
using System.Threading.Tasks;
using EasyChef.Contracts.Shared.Messages;
using EasyChef.Contracts.Shared.RequestResponse;
using EasyChef.Screenscrapers.CollectAndGo.Pages;
using MassTransit;
using OpenQA.Selenium.Chrome;

namespace EasyChef.Screenscrapers.CollectAndGo.Windows.SeleniumTasks
{
    public class VerifyLoginConsumer : SeleniumTask, IConsumer<VerifyLogin>

    {
        public Task Consume(ConsumeContext<VerifyLogin> context)
        {
            try
            {
                Driver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, new ChromeOptions { Proxy = null });

                if(!Page<LoginPage>().Login(context.Message.Login, context.Message.Password))
                    context.Respond(new VerifyLoginResponse() { Success = false });

                context.Respond(new VerifyLoginResponse() { Success = true });

                return Console.Out.WriteLineAsync("Verified login for user " + context.Message.Login);
            }
            catch (Exception)
            {
                context.Respond(new VerifyLoginResponse() { Success = false });

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
