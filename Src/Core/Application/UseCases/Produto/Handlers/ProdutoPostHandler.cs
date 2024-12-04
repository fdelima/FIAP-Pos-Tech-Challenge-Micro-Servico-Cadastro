using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Handlers
{
    public class ProdutoPostHandler : IRequestHandler<ProdutoPostCommand, ModelResult>
    {
        private readonly IProdutoService _service;

        public ProdutoPostHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoPostCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.InsertAsync(command.Entity, command.BusinessRules);
        }
    }
}
