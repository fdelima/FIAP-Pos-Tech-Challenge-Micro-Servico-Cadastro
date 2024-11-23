using Microsoft.EntityFrameworkCore;

namespace TestProject.IntegrationTest.Infra
{
    public class SqlServerTestFixture : IDisposable
    {
        const string port = "1432";
        const string pwd = "SqlServer2019!";
        const string network = "network-cadastro-test";

        //sqlserver
        private const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";
        private const string DatabaseContainerName = "sqlserver-db-cadastro-test";
        private const string DataBaseName = "tech-challenge-micro-servico-cadastro-grupo-71";
        
        private string ConnectionString = $"Server=localhost,{port}; Database={DataBaseName}; User ID=sa; Password={pwd}; MultipleActiveResultSets=true; TrustServerCertificate=True";

        //mssql-tools
        private const string ImageNameMssqlTools = "fdelima/fiap-pos-techchallenge-micro-servico-cadastro-gurpo-71-scripts-database:fase4-test";
        private const string DatabaseContainerNameMssqlTools = "mssql-tools-cadastro-test";

        public SqlServerTestFixture()
        {
            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(DatabaseContainerName))
                {
                    DockerManager.PullImageIfDoesNotExists(ImageName);
                    DockerManager.KillContainer(DatabaseContainerName);
                    DockerManager.KillVolume(DatabaseContainerName);

                    DockerManager.CreateNetWork(network);

                    DockerManager.RunContainerIfIsNotRunning(DatabaseContainerName,
                        $"run --name {DatabaseContainerName} " +
                        $"-e ACCEPT_EULA=Y " +
                        $"-e MSSQL_SA_PASSWORD={pwd} " +
                        $"-e MSSQL_PID=Developer " +
                        $"-p {port}:1433 " +
                        $"--network {network} " +
                        $"-d {ImageName}");

                    Thread.Sleep(1000);

                    DockerManager.PullImageIfDoesNotExists(ImageNameMssqlTools);
                    DockerManager.KillContainer(DatabaseContainerNameMssqlTools);
                    DockerManager.KillVolume(DatabaseContainerNameMssqlTools);
                    DockerManager.RunContainerIfIsNotRunning(DatabaseContainerNameMssqlTools,
                        $"run --name {DatabaseContainerNameMssqlTools} " +
                        $"--network {network} " +
                        $"-d {ImageNameMssqlTools}");

                    while (DockerManager.ContainerIsRunning(DatabaseContainerNameMssqlTools))
                    {
                        Thread.Sleep(1000);
                    }
                }
            }
        }

        public FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context GetDbContext()
        {
            var options = new DbContextOptionsBuilder<FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context>()
                                .UseSqlServer(ConnectionString).Options;

            return new FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context(options);
        }

        public void Dispose()
        {
            if (DockerManager.UseDocker())
            {
                DockerManager.KillContainer(DatabaseContainerName);
                DockerManager.KillVolume(DatabaseContainerName);
            }
            GC.SuppressFinalize(this);
        }
    }
}
