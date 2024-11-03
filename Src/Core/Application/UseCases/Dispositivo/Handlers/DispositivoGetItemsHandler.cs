using FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Commands;
using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Handlers
{
    internal class DispositivoGetItemsHandler : IRequestHandler<DispositivoGetItemsCommand, PagingQueryResult<Domain.Entities.Dispositivo>>
    {
        private readonly IService<Domain.Entities.Dispositivo> _service;

        public DispositivoGetItemsHandler(IService<Domain.Entities.Dispositivo> service)
        {
            _service = service;
        }

        public async Task<PagingQueryResult<Domain.Entities.Dispositivo>> Handle(DispositivoGetItemsCommand command, CancellationToken cancellationToken = default)
        {
            if (command.Expression == null)
                return await _service.GetItemsAsync(command.Filter, command.SortProp);
            else
                return await _service.GetItemsAsync(command.Filter, command.Expression, command.SortProp);
        }
    }
}
