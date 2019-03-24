using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBServerAPI.Infra;
using DBServerAPI.Infra.Interfaces;
using DBServerAPI.Services;
using DBServerAPI.Services.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DBServerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTransient<IRepositorioLancamentos, RepositorioLancamentos>()
                .AddTransient<IRepositorioClientes, RepositorioClientes>()
                .AddTransient<IFinanceiroService, FinanceiroService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

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
