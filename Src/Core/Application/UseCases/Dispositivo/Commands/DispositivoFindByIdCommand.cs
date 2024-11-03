using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Commands
{
    internal class DispositivoFindByIdCommand : IRequest<ModelResult>
    {
        public DispositivoFindByIdCommand(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}