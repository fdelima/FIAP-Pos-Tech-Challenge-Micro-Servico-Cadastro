using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Handlers
{
    public class ProdutoGetCategoriasHandler : IRequestHandler<ProdutoGetCategoriasCommand, PagingQueryResult<KeyValuePair<short, string>>>
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
