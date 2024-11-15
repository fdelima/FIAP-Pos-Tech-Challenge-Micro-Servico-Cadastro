using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Gateways;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.IoC
{
    internal static class GatewaysRegistry
    {
        public static void RegisterGateways(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IGateways<>), typeof(BaseGateway<>));
        }
    }
}