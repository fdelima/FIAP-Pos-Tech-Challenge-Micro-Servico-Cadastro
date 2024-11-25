using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class ComponentTestsBase : IDisposable
    {
        protected readonly DbContextOptions<FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context> _options;
        internal readonly SqlServerTestFixture _sqlserverTest;
        internal readonly ApiTestFixture _apiTest;

        public ComponentTestsBase()
        {
            // Do "global" initialization here; Called before every test method.
            _sqlserverTest = new SqlServerTestFixture(
                imageNameMssqlTools: "fdelima/fiap-pos-techchallenge-micro-servico-cadastro-gurpo-71-scripts-database:fase4-component-test",
                containerNameMssqlTools: "mssql-tools-cadastro-component-test",
                databaseContainerName: "sqlserver-db-cadastro-component-test", port: "1428");
            _apiTest = new ApiTestFixture();
        }

        public void Dispose()
        {
            // Do "global" teardown here; Called after every test method.
            _sqlserverTest.Dispose();
            _apiTest.Dispose();
        }
    }
}
