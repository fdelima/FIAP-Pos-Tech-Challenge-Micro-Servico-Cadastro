using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ProdutoUseCasesTest
    {
        private readonly IProdutoService _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProdutoUseCasesTest()
        {
            _service = Substitute.For<IProdutoService>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(string nome, decimal preco, string descricao, string categoria)
        {
            // Arrange
            var produto = new Produto
            {
                Nome = nome,
                Preco = preco,
                Descricao = descricao,
                Categoria = categoria
            };

            var command = new ProdutoPostCommand(produto);

            // Mockando retorno do serviço de domínio.
            _service.InsertAsync(produto, Arg.Any<string[]>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(produto)));

            // Act
            var handler = new ProdutoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
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

            var command = new ProdutoPostCommand(produto);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(produto)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Produto>()));

            //Act
            var handler = new ProdutoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new ProdutoPutCommand(idProduto, produto);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(produto)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new ProdutoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new ProdutoPutCommand(idProduto, produto);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(produto)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Produto>()));

            //Act
            var handler = new ProdutoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarProduto(Guid idProduto)
        {
            ///Arrange
            var command = new ProdutoDeleteCommand(idProduto);

            //Mockando retorno do serviço de domínio.
            _service.DeleteAsync(idProduto)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new ProdutoDeleteHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarProdutoPorId(Guid idProduto, string nome, decimal preco, string descricao, string categoria)
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

            var command = new ProdutoFindByIdCommand(idProduto);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idProduto)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(produto)));

            //Act
            var handler = new ProdutoFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
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

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<Produto>>(),
                Arg.Any<Expression<Func<Produto, bool>>>(),
                Arg.Any<Expression<Func<Produto, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Produto>>(new PagingQueryResult<Produto>(new List<Produto>(produtos))));

            //Act
            var handler = new ProdutoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Produto>>(new PagingQueryResult<Produto>(new List<Produto>(produtos))));

            //Act
            var handler = new ProdutoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return ProdutoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return ProdutoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
