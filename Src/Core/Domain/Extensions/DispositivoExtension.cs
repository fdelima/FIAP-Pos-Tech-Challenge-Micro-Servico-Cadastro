using FIAP.Pos.Tech.Challenge.Domain.Entities;
using System.Linq.Expressions;

namespace FIAP.Pos.Tech.Challenge.Domain.Extensions
{
    /// <summary>
    /// Extensão da model para informar os campos de validação.
    /// </summary>
    public static class DispositivoExtension
    {
        /// <summary>
        /// Retorna a regra de validação a ser utilizada na atualização.
        /// </summary>
        public static Expression<Func<Dispositivo, bool>> ConsultRule(this PagingQueryParam<Dispositivo> param)
        {
            return x => (x.IdDispositivo.Equals(param.ObjFilter.IdDispositivo) || param.ObjFilter.IdDispositivo.Equals(default)) &&
                        (x.Identificador.Contains(param.ObjFilter.Identificador) || string.IsNullOrWhiteSpace(param.ObjFilter.Identificador)) &&
                        (string.IsNullOrWhiteSpace(param.ObjFilter.Modelo) || x.Modelo.Contains(param.ObjFilter.Modelo)) &&
                        (string.IsNullOrWhiteSpace(param.ObjFilter.Serie) || x.Serie.Contains(param.ObjFilter.Serie));
        }

        /// <summary>
        /// Retorna a propriedade a ser ordenada
        /// </summary>
        public static Expression<Func<Dispositivo, object>> SortProp(this PagingQueryParam<Dispositivo> param)
        {
            switch (param?.SortProperty?.ToLower())
            {
                case "iddispositivo":
                    return fa => fa.IdDispositivo;
                case "modelo":
                    return fa => fa.Modelo;
                case "serie":
                    return fa => fa.Serie;
                default: return fa => fa.Identificador;
            }
        }
    }
}
