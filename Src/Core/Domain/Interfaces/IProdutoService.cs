using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces
{
    public interface IProdutoService : IService<Produto>
    {
        /// <summary>
        /// Retorna as categorias dos produtos
        /// </summary>
        public Task<PagingQueryResult<KeyValuePair<short, string>>> GetCategoriasAsync();
    }
}
