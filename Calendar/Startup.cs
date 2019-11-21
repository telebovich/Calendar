using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Calendar.Data;
using Calendar.Services;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.Extensions.Hosting;

namespace Calendar
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                /*
                 * This lambda determines whether user consent for non-essential 
                 * cookies is needed for a given request.
                 */
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddControllersWithViews();
            services.AddRazorPages()
                .AddRazorPagesOptions(options =>
                {
                    options.Conventions
                        .AddPageRouteModelConvention("/Calendar", model =>
                        {
                            var selectorCount = model.Selectors.Count;
                            for (var i = 0; i < selectorCount; i++)
                            {
                                var selector = model.Selectors[i];
                                model.Selectors.Add(new SelectorModel
                                {
                                    AttributeRouteModel = new AttributeRouteModel
                                    {
                                        Template = AttributeRouteModel.CombineTemplates(
                                            selector.AttributeRouteModel.Template,
                                            "{year:int}/{month:int}")
                                    }
                                });
                            }
                        });
                    options.Conventions
                        .AddPageRouteModelConvention("/SelectYear", model =>
                        {
                            var selectorCount = model.Selectors.Count;
                            for (var i = 0; i < selectorCount; i++)
                            {
                                var selector = model.Selectors[i];
                                model.Selectors.Add(new SelectorModel
                                {
                                    AttributeRouteModel = new AttributeRouteModel
                                    {
                                        Template = AttributeRouteModel.CombineTemplates(
                                            selector.AttributeRouteModel.Template,
                                            "{decadeBeginYear:int}-{decadeEndYear:int}")
                                    }
                                });
                            }
                        });
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            // Register no-op EmailSender used by account confirmation and password reset during development
            // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=532713
            services.AddSingleton<IEmailSender, EmailSender>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
