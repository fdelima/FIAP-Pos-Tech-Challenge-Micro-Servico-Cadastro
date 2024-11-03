using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Commands
{
    internal class ClientePostCommand : IRequest<ModelResult>
    {
        public ClientePostCommand(Domain.Entities.Cliente entity,
            string[]? businessRules = null)
        {
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Domain.Entities.Cliente Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}