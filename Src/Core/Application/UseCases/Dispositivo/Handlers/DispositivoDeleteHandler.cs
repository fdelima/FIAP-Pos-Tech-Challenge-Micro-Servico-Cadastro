using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Handlers
{
    public class DispositivoDeleteHandler : IRequestHandler<DispositivoDeleteCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Dispositivo> _service;

        public DispositivoDeleteHandler(IService<Domain.Entities.Dispositivo> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(DispositivoDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
