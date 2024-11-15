using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands;
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
    public partial class ClienteControllerTest
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Cliente> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ClienteControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _validator = new ClienteValidator();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var aplicationController = new ClienteController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ClientePostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PostAsync(Cliente);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var aplicationController = new ClienteController(_mediator, _validator);

            //Act
            var result = await aplicationController.PostAsync(Cliente);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var aplicationController = new ClienteController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ClientePutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idCliente, Cliente);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var aplicationController = new ClienteController(_mediator, _validator);

            //Act
            var result = await aplicationController.PutAsync(idCliente, Cliente);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarCliente(Guid idCliente)
        {
            ///Arrange
            var aplicationController = new ClienteController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ClienteDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.DeleteAsync(idCliente);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarClientePorId(Guid idCliente)
        {
            ///Arrange
            var aplicationController = new ClienteController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ClienteFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.FindByIdAsync(idCliente);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarClienteComCondicao(IPagingQueryParam filter, Expression<Func<Cliente, object>> sortProp, IEnumerable<Cliente> clientes)
        {
            ///Arrange
            var aplicationController = new ClienteController(_mediator, _validator);
            var param = new PagingQueryParam<Cliente>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ClienteGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Cliente>(new List<Cliente>(clientes), 1, 1)));

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
        public async Task ConsultarClienteSemCondicao(IPagingQueryParam filter, Expression<Func<Cliente, object>> sortProp, IEnumerable<Cliente> clientes)
        {
            ///Arrange
            var aplicationController = new ClienteController(_mediator, _validator);
            var param = new PagingQueryParam<Cliente>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<ClienteGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Cliente>(new List<Cliente>(clientes), 1, 1)));

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
        public async Task ConsultarClienteComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Cliente, object>> sortProp, IEnumerable<Cliente> clientes)
        {
            ///Arrange

            filter = null;
            var param = new PagingQueryParam<Cliente>() { CurrentPage = 1, Take = 10 };
            var aplicationController = new ClienteController(_mediator, _validator);

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
        public async Task ConsultarClienteSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Cliente, object>> sortProp, IEnumerable<Cliente> clientes)
        {
            ///Arrange

            filter = null;
            var aplicationController = new ClienteController(_mediator, _validator);

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
                        return ClienteMock.ObterDadosValidos(quantidade);
                    else
                        return ClienteMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return ClienteMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return ClienteMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    return ClienteMock.ObterDadosConsulta(quantidade);
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return ClienteMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return ClienteMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
