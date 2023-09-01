using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProEvento.Aplicacao.Helpers;
using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEvento.Aplicacao.Servicos;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using ProEvento.Dominio.Servicos;
using ProEvento.Infraestrutura.Repositorio;
using ProEventos.Api.Data;
using System;
using System.IO;

namespace ProEventos.Api
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
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<ProEventoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Connection")));

            services.AddAutoMapper(typeof(ProEventosProfile));

            // App
            services.AddScoped<IAppEvento, AppEvento>();
            services.AddScoped<IAppLote, AppLote>();

            // Servico
            services.AddScoped<IServicoEvento, ServicoEvento>();
            services.AddScoped<IServicoPalestrantes, ServicoPalestrantes>();
            services.AddScoped<IServicoLote, ServicoLote>();

            // Repositorio
            services.AddScoped<IRepositorioEventos, RepositorioEvento>();
            services.AddScoped<IRepositorioPalestrantes, RepositorioPalestrantes>();
            services.AddScoped<IRepositorioLote, RepositorioLote>();

            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProEventos.Api v1"));
            }
            else
            {
                app.UseHttpsRedirection();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseCors(x =>
                x.AllowAnyHeader()
                 .AllowAnyMethod()
                 .AllowAnyOrigin()
            );

            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Resources")),
                RequestPath = new PathString("/Resources")
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}