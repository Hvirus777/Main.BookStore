using Main.BookStore.Data;
using Main.BookStore.Helper;
using Main.BookStore.Models;
using Main.BookStore.Repository;
using Main.BookStore.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Main.BookStore
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BookStoreContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreContext>();

            //updating Password Policy
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });

            //Custom Login Path if user is not login and accessing unauthorize pages it will be redirected to login page
            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = configuration["Application:LoginPath"];
            });

            //To use MVC controller and views service.
            services.AddControllersWithViews();

            // this should only work in Development Env, not in Testing or production or staging so to do that we use #If debug statement

#if DEBUG

            services.AddRazorPages().AddRazorRuntimeCompilation();

            /* Uncomment this code below to disable Client SIde Validation */

            //    .AddViewOptions(options => {

            //    options.HtmlHelperOptions.ClientValidationEnabled = false;
            //});
#endif
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILanguageRepository, LanguageRepository>();
            services.Configure<NewBookAlertConfig>(configuration.GetSection("NewBookAlert"));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IUserService, UserService>();

            //to use custom claims
            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            /*
             
             TO use Static file from folders other than wwwroot folder
            
              app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "MyStaticFiles")),
            //    RequestPath= "/MyStaticFiles"

            //});*/
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //   endpoints.MapDefaultControllerRoute();

                //endpoints.MapControllerRoute(
                //        name:"Default",
                //        pattern: "bookApp/{controller=Home}/{action=Index}/{id?}"
                //);


                //Conventional Routing below

                //endpoints.MapControllerRoute(
                //        name: "Default",
                //        pattern: "about-us",
                //        defaults : new { controller = "Home",action="AboutUs" }

                //);

                endpoints.MapControllers();


                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });



            //app.Use(async (context, next) =>
            //{

            //    await context.Response.WriteAsync("Yoo!");
            //   await next();

            //});

            //app.Use(async (context, next) =>
            //{

            //    await context.Response.WriteAsync("Yoo! Yoo! 2x");

            //});

        }
    }
}
