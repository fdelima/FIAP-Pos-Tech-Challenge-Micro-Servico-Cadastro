using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands
{
    public class DispositivoPutCommand : IRequest<ModelResult>
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