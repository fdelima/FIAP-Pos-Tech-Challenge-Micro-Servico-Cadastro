using FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Handlers
{
    internal class ProdutoPutHandler : IRequestHandler<ProdutoPutCommand, ModelResult>
    {
        private readonly IProdutoService _service;

        public ProdutoPutHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoPutCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command.Entity, command.BusinessRules);
        }
    }
}
