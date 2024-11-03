using FIAP.Pos.Tech.Challenge.Domain;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands
{
    internal class ProdutoGetCategoriasCommand : IRequest<PagingQueryResult<KeyValuePair<short, string>>>
    {
    }
}