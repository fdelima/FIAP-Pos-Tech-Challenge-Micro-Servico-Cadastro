using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Services
{
    public class DispositivoService : BaseService<Dispositivo>
    {
        /// <summary>
        /// Lógica de negócio referentes ao dispositivo.
        /// </summary>
        /// <param name="gateway">Gateway de dispositivo a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        public DispositivoService(IGateways<Dispositivo> gateway, IValidator<Dispositivo> validator)
            : base(gateway, validator)
        {
        }

        /// <summary>
        /// Regras para inserção do dispositivo
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Dispositivo entity, string[]? businessRules = null)
        {
            entity.IdDispositivo = entity.IdDispositivo.Equals(default) ? Guid.NewGuid() : entity.IdDispositivo;
            return await base.InsertAsync(entity, businessRules);
        }
    }
}
