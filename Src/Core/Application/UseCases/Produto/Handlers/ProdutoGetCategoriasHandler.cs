using FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Handlers
{
    internal class ProdutoGetCategoriasHandler : IRequestHandler<ProdutoGetCategoriasCommand, PagingQueryResult<KeyValuePair<short, string>>>
    {
        private readonly IProdutoService _service;

        public ProdutoGetCategoriasHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<KeyValuePair<short, string>>> Handle(ProdutoGetCategoriasCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.GetCategoriasAsync();
        }
    }
}
