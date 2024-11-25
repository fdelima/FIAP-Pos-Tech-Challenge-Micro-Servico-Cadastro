using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;
using TestProject.Infra;
using Xunit.Gherkin.Quick;

namespace TestProject.ComponenteTest
{
    /// <summary>
    /// Classe de teste.
    /// </summary>
    [FeatureFile("./BDD/Features/ControlarClientes.feature")]
    public class ClienteControllerTest : Feature, IClassFixture<ComponentTestsBase>
    {
        private readonly ApiTestFixture _apiTest;
        private ModelResult expectedResult;
        Cliente _Cliente;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ClienteControllerTest(ComponentTestsBase data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public Cliente Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Recebendo um Cliente na lanchonete")]
        public void PrepararCliente()
        {
            _Cliente = new Cliente
            {
                Nome = $"Fernando Lima {DateTime.Now}",
                Email = "fiap@tech.com",
                Cpf = 12345678908
            };
        }

        [And(@"Adicionar o Cliente")]
        public async Task AdicionarCliente()
        {
            expectedResult = ModelResultFactory.InsertSucessResult<Cliente>(_Cliente);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/cadastro/Cliente", _Cliente);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);

            _Cliente = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);

            Assert.True(true);
        }

        [And(@"Encontrar o Cliente")]
        public async Task EncontrarCliente()
        {
            expectedResult = ModelResultFactory.SucessResult(_Cliente);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/cadastro/Cliente/{_Cliente.IdCliente}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Cliente = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [And(@"Alterar o Cliente")]
        public async Task AlterarCliente()
        {
            expectedResult = ModelResultFactory.UpdateSucessResult<Cliente>(_Cliente);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/cadastro/Cliente/{_Cliente.IdCliente}", _Cliente);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Cliente = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [When(@"Consultar o Cliente")]
        public async Task ConsultarCliente()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/cadastro/Cliente/consult", new PagingQueryParam<Cliente> { ObjFilter = _Cliente });

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [Then(@"posso deletar o Cliente")]
        public async Task DeletarCliente()
        {
            expectedResult = ModelResultFactory.DeleteSucessResult<Cliente>();

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/cadastro/Cliente/{_Cliente.IdCliente}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Cliente = null;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }
    }
}
