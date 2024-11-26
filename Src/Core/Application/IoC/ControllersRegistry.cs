using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    public static class ControllersRegistry
    {
        public static void RegisterAppControllers(this IServiceCollection services)
        {
            //Controlles
            services.AddScoped(typeof(IController<Domain.Entities.Cliente>), typeof(ClienteController));
            services.AddScoped(typeof(IController<Domain.Entities.Dispositivo>), typeof(DispositivoController));
            services.AddScoped(typeof(IProdutoController), typeof(ProdutoController));
        }
    }
}