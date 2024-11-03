using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Cliente.Commands
{
    internal class ClienteFindByIdCommand : IRequest<ModelResult>
    {
        public ClienteFindByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}