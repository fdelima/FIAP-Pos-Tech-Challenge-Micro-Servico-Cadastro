using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Handlers
{
    public class ProdutoDeleteHandler : IRequestHandler<ProdutoDeleteCommand, ModelResult>
    {
        private readonly IProdutoService _service;

        public ProdutoDeleteHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoDeleteCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.DeleteAsync(command.Id, command.BusinessRules);
        }
    }
}
