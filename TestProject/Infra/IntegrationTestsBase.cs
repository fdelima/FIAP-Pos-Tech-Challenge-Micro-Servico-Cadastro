using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class IntegrationTestsBase : IDisposable
    {
        const string containerNameMssqlTools = "mssql-tools-cadastro-test";
        internal readonly SqlServerTestFixture _sqlserverTest;
        private static int _tests = 0;

        public IntegrationTestsBase()
        {
            _tests += 1;
            _sqlserverTest = new SqlServerTestFixture(
            imageNameMssqlTools: "fdelima/fiap-pos-techchallenge-micro-servico-cadastro-gurpo-71-scripts-database:fase4-test",
            containerNameMssqlTools: containerNameMssqlTools,
            databaseContainerName: "sqlserver-db-cadastro-test", port: "1432");
            Thread.Sleep(15000);
        }

        public void Dispose()
        {
            _tests -= 1;
            if (_tests == 0)
            {
                _sqlserverTest.Dispose();
            }
        }
    }
}
