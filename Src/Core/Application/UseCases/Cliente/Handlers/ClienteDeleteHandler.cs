using FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Handlers
{
    internal class ClienteDeleteHandler : IRequestHandler<ClienteDeleteCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Cliente> _service;

        public ClienteDeleteHandler(IService<Domain.Entities.Cliente> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClienteDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
