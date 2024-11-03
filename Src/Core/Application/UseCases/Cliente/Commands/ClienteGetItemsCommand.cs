using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Commands
{
    internal class ClienteGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Cliente>>
    {
        public ClienteGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.Cliente, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public ClienteGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.Cliente, bool>> expression, Expression<Func<Domain.Entities.Cliente, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.Cliente, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.Cliente, object>> SortProp { get; }
    }
}