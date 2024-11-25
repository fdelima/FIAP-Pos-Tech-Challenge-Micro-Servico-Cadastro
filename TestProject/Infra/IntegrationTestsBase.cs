using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class IntegrationTestsBase : IDisposable
    {
        protected readonly DbContextOptions<FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context> _options;
        internal readonly SqlServerTestFixture _sqlserverTest;

        public IntegrationTestsBase()
        {
            // Do "global" initialization here; Called before every test method.
            _sqlserverTest = new SqlServerTestFixture(
                imageNameMssqlTools: "fdelima/fiap-pos-techchallenge-micro-servico-cadastro-gurpo-71-scripts-database:fase4-test",
                containerNameMssqlTools: "mssql-tools-cadastro-test",
                databaseContainerName: "sqlserver-db-cadastro-test", port: "1432");
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
            _sqlserverTest.Dispose();
        }
    }
}
