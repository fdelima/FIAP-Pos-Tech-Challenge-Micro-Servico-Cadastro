using System.Net.Http.Headers;

namespace TestProject.Infra
{
    public class ApiTestFixture : IDisposable
    {
        const string port = "5000";
        const string network = "network-cadastro-test";

        //api
        private const string ImageName = "fdelima/fiap-pos-tech-challenge-micro-servico-cadastro-gurpo-71-api:fase4";
        private const string DatabaseContainerName = "api-cadastro-test";
        private const string DataBaseName = "tech-challenge-micro-servico-cadastro-grupo-71";
        private HttpClient _client;

        public ApiTestFixture()
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
                        $"-e ASPNETCORE_ENVIRONMENT=Test " +
                        $"-p {port}:8080 " +
                        $"--network {network} " +
                        $"-d {ImageName}");
                }
            }
        }

        public HttpClient GetClient()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri($"http://localhost:{port}/");
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            return _client;
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
