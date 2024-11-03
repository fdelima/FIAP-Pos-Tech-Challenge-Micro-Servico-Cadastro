using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Domain.Entities;

public partial class Produto : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((Produto)x).Nome.Equals(Nome);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((Produto)x).IdProduto.Equals(IdProduto) &&
                     ((Produto)x).Nome.Equals(Nome);
    }
    public Guid IdProduto { get; set; }

    public string Nome { get; set; } = null!;

    public decimal Preco { get; set; }

    public string Descricao { get; set; } = null!;

    public string Categoria { get; set; }

    public virtual ICollection<ProdutoImagens> ProdutoImagens { get; set; } = new List<ProdutoImagens>();

}
