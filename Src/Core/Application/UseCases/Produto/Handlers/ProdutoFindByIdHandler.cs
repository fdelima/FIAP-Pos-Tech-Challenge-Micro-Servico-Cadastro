using FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Handlers
{
    internal class ProdutoFindByIdHandler : IRequestHandler<ProdutoFindByIdCommand, ModelResult>
    {
        private readonly IProdutoService _service;

        public ProdutoFindByIdHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
