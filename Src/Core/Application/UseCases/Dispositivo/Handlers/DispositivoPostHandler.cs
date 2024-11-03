﻿using FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Commands;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Application.UseCases.Dispositivo.Handlers
{
    internal class DispositivoPostHandler : IRequestHandler<DispositivoPostCommand, ModelResult>
    {
        private readonly IService<Domain.Entities.Dispositivo> _service;

        public DispositivoPostHandler(IService<Domain.Entities.Dispositivo> service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(DispositivoPostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
