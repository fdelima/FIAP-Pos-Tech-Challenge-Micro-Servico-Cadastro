using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Commands;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Application.UseCases.Dispositivo.Handlers;
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
    public partial class DispositivoUseCasesTest
    {
        private readonly IService<Dispositivo> _service;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public DispositivoUseCasesTest()
        {
            _service = Substitute.For<IService<Dispositivo>>();
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

            var command = new DispositivoPostCommand(dispositivo);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(dispositivo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(dispositivo)));

            //Act
            var handler = new DispositivoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new DispositivoPostCommand(dispositivo);

            //Mockando retorno do serviço de domínio.
            _service.InsertAsync(dispositivo)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Dispositivo>()));

            //Act
            var handler = new DispositivoPostHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new DispositivoPutCommand(idDispositivo, dispositivo);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(dispositivo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new DispositivoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            var command = new DispositivoPutCommand(idDispositivo, dispositivo);

            //Mockando retorno do serviço de domínio.
            _service.UpdateAsync(dispositivo)
                .Returns(Task.FromResult(ModelResultFactory.NotFoundResult<Dispositivo>()));

            //Act
            var handler = new DispositivoPutHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.False(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.ConsultaPorId, true, 3)]
        public async Task DeletarDispositivo(Guid idDispositivo)
        {
            ///Arrange
            var command = new DispositivoDeleteCommand(idDispositivo);

            //Mockando retorno do serviço de domínio.
            _service.DeleteAsync(idDispositivo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult()));

            //Act
            var handler = new DispositivoDeleteHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(result.IsValid);
        }

        /// <summary>
        /// Testa a consulta por id
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Alteracao, true, 3)]
        public async Task ConsultarDispositivoPorId(Guid idDispositivo, string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = idDispositivo,
                Identificador = identificador
            };

            var command = new DispositivoFindByIdCommand(idDispositivo);

            //Mockando retorno do serviço de domínio.
            _service.FindByIdAsync(idDispositivo)
                .Returns(Task.FromResult(ModelResultFactory.SucessResult(dispositivo)));

            //Act
            var handler = new DispositivoFindByIdHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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
            var param = new PagingQueryParam<Dispositivo>() { CurrentPage = 1, Take = 10 };
            var command = new DispositivoGetItemsCommand(filter, param.ConsultRule(), sortProp);

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(Arg.Any<PagingQueryParam<Dispositivo>>(),
                Arg.Any<Expression<Func<Dispositivo, bool>>>(),
                Arg.Any<Expression<Func<Dispositivo, object>>>())
                .Returns(new ValueTask<PagingQueryResult<Dispositivo>>(new PagingQueryResult<Dispositivo>(new List<Dispositivo>(dispositivos))));

            //Act
            var handler = new DispositivoGetItemsHandler(_service);
            var result = await handler.Handle(command, CancellationToken.None);

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

            //Mockando retorno do serviço de domínio.
            _service.GetItemsAsync(filter, sortProp)
                .Returns(new ValueTask<PagingQueryResult<Dispositivo>>(new PagingQueryResult<Dispositivo>(new List<Dispositivo>(dispositivos))));

            //Act
            var handler = new DispositivoGetItemsHandler(_service);
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
