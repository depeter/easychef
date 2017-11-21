using System;
using AutoMapper;
using EasyChef.Backend.Rest.Repositories;
using EasyChef.Contracts.Shared.RequestResponse;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EasyChef.Backend.Rest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddAutoMapper(typeof(Startup));

            services.AddDbContext<DBContext>((builder) =>
            {
                builder.UseSqlite("Data Source=EasyChef.db");
            });
            services.AddTransient<ICategoryRepo, CategoryRepo>();
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<IShoppingCartRepo, ShoppingCartRepo>();
            services.AddTransient<IShoppingCartProductRepo, ShoppingCartProductRepo>();
            services.AddTransient<IUserRepo, UserRepo>();
            services.AddTransient<IRecepyRepo, RecepyRepo>();
            services.AddTransient<IIngredientRepo, IngredientRepo>();
            services.AddTransient<IRecepyPreparationRepo, RecepyPreparationRepo>();

            services.AddCors();

            // service bus dependencies
            var address = new Uri("rabbitmq://localhost");
            var timeout = TimeSpan.FromSeconds(30);

            services.AddSingleton((x) => BusConfiguration(address));

            services.AddTransient<IRequestClient<VerifyLogin, VerifyLoginResponse>>((x) =>
                new MessageRequestClient<VerifyLogin, VerifyLoginResponse>(x.GetService<IBusControl>(), new Uri(address.OriginalString + "/scrapingjobs_queue"), timeout));
        }

        private static IBusControl BusConfiguration(Uri address)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(sbc =>
            {
                sbc.Host(address, h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });
            });
            bus.Start();
            return bus;
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors((x) =>
            {
                //x.WithOrigins("http://localhost:60001");
                x.AllowAnyOrigin()
                 .AllowAnyMethod()
                 .AllowAnyHeader()
                 .AllowCredentials();
            });
            app.UseMvc();

        }
    }
}
