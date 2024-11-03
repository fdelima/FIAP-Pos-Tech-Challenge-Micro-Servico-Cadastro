using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Produto.Commands
{
    internal class ProdutoGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Produto>>
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