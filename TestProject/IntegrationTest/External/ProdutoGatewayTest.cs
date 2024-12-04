using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Gateways;
using System.Linq.Expressions;
using TestProject.Infra;
using TestProject.MockData;

namespace TestProject.IntegrationTest.External
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ProdutoGatewayTest : IClassFixture<IntegrationTestsBase>
    {
        internal readonly SqlServerTestFixture _sqlserverTest;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProdutoGatewayTest(IntegrationTestsBase data)
        {
            _sqlserverTest = data._sqlserverTest;
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(string nome, decimal preco, string descricao, string categoria)
        {
            ///Arrange
            var produto = new Produto
            {
                IdProduto = Guid.NewGuid(),
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            //Act
            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());
            var result = await _produtoGateway.InsertAsync(produto);

            //Assert
            try
            {
                await _produtoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(string nome, decimal preco, string descricao, string categoria)
        {
            ///Arrange
            var produto = new Produto
            {
                IdProduto = Guid.NewGuid(),
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            //Act
            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());
            var result = await _produtoGateway.InsertAsync(produto);

            //Assert
            try
            {
                await _produtoGateway.CommitAsync();
                Assert.True(false);
            }
            catch (InvalidOperationException)
            {
                Assert.True(true);
            }

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idProduto, string nome, decimal preco, string descricao, string categoria)
        {
            ///Arrange
            var produto = new Produto
            {
                IdProduto = idProduto,
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());
            var result = await _produtoGateway.InsertAsync(produto);
            await _produtoGateway.CommitAsync();

            //Alterando
            produto.Nome = nome + " ALTERADO !!!";

            var dbEntity = await _produtoGateway.FindByIdAsync(idProduto);

            //Act
            await _produtoGateway.UpdateAsync(dbEntity, produto);
            await _produtoGateway.UpdateAsync(produto);

            try
            {
                await _produtoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosInvalidos(Guid idProduto, string nome, decimal preco, string descricao, string categoria)
        {
            ///Arrange
            var produto = new Produto
            {
                IdProduto = idProduto,
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());
            var result = await _produtoGateway.InsertAsync(produto);
            await _produtoGateway.CommitAsync();

            //Alterando
            produto.Nome = null;

            var dbEntity = await _produtoGateway.FindByIdAsync(idProduto);

            //Act
            await _produtoGateway.UpdateAsync(dbEntity, produto);
            await _produtoGateway.UpdateAsync(produto);

            //Assert
            try
            {
                await _produtoGateway.CommitAsync();
                Assert.True(false);
            }
            catch (InvalidOperationException)
            {
                Assert.True(true);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 1)]
        public async Task DeletarProduto(string nome, decimal preco, string descricao, string categoria)
        {
            ///Arrange
            var produto = new Produto
            {
                IdProduto = Guid.NewGuid(),
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());
            var result = await _produtoGateway.InsertAsync(produto);
            await _produtoGateway.CommitAsync();

            //Act
            await _produtoGateway.DeleteAsync(produto.IdProduto);

            //Assert
            try
            {
                await _produtoGateway.CommitAsync();
                Assert.True(true);
            }
            catch (InvalidOperationException)
            {
                Assert.True(false);
            }
            catch (Exception)
            {
                Assert.True(false);
            }
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 1)]
        public async Task ConsultarProdutoPorId(string nome, decimal preco, string descricao, string categoria)
        {
            ///Arrange
            var produto = new Produto
            {
                IdProduto = Guid.NewGuid(),
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());
            await _produtoGateway.InsertAsync(produto);
            await _produtoGateway.CommitAsync();

            //Act
            var result = await _produtoGateway.FindByIdAsync(produto.IdProduto);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProduto(IPagingQueryParam filter, Expression<Func<Produto, object>> sortProp, IEnumerable<Produto> Produtos)
        {
            ///Arrange
            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());

            //Act
            var result = await _produtoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProdutoComCondicao(IPagingQueryParam filter, Expression<Func<Produto, object>> sortProp, IEnumerable<Produto> produtos)
        {
            ///Arrange
            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());

            await _produtoGateway.InsertRangeAsync(produtos);
            await _produtoGateway.CommitAsync();

            var param = new PagingQueryParam<Produto>() { CurrentPage = 1, Take = 10, ObjFilter = produtos.ElementAt(0) };

            //Act
            var result = await _produtoGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProdutoSemCondicao(IPagingQueryParam filter, Expression<Func<Produto, object>> sortProp, IEnumerable<Produto> produtos)
        {
            ///Arrange
            var _produtoGateway = new BaseGateway<Produto>(_sqlserverTest.GetDbContext());

            await _produtoGateway.InsertRangeAsync(produtos);
            await _produtoGateway.CommitAsync();

            //Act
            var result = await _produtoGateway.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        #region [ Xunit MemberData ]

        /// <summary>
        /// Mock de dados
        /// </summary>
        public static IEnumerable<object[]> ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
        {
            switch (tipo)
            {
                case enmTipo.Inclusao:
                    if (dadosValidos)
                        return ProdutoMock.ObterDadosValidos(quantidade);
                    else
                        return ProdutoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return ProdutoMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return ProdutoMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    return ProdutoMock.ObterDadosConsulta(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
