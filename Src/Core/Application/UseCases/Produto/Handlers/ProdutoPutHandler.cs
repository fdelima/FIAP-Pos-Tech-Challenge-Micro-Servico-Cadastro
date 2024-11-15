using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Handlers
{
    public class ProdutoPutHandler : IRequestHandler<ProdutoPutCommand, ModelResult>
    {
        private readonly IProdutoService _service;

        public ProdutoPutHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoPutCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.UpdateAsync(command.Entity, command.BusinessRules);
        }
    }
}
