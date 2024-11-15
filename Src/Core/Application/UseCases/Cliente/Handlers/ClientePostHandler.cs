using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Handlers
{
    public class ClientePostHandler : IRequestHandler<ClientePostCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Cliente> _service;

        public ClientePostHandler(IService<Domain.Entities.Cliente> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClientePostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
