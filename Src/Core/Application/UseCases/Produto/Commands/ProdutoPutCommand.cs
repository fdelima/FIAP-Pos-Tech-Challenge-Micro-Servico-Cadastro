using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands
{
    internal class ProdutoPutCommand : IRequest<ModelResult>
    {
        public ProdutoPutCommand(Guid id, Domain.Entities.Produto entity,
            string[]? businessRules = null)
        {
            Id = id;
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public Domain.Entities.Produto Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}