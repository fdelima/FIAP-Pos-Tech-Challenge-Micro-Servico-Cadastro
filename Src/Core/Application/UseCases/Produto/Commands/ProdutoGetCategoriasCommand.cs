using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands
{
    public class ProdutoGetCategoriasCommand : IRequest<PagingQueryResult<KeyValuePair<short, string>>>
    {
    }
}