using FIAP.Pos.Tech.Challenge.Domain.Entities;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Domain.Extensions
{
    /// <summary>
    /// Extensão da model para informar os campos de validação.
    /// </summary>
    public static class ClienteExtension
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public static Expression<Func<Cliente, bool>> ConsultRule(this PagingQueryParam<Cliente> param)
        {
            return x => (x.IdCliente.Equals(param.ObjFilter.IdCliente) || param.ObjFilter.IdCliente.Equals(default)) &&
                        (x.Nome.Contains(param.ObjFilter.Nome) || string.IsNullOrWhiteSpace(param.ObjFilter.Nome)) &&
                        (x.Email.Contains(param.ObjFilter.Email) || string.IsNullOrWhiteSpace(param.ObjFilter.Email)) &&
                        (x.Cpf.Equals(param.ObjFilter.Cpf) || param.ObjFilter.Cpf == null || param.ObjFilter.Cpf.Equals(default));



        }

        /// <summary>
        /// Retorna a propriedade a ser ordenada
        /// </summary>
        public static Expression<Func<Cliente, object>> SortProp(this PagingQueryParam<Cliente> param)
        {
            switch (param?.SortProperty?.ToLower())
            {
                case "idcliente":
                    return fa => fa.IdCliente;
                case "email":
                    return fa => fa.Email;
                case "cpf":
                    return fa => fa.Cpf;
                default: return fa => fa.Nome;
            }
        }
    }
}
