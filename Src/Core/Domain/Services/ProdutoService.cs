using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.ValuesObject;
using FluentValidation;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Services
{
    public class ProdutoService : BaseService<Produto>, IProdutoService
    {
        /// <summary>
        /// Lógica de negócio referentes ao produto.
        /// </summary>
        /// <param name="gateway">Gateway de produto a ser injetado durante a execução</param>
        /// <param name="validator">abstração do validador a ser injetado durante a execução</param>
        public ProdutoService(IGateways<Produto> gateway, IValidator<Produto> validator)
            : base(gateway, validator) { }

        /// <summary>
        /// Regras para carregar o produtos e suas imagens.
        /// </summary>
        public async override Task<ModelResult> FindByIdAsync(Guid Id)
        {
            Produto? result = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.ProdutoImagens, x => x.IdProduto == Id);

            if (result == null)
                return ModelResultFactory.NotFoundResult<Produto>();

            return ModelResultFactory.SucessResult(result);
        }

        /// <summary>
        /// Regras para atualizar o produto e suas dependências.
        /// </summary>
        public async override Task<ModelResult> UpdateAsync(Produto entity, string[]? businessRules = null)
        {
            Produto? dbEntity = await _gateway.FirstOrDefaultWithIncludeAsync(x => x.ProdutoImagens, x => x.IdProduto == entity.IdProduto);

            if (dbEntity == null)
                return ModelResultFactory.NotFoundResult<Produto>();

            for (int i = 0; i < dbEntity.ProdutoImagens.Count; i++)
            {
                ProdutoImagens item = dbEntity.ProdutoImagens.ElementAt(i);
                if (!entity.ProdutoImagens.Any(x => x.IdProdutoImagem.Equals(item.IdProdutoImagem)))
                    dbEntity.ProdutoImagens.Remove(dbEntity.ProdutoImagens.First(x => x.IdProdutoImagem.Equals(item.IdProdutoImagem)));
            }

            for (int i = 0; i < entity.ProdutoImagens.Count; i++)
            {
                ProdutoImagens item = entity.ProdutoImagens.ElementAt(i);
                if (!dbEntity.ProdutoImagens.Any(x => x.IdProdutoImagem.Equals(item.IdProdutoImagem)))
                {
                    item.IdProdutoImagem = item.IdProdutoImagem.Equals(default) ? Guid.NewGuid() : item.IdProdutoImagem;
                    dbEntity.ProdutoImagens.Add(item);
                }
            }

            await _gateway.UpdateAsync(dbEntity, entity);
            return await base.UpdateAsync(dbEntity, businessRules);
        }

        /// <summary>
        /// Regras para Retornar as categorias dos produtos
        /// </summary>
        public Task<PagingQueryResult<KeyValuePair<short, string>>> GetCategoriasAsync()
        {
            List<KeyValuePair<short, string>> content = new List<KeyValuePair<short, string>>();

            foreach (enmProdutoCategoria value in Enum.GetValues<enmProdutoCategoria>())
                content.Add(new KeyValuePair<short, string>((short)value, value.ToString()));

            return Task.FromResult(new PagingQueryResult<KeyValuePair<short, string>>(content));

        }

        /// <summary>
        /// Regras para inserção do produto
        /// </summary>
        /// <param name="entity">Entidade</param>
        /// <param name="ValidatorResult">Validações já realizadas a serem adicionadas ao contexto</param>
        public override async Task<ModelResult> InsertAsync(Produto entity, string[]? businessRules = null)
        {
            entity.IdProduto = entity.IdProduto.Equals(default) ? Guid.NewGuid() : entity.IdProduto;

            foreach (ProdutoImagens item in entity.ProdutoImagens)
                item.IdProdutoImagem = item.IdProdutoImagem.Equals(default) ? Guid.NewGuid() : item.IdProdutoImagem;

            return await base.InsertAsync(entity, businessRules);
        }
    }
}
