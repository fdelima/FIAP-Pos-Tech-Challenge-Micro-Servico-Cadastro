using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions
{
    /// <summary>
    /// Extensão da model para informar os campos de validação.
    /// </summary>
    public static class ProdutoExtension
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public static Expression<Func<Produto, bool>> ConsultRule(this PagingQueryParam<Produto> param)
        {
            return x => (x.IdProduto.Equals(param.ObjFilter.IdProduto) || param.ObjFilter.IdProduto.Equals(default)) &&
                        (x.Nome.Contains(param.ObjFilter.Nome) || string.IsNullOrWhiteSpace(param.ObjFilter.Nome)) &&
                        (x.Preco.Equals(param.ObjFilter.Preco) || param.ObjFilter.Preco.Equals(default)) &&
                        (x.Descricao.Contains(param.ObjFilter.Descricao) || string.IsNullOrWhiteSpace(param.ObjFilter.Descricao)) &&
                        (x.Categoria.Equals(param.ObjFilter.Categoria.ToString()) || param.ObjFilter.Categoria.Equals(default));
        }

        /// <summary>
        /// Retorna a propriedade a ser ordenada
        /// </summary>
        public static Expression<Func<Produto, object>> SortProp(this PagingQueryParam<Produto> param)
        {
            switch (param?.SortProperty?.ToLower())
            {
                case "idProduto":
                    return fa => fa.IdProduto;
                case "preco":
                    return fa => fa.Preco;
                case "descricao":
                    return fa => fa.Descricao;
                case "categoria":
                    return fa => fa.Categoria;
                default: return fa => fa.Nome;
            }
        }
    }
}
