using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.Controllers;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands;
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
    public partial class DispositivoControllerTest
    {
        private readonly IMediator _mediator;
        private readonly IValidator<Dispositivo> _validator;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public DispositivoControllerTest()
        {
            _mediator = Substitute.For<IMediator>();
            _validator = new DispositivoValidator();
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

            var aplicationController = new DispositivoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<DispositivoPostCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PostAsync(dispositivo);

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

            var aplicationController = new DispositivoController(_mediator, _validator);

            //Act
            var result = await aplicationController.PostAsync(dispositivo);

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

            var aplicationController = new DispositivoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<DispositivoPutCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.PutAsync(idDispositivo, dispositivo);

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

            var aplicationController = new DispositivoController(_mediator, _validator);

            //Act
            var result = await aplicationController.PutAsync(idDispositivo, dispositivo);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a deletar
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarDispositivo(Guid idDispositivo)
        {
            ///Arrange
            var aplicationController = new DispositivoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<DispositivoDeleteCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.DeleteAsync(idDispositivo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task ConsultarDispositivoPorId(Guid idDispositivo)
        {
            ///Arrange
            var aplicationController = new DispositivoController(_mediator, _validator);

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<DispositivoFindByIdCommand>())
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var result = await aplicationController.FindByIdAsync(idDispositivo);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta com condição de pesquisa
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarDispositivoComCondicao(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange
            var aplicationController = new DispositivoController(_mediator, _validator);
            var param = new PagingQueryParam<Dispositivo>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<DispositivoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Dispositivo>(new List<Dispositivo>(dispositivos), 1, 1)));

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
        public async Task ConsultarDispositivoSemCondicao(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange
            var aplicationController = new DispositivoController(_mediator, _validator);
            var param = new PagingQueryParam<Dispositivo>() { CurrentPage = 1, Take = 10 };

            //Mockando retorno do mediator.
            _mediator.Send(Arg.Any<DispositivoGetItemsCommand>())
                .Returns(Task.FromResult(new PagingQueryResult<Dispositivo>(new List<Dispositivo>(dispositivos), 1, 1)));

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
        public async Task ConsultarDispositivoComCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange

            filter = null;
            var param = new PagingQueryParam<Dispositivo>() { CurrentPage = 1, Take = 10 };
            var aplicationController = new DispositivoController(_mediator, _validator);

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
        public async Task ConsultarDispositivoSemCondicaoInvalidos(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange

            filter = null;
            var aplicationController = new DispositivoController(_mediator, _validator);

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
                case enmTipo.ConsultaPorId:
                    if (dadosValidos)
                        return DispositivoMock.ObterDadosConsultaPorIdValidos(quantidade);
                    else
                        return DispositivoMock.ObterDadosConsultaPorIdInvalidos(quantidade);
                default:
                    return null;
            }
        }

        #endregion

    }
}
