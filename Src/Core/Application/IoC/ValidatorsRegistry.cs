using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Validator;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.IoC
{
    [ExcludeFromCodeCoverage(Justification = "Arquivo de configuração")]
    public static class ValidatorsRegistry
    {
        public static void RegisterValidators(this IServiceCollection services)
        {
            //TODO: Validators :: 3 - Adicione sua configuração aqui

            //Validators
            services.AddScoped(typeof(IValidator<Cliente>), typeof(ClienteValidator));
            services.AddScoped(typeof(IValidator<Dispositivo>), typeof(DispositivoValidator));
            services.AddScoped(typeof(IValidator<Produto>), typeof(ProdutoValidator));
        }
    }
}
