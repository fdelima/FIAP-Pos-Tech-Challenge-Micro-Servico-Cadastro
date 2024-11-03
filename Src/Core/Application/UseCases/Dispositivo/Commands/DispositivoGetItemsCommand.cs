using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Commands
{
    internal class DispositivoGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Dispositivo>>
    {
        public DispositivoGetItemsCommand(IPagingQueryParam filter, Expression<Func<Domain.Entities.Dispositivo, object>> sortProp)
        {
            Filter = filter;
            SortProp = sortProp;
        }

        public DispositivoGetItemsCommand(IPagingQueryParam filter,
            Expression<Func<Domain.Entities.Dispositivo, bool>> expression, Expression<Func<Domain.Entities.Dispositivo, object>> sortProp)
            : this(filter, sortProp)
        {
            Expression = expression;
        }

        public IPagingQueryParam Filter { get; }
        public Expression<Func<Domain.Entities.Dispositivo, bool>> Expression { get; }

        public Expression<Func<Domain.Entities.Dispositivo, object>> SortProp { get; }
    }
}