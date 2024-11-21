using System.Linq.Expressions;
using TestProject.IntegrationTest.Infra;
using TestProject.MockData;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Gateways;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;

namespace TestProject.IntegrationTest.External
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    public partial class ClienteGatewayTest : IClassFixture<TestsBase>
    {
        internal readonly SqlServerTestFixture _sqlserverTest;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ClienteGatewayTest(TestsBase data)
        {
            _sqlserverTest = data._sqlserverTest;
        }

        /// <summary>
        /// Testa a inserção com dados válidos
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Inclusao, true, 3)]
        public async void InserirComDadosValidos(string nome, string email, long cpf)
        {
            ///Arrange
            var cliente = new Cliente
            {
                IdCliente = Guid.NewGuid(),
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            //Act
            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());
            var result = await _clienteGateway.InsertAsync(cliente);

            //Assert
            try
            {
                await _clienteGateway.CommitAsync();
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
        public async Task InserirComDadosInvalidos(string nome, string email, long cpf)
        {
            ///Arrange
            var cliente = new Cliente
            {
                IdCliente = Guid.NewGuid(),
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            //Act
            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());
            var result = await _clienteGateway.InsertAsync(cliente);

            //Assert
            try
            {
                await _clienteGateway.CommitAsync();
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
        public async void AlterarComDadosValidos(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());
            var result = await _clienteGateway.InsertAsync(cliente);
            await _clienteGateway.CommitAsync();

            //Alterando
            cliente.Nome = nome + " ALTERADO !!!";

            var dbEntity = await _clienteGateway.FindByIdAsync(idCliente);

            //Act
            await _clienteGateway.UpdateAsync(dbEntity, cliente);
            await _clienteGateway.UpdateAsync(cliente);

            try
            {
                await _clienteGateway.CommitAsync();
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
        public async void AlterarComDadosInvalidos(Guid idCliente, string nome, string email, long cpf)
        {
            ///Arrange
            var cliente = new Cliente
            {
                IdCliente = idCliente,
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());
            var result = await _clienteGateway.InsertAsync(cliente);
            await _clienteGateway.CommitAsync();

            //Alterando
            cliente.Nome = null;

            var dbEntity = await _clienteGateway.FindByIdAsync(idCliente);

            //Act
            await _clienteGateway.UpdateAsync(dbEntity, cliente);
            await _clienteGateway.UpdateAsync(cliente);

            //Assert
            try
            {
                await _clienteGateway.CommitAsync();
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
        public async void DeletarCliente(string nome, string email, long cpf)
        {
            ///Arrange
            var cliente = new Cliente
            {
                IdCliente = Guid.NewGuid(),
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());
            var result = await _clienteGateway.InsertAsync(cliente);
            await _clienteGateway.CommitAsync();

            //Act
            await _clienteGateway.DeleteAsync(cliente.IdCliente);

            //Assert
            try
            {
                await _clienteGateway.CommitAsync();
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
        public async void ConsultarClientePorId(string nome, string email, long cpf)
        {
            ///Arrange
            var cliente = new Cliente
            {
                IdCliente = Guid.NewGuid(),
                Nome = nome,
                Email = email,
                Cpf = cpf
            };

            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());
            await _clienteGateway.InsertAsync(cliente);
            await _clienteGateway.CommitAsync();

            //Act
            var result = await _clienteGateway.FindByIdAsync(cliente.IdCliente);

            //Assert
            Assert.True(result != null);
        }

        /// <summary>
        /// Testa a consulta Valida
        /// </summary>
        [Theory]
        [MemberData(nameof(ObterDados), enmTipo.Consulta, true, 3)]
        public async Task ConsultarCliente(IPagingQueryParam filter, Expression<Func<Cliente, object>> sortProp, IEnumerable<Cliente> Clientes)
        {
            ///Arrange
            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());

            //Act
            var result = await _clienteGateway.GetItemsAsync(filter, sortProp);

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
            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());

            await _clienteGateway.InsertRangeAsync(clientes);
            await _clienteGateway.CommitAsync();

            var param = new PagingQueryParam<Cliente>() { CurrentPage = 1, Take = 10, ObjFilter = clientes.ElementAt(0) };

            //Act
            var result = await _clienteGateway.GetItemsAsync(filter, param.ConsultRule(), sortProp);

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
            var _clienteGateway = new BaseGateway<Cliente>(_sqlserverTest.GetDbContext());

            await _clienteGateway.InsertRangeAsync(clientes);
            await _clienteGateway.CommitAsync();

            //Act
            var result = await _clienteGateway.GetItemsAsync(filter, sortProp);

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
