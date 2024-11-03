using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands
{
    internal class ProdutoPostCommand : IRequest<ModelResult>
    {
        public ProdutoPostCommand(Domain.Entities.Produto entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Produto Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}