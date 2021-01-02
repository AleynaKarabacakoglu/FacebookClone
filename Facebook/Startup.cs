using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Dtos;
using Core.Services.Concrete;
using Core.Services.Interfaces;
using Domain.Context;
using Domain.Entities.Concrete;
using Facebook.CustomValidation;
using Facebook.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Facebook
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
            
            services.AddDbContext<FbContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionStr"]);
            });
           
            services.AddIdentity<User,IdentityRole<int>>().AddEntityFrameworkStores<FbContext>().AddDefaultTokenProviders();
            services.Configure<IdentityOptions>
            (opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequiredLength = 4;
                opts.User.AllowedUserNameCharacters = "abcçdefghijklmnoöpqrstuüvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";               
                opts.Password.RequireUppercase = false;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Lockout.AllowedForNewUsers = true;
               

                opts.Password.RequireDigit = true;

                opts.SignIn.RequireConfirmedAccount = false;
                opts.SignIn.RequireConfirmedEmail = false;

            });
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IConnectionService, ConnectionService>();
            services.AddTransient<IMessageService, MessageService>();
            services.AddControllersWithViews();
            services.AddSession();
            services.AddSignalR();
            services.ConfigureApplicationCookie(opt =>
            {
                opt.Cookie.HttpOnly = false; 
                opt.Cookie.Name = "Cookies";
                opt.Cookie.SameSite = SameSiteMode.Strict; 
                opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                opt.ExpireTimeSpan = TimeSpan.FromDays(20);

            });
        }
       
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
          
            app.UseAuthorization();
            app.UseAuthentication();
            //app.UseEndpoints(endpoints =>
            //{

            //    endpoints.MapHub<ChatHub>("/chathub");
            //});

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller=Account}/{action=SignUp}/{id?}");
            //});
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=SignUp}/{id?}");
                endpoints.MapHub<ChatHub>("/chathub");
            });

        }
    }
}
