using Microsoft.EntityFrameworkCore;

namespace TestProject.Infra
{
    public class SqlServerTestFixture : IDisposable
    {
        const string pwd = "SqlServer2019!";
        const string network = "network-cadastro-test";

        //sqlserver
        private const string ImageName = "mcr.microsoft.com/mssql/server:2019-latest";
        private const string DataBaseName = "tech-challenge-micro-servico-cadastro-grupo-71";

        string _port; string _databaseContainerName; string _containerNameMssqlTools;

        public SqlServerTestFixture(string imageNameMssqlTools,
                                    string containerNameMssqlTools,
                                    string databaseContainerName, string port)
        {
            _port = port;
            _databaseContainerName = databaseContainerName;
            _containerNameMssqlTools = containerNameMssqlTools;

            if (DockerManager.UseDocker())
            {
                if (!DockerManager.ContainerIsRunning(databaseContainerName))
                {
                    DockerManager.PullImageIfDoesNotExists(ImageName);
                    DockerManager.KillContainer(databaseContainerName);
                    DockerManager.KillVolume(databaseContainerName);

                    DockerManager.CreateNetWork(network);

                    DockerManager.RunContainerIfIsNotRunning(databaseContainerName,
                        $"run --name {databaseContainerName} " +
                        $"-e ACCEPT_EULA=Y " +
                        $"-e MSSQL_SA_PASSWORD={pwd} " +
                        $"-e MSSQL_PID=Developer " +
                        $"-p {port}:1433 " +
                        $"--network {network} " +
                        $"-d {ImageName}");

                    DockerManager.PullImageIfDoesNotExists(imageNameMssqlTools);
                    DockerManager.KillContainer(containerNameMssqlTools);
                    DockerManager.KillVolume(containerNameMssqlTools);
                    DockerManager.RunContainerIfIsNotRunning(containerNameMssqlTools,
                        $"run --name {containerNameMssqlTools} " +
                        $"--network {network} " +
                        $"-d {imageNameMssqlTools}");
                }
            }
        }

        public FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context GetDbContext()
        {
            while (DockerManager.ContainerIsRunning(_containerNameMssqlTools))
            {
                Thread.Sleep(1000);
            }

            string ConnectionString = $"Server=localhost,{_port}; Database={DataBaseName}; User ID=sa; Password={pwd}; MultipleActiveResultSets=true; TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context>()
                                .UseSqlServer(ConnectionString).Options;

            return new FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Infra.Context(options);
        }

        public void Dispose()
        {
            if (DockerManager.UseDocker())
            {
                DockerManager.KillContainer(_databaseContainerName);
                DockerManager.KillVolume(_databaseContainerName);
            }
            GC.SuppressFinalize(this);
        }
    }
}
