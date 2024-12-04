using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using MediatR;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Handlers
{
    public class ProdutoFindByIdHandler : IRequestHandler<ProdutoFindByIdCommand, ModelResult>
    {
        private readonly IProdutoService _service;

        public ProdutoFindByIdHandler(IProdutoService service)
        {
            _service = service;
        }

        public async Task<ModelResult> Handle(ProdutoFindByIdCommand command, CancellationToken cancellationToken = default)
        {
            return await _service.FindByIdAsync(command.Id);
        }
    }
}
