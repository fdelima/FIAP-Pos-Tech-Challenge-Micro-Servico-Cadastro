using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;

public partial class Cliente : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((Cliente)x).Cpf.Equals(Cpf);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((Cliente)x).IdCliente.Equals(IdCliente) &&
                    ((Cliente)x).Cpf.Equals(Cpf);
    }

    public Guid IdCliente { get; set; }

    public string Nome { get; set; } = null!;

    public string Email { get; set; } = null!;

    public long Cpf { get; set; }

}
