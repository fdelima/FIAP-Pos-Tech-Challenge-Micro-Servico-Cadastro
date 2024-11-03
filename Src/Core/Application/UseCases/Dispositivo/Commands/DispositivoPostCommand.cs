using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Commands
{
    internal class DispositivoPostCommand : IRequest<ModelResult>
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