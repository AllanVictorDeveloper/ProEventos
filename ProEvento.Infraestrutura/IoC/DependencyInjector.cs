using Microsoft.Extensions.DependencyInjection;
using ProEvento.Aplicacao.Interfaces.Servicos;
using ProEvento.Aplicacao.Servicos;
using ProEvento.Dominio.Interfaces.Repositorios;
using ProEvento.Dominio.Interfaces.Servicos;
using ProEvento.Dominio.Servicos;
using ProEvento.Infraestrutura.Repositorio;

namespace ProEvento.Infraestrutura.IoC
{
    public static class DependencyInjector
    {
        public static void RegistrarDI(this IServiceCollection services)
        {
            // App
            services.AddScoped<IAppEvento, AppEvento>();
            services.AddScoped<IAppLote, AppLote>();
            services.AddScoped<IAppUsuario, AppUsuario>();

            // Servico
            services.AddScoped<IServicoEvento, ServicoEvento>();
            services.AddScoped<IServicoPalestrantes, ServicoPalestrantes>();
            services.AddScoped<IServicoLote, ServicoLote>();
            services.AddScoped<IServicoUsuario, ServicoUsuario>();

            // Repositorio
            services.AddScoped<IRepositorioEventos, RepositorioEvento>();
            services.AddScoped<IRepositorioPalestrantes, RepositorioPalestrantes>();
            services.AddScoped<IRepositorioLote, RepositorioLote>();
            services.AddScoped<IRepositorioUsuario, RepositorioUsuario>();
        }
    }
}