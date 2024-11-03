using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands
{
    internal class ProdutoFindByIdCommand : IRequest<ModelResult>
    {
        public ProdutoFindByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}