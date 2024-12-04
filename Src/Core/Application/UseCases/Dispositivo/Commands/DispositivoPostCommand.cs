using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands
{
    public class DispositivoPostCommand : IRequest<ModelResult>
    {
        public DispositivoPostCommand(Domain.Entities.Dispositivo entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Dispositivo Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}