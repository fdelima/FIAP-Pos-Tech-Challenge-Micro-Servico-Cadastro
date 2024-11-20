using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;

namespace TestProject.MockData
{
    /// <summary>
    /// Mock de dados das ações
    /// </summary>
    public class ProdutoMock
    {

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    $"Nome do Produto {index}",
                    10 + index,
                    $"Descricao do Produto {index}",
                    "LANCHE"
                };
        }

        /// <summary>
        /// Mock de dados inválidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    string.Empty,
                    0,
                    string.Empty,
                    string.Empty
                };
        }

        /// <summary>
        /// Mock de dados válidos
        /// </summary>
        public static IEnumerable<object[]> ObterDadosConsulta(int quantidade)
        {

            for (var index = 1; index <= quantidade; index++)
            {
                var param = new PagingQueryParam<Produto>() { CurrentPage = index, Take = 10 };
                yield return new object[]
                {
                    param,
                    param.SortProp(),
                    new List<Produto>{
                        new Produto {
                            Nome = $"Nome do Produto {index}",
                            Preco = 10 + index,
                            Descricao = $"Descricao do Produto {index}",
                            Categoria = "LANCHE"
                        }
                    }
                };
            }
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdValidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.NewGuid()
                };
        }

        public static IEnumerable<object[]> ObterDadosConsultaPorIdInvalidos(int quantidade)
        {
            for (var index = 1; index <= quantidade; index++)
                yield return new object[]
                {
                    Guid.Empty
                };
        }
    }
}
