﻿namespace TestProject.Infra
{
    public class ComponentTestsBase : IDisposable
    {
        private readonly SqlServerTestFixture _sqlserverTest;
        internal readonly ApiTestFixture _apiTest;
        private static int _tests = 0;

        public ComponentTestsBase()
        {
            _tests += 1;
            _sqlserverTest = new SqlServerTestFixture(
                imageNameMssqlTools: "fdelima/fiap-pos-techchallenge-micro-servico-cadastro-gurpo-71-scripts-database:fase4-component-test",
                containerNameMssqlTools: "mssql-tools-cadastro-component-test",
                databaseContainerName: "sqlserver-db-cadastro-component-test", port: "1428");
            _apiTest = new ApiTestFixture();

            //Aguardar download da image
            Thread.Sleep(15000);
        }

        public void Dispose()
        {
            _tests -= 1;
            if (_tests == 0)
            {
                _sqlserverTest.Dispose();
                _apiTest.Dispose();
            }
        }
    }
}
