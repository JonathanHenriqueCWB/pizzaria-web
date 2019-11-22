using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.DAL;
using Repository.DATA;

namespace PizzariaWeb
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
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //Configurando injeção de dependência
            services.AddScoped<FuncionarioDAO>();
            services.AddScoped<CargoDAO>();
            services.AddScoped<TamanhoDAO>();
            services.AddScoped<SaborDAO>();
            services.AddScoped<BebidaDAO>();
            services.AddScoped<UsuarioDAO>();

            //Autenticação de usuario IdentityUser
            services.AddIdentity<UsuarioLogado, IdentityRole>().AddEntityFrameworkStores<PizzariaContext>().AddDefaultTokenProviders();
            services.ConfigureApplicationCookie(option => {
                option.LoginPath = "/Usuario/Login";
                option.AccessDeniedPath = "/Usuario/AcessoNegado";
            });

            //Configuração do contexto para as migrações
            services.AddDbContext<PizzariaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("PizzariaConnection")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
