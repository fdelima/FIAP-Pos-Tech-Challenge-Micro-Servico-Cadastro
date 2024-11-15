using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Cliente.Handlers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Validator;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.ValuesObject;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using NSubstitute.Extensions;
using System.Linq.Expressions;
using TestProject.MockData;

namespace TestProject.UnitTest.Aplication
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ClienteUseCasesTest
    {
        private readonly IService<Cliente> _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ClienteUseCasesTest()
        {
            _service = Substitute.For<IService<Cliente>>();
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

            var command = new ClientePostCommand(Cliente);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(Cliente)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(Cliente)));

            //Act
            var handler = new ClientePostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new ClientePostCommand(Cliente);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(Cliente)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Cliente>()));

            //Act
            var handler = new ClientePostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new ClientePutCommand(idCliente, Cliente);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(Cliente)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new ClientePutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new ClientePutCommand(idCliente, Cliente);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(Cliente)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Cliente>()));

            //Act
            var handler = new ClientePutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarCliente(Guid idCliente)
        {
            ///Arrange
            var command = new ClienteDeleteCommand(idCliente);

            //Mockando retorno do serviço de domínio.
            _service.DeleteAsync(idCliente)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new ClienteDeleteHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarClientePorId(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var Cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var command = new ClienteFindByIdCommand(idCliente);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idCliente)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(Cliente)));

            //Act
            var handler = new ClienteFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var param = new PagingQueryParam<Cliente>() { CurrentPage = 1, Take = 10 };
            var command = new ClienteGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<Cliente>>(),
                Arg.Any<Expression<Func<Cliente, bool>>>(),
                Arg.Any<Expression<Func<Cliente, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Cliente>>(new PagingQueryResult<Cliente>(new List<Cliente>(clientes))));

            //Act
            var handler = new ClienteGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Cliente>>(new PagingQueryResult<Cliente>(new List<Cliente>(clientes))));

            //Act
            var handler = new ClienteGetItemsHandler(_service);
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
