using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;

public partial class Dispositivo : IDomainEntity
{
    /// <summary>
    /// Retorna a regra de validação a ser utilizada na inserção.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> InsertDuplicatedRule()
    {
        return x => ((Dispositivo)x).Identificador.Trim().Equals(Identificador);
    }

    /// <summary>
    /// Retorna a regra de validação a ser utilizada na atualização.
    /// </summary>
    public Expression<Func<IDomainEntity, bool>> AlterDuplicatedRule()
    {
        return x => !((Dispositivo)x).IdDispositivo.Equals(IdDispositivo) &&
                    ((Dispositivo)x).Identificador.Trim().Equals(Identificador);
    }

    public Guid IdDispositivo { get; set; }

    public string Identificador { get; set; } = null!;

    public string? Modelo { get; set; }

    public string? Serie { get; set; }

}
