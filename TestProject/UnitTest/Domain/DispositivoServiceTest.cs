using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands;
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
    public partial class DispositivoServiceTest
    {
        private readonly IGateways<Dispositivo> _gatewayDispositivoMock;
        private readonly IValidator<Dispositivo> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public DispositivoServiceTest()
        {
            _validator = new DispositivoValidator();
            _gatewayDispositivoMock = Substitute.For<IGateways<Dispositivo>>();
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async Task InserirComDadosValidos(string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                Identificador = identificador
            };

            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);


            //Act
            var result = await domainService.InsertAsync(dispositivo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a inserção com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, false, 3)]
        public async Task InserirComDadosInvalidos(string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                Identificador = identificador
            };

            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);

            //Act
            var result = await domainService.InsertAsync(dispositivo);

            //Assert
            Assert.False(result.IsValid);

        }

        /// <summary>
        /// Testa a alteração com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task AlterarComDadosValidos(Guid idDispositivo, string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = idDispositivo,
                Identificador = identificador
            };

            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);

            //Mockando retorno do metodo interno do UpdateAsync
            _gatewayDispositivoMock.UpdateAsync(Arg.Any<Dispositivo>())
                .Returns(Task.FromResult(dispositivo));

            //Act
            var result = await domainService.UpdateAsync(dispositivo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a alteração com dados inválidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, false, 3)]
        public async Task AlterarComDadosInvalidos(Guid idDispositivo, string identificador)
        {
            ///Arrange 
            var dispositivo = new Dispositivo
            {
                IdDispositivo = idDispositivo,
                Identificador = identificador
            };

            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);

            //Act
            var result = await domainService.UpdateAsync(dispositivo);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task DeletarDispositivo(Guid idDispositivo, string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = idDispositivo,
                Identificador = identificador
            };

            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _gatewayDispositivoMock.FindByIdAsync(idDispositivo)
                .Returns(new ValueTask<Dispositivo>(dispositivo));

            _gatewayDispositivoMock.DeleteAsync(idDispositivo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await domainService.DeleteAsync(idDispositivo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarDispositivoPorIdComDadosValidos(Guid idDispositivo, string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = idDispositivo,
                Identificador = identificador
            };

            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);

            //Mockando retorno do metodo interno do FindByIdAsync
            _ = _gatewayDispositivoMock.FindByIdAsync(idDispositivo)
                .Returns(new ValueTask<Dispositivo>(dispositivo));

            //Act
            var result = await domainService.FindByIdAsync(idDispositivo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarDispositivoPorIdComDadosInvalidos(Guid idDispositivo, string identificador)
        {
            ///Arrange
            _ = new Dispositivo
            {
                Identificador = identificador
            };

            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);

            //Act
            var result = await domainService.FindByIdAsync(idDispositivo);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarDispositivo(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange
            var domainService = new DispositivoService(_gatewayDispositivoMock, _validator);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayDispositivoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Dispositivo>>(),
                Arg.Any<Expression<Func<Dispositivo, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Dispositivo>>(new PagingQueryResult<Dispositivo>(new List<Dispositivo>(dispositivos))));


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
        public async Task ConsultarDispositivoComCondicao(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange
            var param = new PagingQueryParam<Dispositivo>() { CurrentPage = 1, Take = 10 };
            var command = new DispositivoGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayDispositivoMock.GetItemsAsync(Arg.Any<PagingQueryParam<Dispositivo>>(),
                Arg.Any<Expression<Func<Dispositivo, bool>>>(),
                Arg.Any<Expression<Func<Dispositivo, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Dispositivo>>(new PagingQueryResult<Dispositivo>(new List<Dispositivo>(dispositivos))));

            //Act
            var result = await _gatewayDispositivoMock.GetItemsAsync(filter, param.ConsultRule(), sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        /// <summary>
        /// Testa a consulta sem condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarDispositivoSemCondicao(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange
            var command = new DispositivoGetItemsCommand(filter, sortProp);

            //Mockando retorno do metodo interno do GetItemsAsync
            _gatewayDispositivoMock.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Dispositivo>>(new PagingQueryResult<Dispositivo>(new List<Dispositivo>(dispositivos))));

            //Act
            var result = await _gatewayDispositivoMock.GetItemsAsync(filter, sortProp);

            //Assert
            Assert.True(result.Content.Any());
        }

        #region [ Xunit MemberData ]

        /// <summary>
        /// Mock de dados
        /// </summary>
        public static IEnumerable<object[]>? ObterDados(enmTipo tipo, bool dadosValidos, int quantidade)
        {
            switch (tipo)
            {
                case enmTipo.Inclusao:
                    if (dadosValidos)
                        return DispositivoMock.ObterDadosValidos(quantidade);
                    else
                        return DispositivoMock.ObterDadosInvalidos(quantidade);
                case enmTipo.Alteracao:
                    if (dadosValidos)
                        return DispositivoMock.ObterDadosValidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                    else
                        return DispositivoMock.ObterDadosInvalidos(quantidade)
                            .Select(i => new object[] { Guid.NewGuid() }.Concat(i).ToArray());
                case enmTipo.Consulta:
                    return DispositivoMock.ObterDadosConsulta(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
