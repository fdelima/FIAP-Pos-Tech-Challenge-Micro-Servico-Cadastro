using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands
{
    public class ProdutoGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Produto>>
    {
        public ProdutoGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.Produto, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public ProdutoGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.Produto, bool>> expression, Expression<Func<Domain.Entities.Produto, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.Produto, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.Produto, object>> SortProp { get; }
    }
}