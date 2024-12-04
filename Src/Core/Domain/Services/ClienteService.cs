using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Services
{
    public class ClienteService : BaseService<Cliente>
    {
        /// <summary>
        /// Lógica de negócio referentes ao cliente.
        /// </summary>
        /// <param name="gateway">Gateway de cliente a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        public ClienteService(IGateways<Cliente> gateway, IValidator<Cliente> validator)
            : base(gateway, validator)
        {
        }

        /// <summary>
        /// Regras para inserção do cliente
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Cliente entity, string[]? businessRules = null)
        {
            entity.IdCliente = entity.IdCliente.Equals(default) ? Guid.NewGuid() : entity.IdCliente;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
