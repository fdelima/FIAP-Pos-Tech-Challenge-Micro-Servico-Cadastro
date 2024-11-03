using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Application.IoC
{
    internal static class DomainServicesRegistry
    {
        public static void RegisterDomainServices(this IServiceCollection services)
        {
            //Services
            services.AddScoped(typeof(IService<>), typeof(BaseService<>));
            services.AddScoped(typeof(IService<Domain.Entities.Cliente>), typeof(ClienteService));
            services.AddScoped(typeof(IService<Domain.Entities.Dispositivo>), typeof(DispositivoService));
            services.AddScoped(typeof(IProdutoService), typeof(ProdutoService));
        }
    }
}