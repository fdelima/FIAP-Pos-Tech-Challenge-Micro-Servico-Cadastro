using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using MediatR;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands
{
    public class ClienteGetItemsCommand : IRequest<PagingQueryResult<Domain.Entities.Cliente>>
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