using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using WebData.Models;

namespace WebData
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
            var stringConnectDB = Configuration.GetConnectionString("WebDataDB");
            services.AddDbContext<XtbDbContext>(optionsAction => optionsAction.UseSqlServer(stringConnectDB));
            services.AddSingleton<HtmlEncoder>(HtmlEncoder.Create(allowedRanges: new[] { UnicodeRanges.All }));
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddMemoryCache();
            services.AddSession(Options =>
            {
                Options.Cookie.HttpOnly= true;
                Options.Cookie.IsEssential = true;
            });

            services.AddHttpContextAccessor();
            services.AddAuthentication("CookieAuthentication")
                .AddCookie("CookieAuthentication", configureOptions =>
                {
                    configureOptions.Cookie.Name = "UserLoginCookie";
                    configureOptions.ExpireTimeSpan = TimeSpan.FromDays(1);
                    configureOptions.LoginPath = "/";
                    configureOptions.LogoutPath = "/dang-xuat.html";
                    configureOptions.AccessDeniedPath = "/404.html";
                });
            services.ConfigureApplicationCookie(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.Cookie.HttpOnly = true;
                OptionsBuilderConfigurationExtensions.Cookie.Expiration = TimeSpan.FromDays(150);
                OptionsBuilderConfigurationExtensions.ExpireTimeSpan = TimeSpan.FromDays(150);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSession();
            app.UseResponseCaching();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
