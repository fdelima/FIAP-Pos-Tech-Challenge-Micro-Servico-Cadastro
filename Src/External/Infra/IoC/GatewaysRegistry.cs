using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Gateways;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    internal static class GatewaysRegistry
    {
        public static void RegisterGateways(this IServiceCollection services)
        {
            //Repositories
            services.AddScoped(typeof(IGateways<>), typeof(BaseGateway<>));
        }
    }
}