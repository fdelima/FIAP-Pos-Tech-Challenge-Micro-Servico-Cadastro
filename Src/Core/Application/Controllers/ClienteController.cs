using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using FluentValidation;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.Controllers
{
    /// <summary>
    /// Regras da aplicação referente ao cliente
    /// </summary>
    public class ClienteController : IController<Domain.Entities.Cliente>
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Domain.Entities.Cliente> _validator;

        public ClienteController(IMediator mediator, IValidator<Domain.Entities.Cliente> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        /// <summary>
        /// Valida a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public async Task<ModelResult> ValidateAsync(Domain.Entities.Cliente entity)
        {
            ModelResult ValidatorResult = new ModelResult(entity);

            FluentValidation.Results.ValidationResult validations = _validator.Validate(entity);
            if (!validations.IsValid)
            {
                ValidatorResult.AddValidations(validations);
                return ValidatorResult;
            }

            return await Task.FromResult(ValidatorResult);
        }

        /// <summary>
        /// Envia a entidade para inserção ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> PostAsync(Domain.Entities.Cliente entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Cliente");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ClientePostCommand command = new(entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para atualização ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="duplicatedExpression">Expressão para verificação de duplicidade.</param>
        public virtual async Task<ModelResult> PutAsync(Guid id, Domain.Entities.Cliente entity)
        {
            if (entity == null) throw new InvalidOperationException($"Necessário informar o Cliente");

            ModelResult ValidatorResult = await ValidateAsync(entity);

            if (ValidatorResult.IsValid)
            {
                ClientePutCommand command = new(id, entity);
                return await _mediator.Send(command);
            }

            return ValidatorResult;
        }

        /// <summary>
        /// Envia a entidade para deleção ao domínio
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> DeleteAsync(Guid id)
        {
            ClienteDeleteCommand command = new(id);
            return await _mediator.Send(command);
        }

        /// <summary>
        /// Retorna a entidade
        /// </summary>
        /// <param name="entity">Entidade</param>
        public virtual async Task<ModelResult> FindByIdAsync(Guid id)
        {
            ClienteFindByIdCommand command = new(id);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades
        /// </summary>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Cliente>> GetItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Cliente, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ClienteGetItemsCommand command = new(filter, sortProp);
            return await _mediator.Send(command);
        }


        /// <summary>
        /// Retorna as entidades que atendem a expressão de filtro 
        /// </summary>
        /// <param name="expression">Condição que filtra os itens a serem retornados</param>
        /// <param name="filter">filtro a ser aplicado</param>
        public virtual async ValueTask<PagingQueryResult<Domain.Entities.Cliente>> ConsultItemsAsync(IPagingQueryParam filter, Expression<Func<Domain.Entities.Cliente, bool>> expression, Expression<Func<Domain.Entities.Cliente, object>> sortProp)
        {
            if (filter == null) throw new InvalidOperationException("Necessário informar o filtro da consulta");

            ClienteGetItemsCommand command = new(filter, expression, sortProp);
            return await _mediator.Send(command);
        }

    }
}
