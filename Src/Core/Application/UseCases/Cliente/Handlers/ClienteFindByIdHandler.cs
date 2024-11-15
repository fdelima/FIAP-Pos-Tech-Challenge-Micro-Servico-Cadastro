using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Handlers
{
    public class ClienteFindByIdHandler : IRequestHandler<ClienteFindByIdCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Cliente> _service;

        public ClienteFindByIdHandler(IService<Domain.Entities.Cliente> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ClienteFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
