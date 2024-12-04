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
    public partial class DispositivoGatewayTest : IClassFixture<IntegrationTestsBase>
    {
        internal readonly SqlServerTestFixture _sqlserverTest;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public DispositivoGatewayTest(IntegrationTestsBase data)
        {
            _sqlserverTest = data._sqlserverTest;
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
                IdDispositivo = Guid.NewGuid(),
                Identificador = identificador
            };

            //Act
            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());
            var result = await _DispositivoGateway.InsertAsync(dispositivo);

            //Assert
            try
            {
                await _DispositivoGateway.CommitAsync();
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
        public async Task InserirComDadosInvalidos(string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = Guid.NewGuid(),
                Identificador = identificador
            };

            //Act
            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());
            var result = await _DispositivoGateway.InsertAsync(dispositivo);

            //Assert
            try
            {
                await _DispositivoGateway.CommitAsync();
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
        public async Task AlterarComDadosValidos(Guid idDispositivo, string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = idDispositivo,
                Identificador = identificador
            };

            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());
            var result = await _DispositivoGateway.InsertAsync(dispositivo);
            await _DispositivoGateway.CommitAsync();

            //Alterando
            dispositivo.Identificador = identificador + " ALTERADO !!!";

            var dbEntity = await _DispositivoGateway.FindByIdAsync(idDispositivo);

            //Act
            await _DispositivoGateway.UpdateAsync(dbEntity, dispositivo);
            await _DispositivoGateway.UpdateAsync(dispositivo);

            try
            {
                await _DispositivoGateway.CommitAsync();
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
        public async Task AlterarComDadosInvalidos(Guid idDispositivo, string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = idDispositivo,
                Identificador = identificador
            };

            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());
            var result = await _DispositivoGateway.InsertAsync(dispositivo);
            await _DispositivoGateway.CommitAsync();

            //Alterando
            dispositivo.Identificador = null;

            var dbEntity = await _DispositivoGateway.FindByIdAsync(idDispositivo);

            //Act
            await _DispositivoGateway.UpdateAsync(dbEntity, dispositivo);
            await _DispositivoGateway.UpdateAsync(dispositivo);

            //Assert
            try
            {
                await _DispositivoGateway.CommitAsync();
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
        public async Task DeletarDispositivo(string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = Guid.NewGuid(),
                Identificador = identificador
            };

            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());
            var result = await _DispositivoGateway.InsertAsync(dispositivo);
            await _DispositivoGateway.CommitAsync();

            //Act
            await _DispositivoGateway.DeleteAsync(dispositivo.IdDispositivo);

            //Assert
            try
            {
                await _DispositivoGateway.CommitAsync();
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
        public async Task ConsultarDispositivoPorId(string identificador)
        {
            ///Arrange
            var dispositivo = new Dispositivo
            {
                IdDispositivo = Guid.NewGuid(),
                Identificador = identificador
            };

            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());
            await _DispositivoGateway.InsertAsync(dispositivo);
            await _DispositivoGateway.CommitAsync();

            //Act
            var result = await _DispositivoGateway.FindByIdAsync(dispositivo.IdDispositivo);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarDispositivo(IPagingQueryParam filter, Expression<Func<Dispositivo, object>> sortProp, IEnumerable<Dispositivo> dispositivos)
        {
            ///Arrange
            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());

            //Act
            var result = await _DispositivoGateway.GetItemsAsync(filter, sortProp);

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
            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());

            await _DispositivoGateway.InsertRangeAsync(dispositivos);
            await _DispositivoGateway.CommitAsync();

            var param = new PagingQueryParam<Dispositivo>() { CurrentPage = 1, Take = 10, ObjFilter = dispositivos.ElementAt(0) };

            //Act
            var result = await _DispositivoGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

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
            var _DispositivoGateway = new BaseGateway<Dispositivo>(_sqlserverTest.GetDbContext());

            await _DispositivoGateway.InsertRangeAsync(dispositivos);
            await _DispositivoGateway.CommitAsync();

            //Act
            var result = await _DispositivoGateway.GetItemsAsync(filter, sortProp);

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
                default:
                    return null;
            }
        }

        #endregion

    }
}
