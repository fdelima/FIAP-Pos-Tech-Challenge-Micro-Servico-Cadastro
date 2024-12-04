using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Services;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Validator;
using FluentValidation;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Domain
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ProdutoServiceTest
    {
        private readonly IGateways<Produto> _gatewayProdutoMock;
        private readonly IValidator<Produto> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProdutoServiceTest()
        {
            _validator = new ProdutoValidator();
            _gatewayProdutoMock = Substitute.For<IGateways<Produto>>();
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
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            //Act
            var result = await domainService.InsertAsync(produto);

            //Assert
            Assert.True(result.IsValid);
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
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            //Act
            var result = await domainService.InsertAsync(produto);

            //Assert
            Assert.False(result.IsValid);

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

            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            // Mockando retorno do metodo interno do FirstOrDefaultWithIncludeAsync
            _gatewayProdutoMock.FirstOrDefaultWithIncludeAsync(
                Arg.Any<Expression<Func<Produto, ICollection<ProdutoImagens>>>>(),
                Arg.Any<Expression<Func<Produto, bool>>>())
                .Returns(new ValueTask<Produto?>(produto));

            // Mockando retorno do metodo interno do UpdateAsync
            _gatewayProdutoMock.UpdateAsync(Arg.Any<Produto>(), Arg.Any<Produto>())
                .Returns(Task.CompletedTask);

            //Act
            var result = await domainService.UpdateAsync(produto);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
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

            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            //Act
            var result = await domainService.UpdateAsync(produto);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarProduto(Guid idProduto, string nome, decimal preco, string descricao, string categoria)
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

            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayProdutoMock.FindByIdAsync(idProduto)
                .Returns(new ValueTask<Produto>(produto));

            _gatewayProdutoMock.DeleteAsync(idProduto)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idProduto);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarProdutoPorIdComDadosValidos(Guid idProduto, string nome, decimal preco, string descricao, string categoria)
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

            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            // Mockando retorno do metodo interno do FirstOrDefaultWithIncludeAsync
            _gatewayProdutoMock.FirstOrDefaultWithIncludeAsync(
                Arg.Any<Expression<Func<Produto, ICollection<ProdutoImagens>>>>(),
                Arg.Any<Expression<Func<Produto, bool>>>())
                .Returns(new ValueTask<Produto?>(produto));

            //Act
            var result = await domainService.FindByIdAsync(idProduto);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarProdutoPorIdComDadosInvalidos(Guid idProduto, string nome, decimal preco, string descricao, string categoria)
        {
            ///Arrange
            _ = new Produto
            {
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            //Act
            var result = await domainService.FindByIdAsync(idProduto);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarProduto(IPagingQueryParam filter, Expression<Func<Produto, object>> sortProp, IEnumerable<Produto> produtos)
        {
            ///Arrange
            var domainService = new ProdutoService(_gatewayProdutoMock, _validator);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayProdutoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Produto>>(),
                Arg.Any<Expression<Func<Produto, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Produto>>(new PagingQueryResult<Produto>(new List<Produto>(produtos))));


            //Act
            var result = await domainService.GetItemsAsync(filter, sortProp);

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
            var param = new PagingQueryParam<Produto>() { CurrentPage = 1, Take = 10 };
            var command = new ProdutoGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayProdutoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Produto>>(),
                Arg.Any<Expression<Func<Produto, bool>>>(),
                Arg.Any<Expression<Func<Produto, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Produto>>(new PagingQueryResult<Produto>(new List<Produto>(produtos))));

            //Act
            var result = await _gatewayProdutoMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

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
            var command = new ProdutoGetItemsCommand(filter, sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayProdutoMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Produto>>(new PagingQueryResult<Produto>(new List<Produto>(produtos))));

            //Act
            var result = await _gatewayProdutoMock.GetItemsAsync(filter, sortProp);

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
