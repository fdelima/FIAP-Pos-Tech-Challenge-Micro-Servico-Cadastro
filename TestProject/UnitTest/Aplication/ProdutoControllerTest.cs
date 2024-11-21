using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Produto.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Validator;
using FluentValidation;
using MediatR;
using NSubstitute;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ProdutoControllerTest
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Produto> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProdutoControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _validator = new ProdutoValidator();
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

            var aplicationController = new ProdutoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProdutoPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PostAsync(produto);

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

            var aplicationController = new ProdutoController(_mediator, _validator);

            //Act
            var result = await aplicationController.PostAsync(produto);

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

            var aplicationController = new ProdutoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProdutoPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idProduto, produto);

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

            var aplicationController = new ProdutoController(_mediator, _validator);

            //Act
            var result = await aplicationController.PutAsync(idProduto, produto);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarProduto(Guid idProduto)
        {
            ///Arrange
            var aplicationController = new ProdutoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProdutoDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.DeleteAsync(idProduto);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarProdutoPorId(Guid idProduto)
        {
            ///Arrange
            var aplicationController = new ProdutoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProdutoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.FindByIdAsync(idProduto);

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
            var aplicationController = new ProdutoController(_mediator, _validator);
            var param = new PagingQueryParam<Produto>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProdutoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Produto>(new List<Produto>(produtos), 1, 1)));

            //Act
            var result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);

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
            var aplicationController = new ProdutoController(_mediator, _validator);
            var param = new PagingQueryParam<Produto>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ProdutoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Produto>(new List<Produto>(produtos), 1, 1)));

            //Act
            var result = await aplicationController.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async Task ConsultarProdutoComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Produto, object>> sortProp, IEnumerable<Produto> produtos)
        {
            ///Arrange

            filter = null;
            var param = new PagingQueryParam<Produto>() { CurrentPage = 1, Take = 10 };
            var aplicationController = new ProdutoController(_mediator, _validator);

            //Act
            try
            {
                var result = await aplicationController.ConsultItemsAsync(filter, param.ConsultRule(), sortProp);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.True(ex.GetType().Equals(typeof(InvalidOperationException)));
            }
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa Inválida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, false, 10)]
        public async Task ConsultarProdutoSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Produto, object>> sortProp, IEnumerable<Produto> produtos)
        {
            ///Arrange

            filter = null;
            var aplicationController = new ProdutoController(_mediator, _validator);

            //Act
            try
            {
                var result = await aplicationController.GetItemsAsync(filter, sortProp);
            }
            catch (Exception ex)
            {
                //Assert
                Assert.True(ex.GetType().Equals(typeof(InvalidOperationException)));
            }
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
