﻿using AutoMapper;
using EasyChef.Backend.Rest.Repositories;
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();

        }
    }
}
