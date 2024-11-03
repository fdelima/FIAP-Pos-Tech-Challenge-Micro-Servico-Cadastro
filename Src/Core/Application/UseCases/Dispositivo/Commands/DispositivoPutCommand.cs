using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Commands
{
    internal class DispositivoPutCommand : IRequest<ModelResult>
    {
        public DispositivoPutCommand(Guid id, Domain.Entities.Dispositivo entity,
            string[]? businessRules = null)
        {
            Id = id;
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public Domain.Entities.Dispositivo Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}