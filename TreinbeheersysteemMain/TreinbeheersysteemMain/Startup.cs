using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreinbeheersysteemMain.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Treinbeheersysteem.DAL.Model;
using TreinbeheersysteemMain.Authentication;
using Treinbeheersysteem.DAL.Contexts;
using Treinbeheersysteem.BLL.Repositories;

namespace TreinbeheersysteemMain
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
            // Sql contexts
            services.AddScoped<IPerronContext, MSSQLPerronContext>();
            services.AddScoped<IStationContext, MSSQLStationContext>();
            services.AddScoped<ITicketContext, MSSQLTicketContext>();
            services.AddScoped<ITrajectContext, MSSQLTrajectContext>();
            services.AddScoped<ITreinContext, MSSQLTreinContext>();
            services.AddScoped<IVerbindingContext, MSSQLVerbindingContext>();
            services.AddScoped<IWagonContext, MSSQLWagonContext>();

            //services.AddSingleton<IAccountContext, MemoryAccountContext>();
            //services.AddSingleton<IPerronContext, MemoryPerronContext>();
            //services.AddSingleton<IStationContext, MemoryStationContext>();
            //services.AddSingleton<ITicketContext, MemoryTicketContext>();
            //services.AddSingleton<ITrajectContext, MemoryTrajectContext>();
            //services.AddSingleton<ITreinContext, MemoryTreinContext>();
            //services.AddSingleton<IVerbindingContext, MemoryVerbindingContext>();
            //services.AddSingleton<IWagonContext, MemoryWagonContext>();
            //Add test data into static lists

            //Repositories
            services.AddScoped<PerronRepository>();
            services.AddScoped<StationRepository>();
            services.AddScoped<TicketRepository>();
            services.AddScoped<TrajectRepository>();
            services.AddScoped<TreinRepository>();
            services.AddScoped<VerbindingRepository>();
            services.AddScoped<WagonRepository>();
            services.AddScoped<AccountRepository>();

            services.AddTransient<IUserStore<Account>, MSSQLAccountContext>();
            services.AddTransient<IRoleStore<Rol>, MSSQLRolContext>();
            services.AddIdentity<Account, Rol>().AddDefaultTokenProviders();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
