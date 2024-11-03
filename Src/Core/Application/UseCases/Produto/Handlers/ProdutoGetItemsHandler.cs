using FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Handlers
{
    internal class ProdutoGetItemsHandler : IRequestHandler<ProdutoGetItemsCommand, PagingQueryResult<Domain.Entities.Produto>>
    {
        private readonly IProdutoService _service;

        public ProdutoGetItemsHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Produto>> Handle(ProdutoGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
