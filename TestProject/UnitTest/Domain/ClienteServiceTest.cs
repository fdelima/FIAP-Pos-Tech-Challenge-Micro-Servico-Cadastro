using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands;
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
    public partial class ClienteServiceTest
    {
        private readonly IGateways<Cliente> _gatewayClienteMock;
        private readonly IValidator<Cliente> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ClienteServiceTest()
        {
            _validator = new ClienteValidator();
            _gatewayClienteMock = Substitute.For<IGateways<Cliente>>();
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

            var domainService = new ClienteService(_gatewayClienteMock, _validator);


            //Act
            var result = await domainService.InsertAsync(Cliente);

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

            var domainService = new ClienteService(_gatewayClienteMock, _validator);

            //Act
            var result = await domainService.InsertAsync(Cliente);

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

            var domainService = new ClienteService(_gatewayClienteMock, _validator);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayClienteMock.UpdateAsync(Arg.Any<Cliente>())
                .Returns(Task.FromResult(Cliente));

            //Act
            var result = await domainService.UpdateAsync(Cliente);

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

            var domainService = new ClienteService(_gatewayClienteMock, _validator);

            //Act
            var result = await domainService.UpdateAsync(Cliente);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarCliente(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var domainService = new ClienteService(_gatewayClienteMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayClienteMock.FindByIdAsync(idCliente)
                .Returns(new ValueTask<Cliente>(Cliente));

            _gatewayClienteMock.DeleteAsync(idCliente)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idCliente);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarClientePorIdComDadosValidos(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var domainService = new ClienteService(_gatewayClienteMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayClienteMock.FindByIdAsync(idCliente)
                .Returns(new ValueTask<Cliente>(Cliente));

            //Act
            var result = await domainService.FindByIdAsync(idCliente);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarClientePorIdComDadosInvalidos(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var domainService = new ClienteService(_gatewayClienteMock, _validator);

            //Act
            var result = await domainService.FindByIdAsync(idCliente);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarCliente(IPagingQueryParam filter, Expression<Func<Cliente, object>> sortProp, IEnumerable<Cliente> clientes)
        {
            ///Arrange
            var domainService = new ClienteService(_gatewayClienteMock, _validator);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayClienteMock.GetItemsAsync(Arg.Any<PagingQueryParam<Cliente>>(),
                Arg.Any<Expression<Func<Cliente, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Cliente>>(new PagingQueryResult<Cliente>(new List<Cliente>(clientes))));


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
        public async Task ConsultarClienteComCondicao(IPagingQueryParam filter, Expression<Func<Cliente, object>> sortProp, IEnumerable<Cliente> clientes)
        {
            ///Arrange
            var param = new PagingQueryParam<Cliente>() { CurrentPage = 1, Take = 10 };
            var command = new ClienteGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayClienteMock.GetItemsAsync(Arg.Any<PagingQueryParam<Cliente>>(),
                Arg.Any<Expression<Func<Cliente, bool>>>(),
                Arg.Any<Expression<Func<Cliente, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Cliente>>(new PagingQueryResult<Cliente>(new List<Cliente>(clientes))));

            //Act
            var result = await _gatewayClienteMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

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
            var command = new ClienteGetItemsCommand(filter, sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayClienteMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Cliente>>(new PagingQueryResult<Cliente>(new List<Cliente>(clientes))));

            //Act
            var result = await _gatewayClienteMock.GetItemsAsync(filter, sortProp);

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
                default:
                    return null;
            }
        }

        #endregion

    }
}
