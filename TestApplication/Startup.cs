using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StatusService.Handlers;
using TestApplication.Classes;
using TestApplication.Models;
using TestApplication.Repository;
using TestApplication.Services;

namespace TestApplication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ShowEnvironment();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Параметры приложения
            services.Configure<MainConfig>(Configuration.GetSection("MainConfig"));
            #endregion

            #region Главный сервис
            services.AddScoped<IMainService, MainService>();
            #endregion

            #region Репозиторий
            services.AddTransient<IRepository<Suggest>, SuggestRepository>();
            #endregion
        }

        void ShowEnvironment()
        {
            int maxLen = Convert.ToInt32(Configuration.AsEnumerable().ToList().OrderByDescending(c => c.Key.Length).FirstOrDefault().Key.Length);
            var s = Configuration.AsEnumerable().ToList().Select(
                c => new { Key = $"{c.Key}{(new string(' ', maxLen - c.Key.Length + 1))}= {c.Value}" }
                ).ToList();
            StringBuilder builder = new StringBuilder();
            foreach (var cat in s)
            {
                builder.Append(cat.ToString().TrimStart('{').TrimEnd('}')).Append("\n");
            }

            string secretPath = $"secrets{Path.DirectorySeparatorChar}appsettings.secrets.json";
            builder.Append(
                (File.Exists(secretPath) ? $" Key = Secret file exist" : " KEY = Secret file not exist")
                .TrimStart('{').TrimEnd('}')).Append("\n");

            Console.WriteLine($"\nENVIRONMENT\n{builder.ToString()}");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            #region Обработка любых ошибок на уровне контроллера
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            #endregion

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
