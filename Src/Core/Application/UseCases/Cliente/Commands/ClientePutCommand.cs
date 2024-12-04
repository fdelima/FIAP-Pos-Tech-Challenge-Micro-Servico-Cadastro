using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands
{
    public class ClientePutCommand : IRequest<ModelResult>
    {
        public ClientePutCommand(Guid id, Domain.Entities.Cliente entity,
            string[]? businessRules = null)
        {
            Id = id;
            Entity = entity;
            BusinessRules = businessRules;
        }

        public Guid Id { get; private set; }
        public Domain.Entities.Cliente Entity { get; private set; }
        public string[]? BusinessRules { get; private set; }
    }
}