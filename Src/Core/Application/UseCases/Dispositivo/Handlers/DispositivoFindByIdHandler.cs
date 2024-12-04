using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Handlers
{
    public class DispositivoFindByIdHandler : IRequestHandler<DispositivoFindByIdCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Dispositivo> _service;

        public DispositivoFindByIdHandler(IService<Domain.Entities.Dispositivo> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(DispositivoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
