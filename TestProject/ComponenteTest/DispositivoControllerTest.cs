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
    [FeatureFile("./BDD/Features/ControlarDispositivos.feature")]
    public class DispositivoControllerTest : Feature, IClassFixture<ComponentTestsBase>
    {
        private readonly ApiTestFixture _apiTest;
        private ModelResult expectedResult;
        Dispositivo _Dispositivo;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public DispositivoControllerTest(ComponentTestsBase data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public Dispositivo Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Recebendo um Dispositivo na lanchonete")]
        public void PrepararDispositivo()
        {
            _Dispositivo = new Dispositivo
            {
                Identificador = $"{DateTime.Now}"
            };
        }

        [And(@"Adicionar o Dispositivo")]
        public async Task AdicionarDispositivo()
        {
            expectedResult = ModelResultFactory.InsertSucessResult<Dispositivo>(_Dispositivo);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/cadastro/Dispositivo", _Dispositivo);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);

            _Dispositivo = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);

            Assert.True(true);
        }

        [And(@"Encontrar o Dispositivo")]
        public async Task EncontrarDispositivo()
        {
            expectedResult = ModelResultFactory.SucessResult(_Dispositivo);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/cadastro/Dispositivo/{_Dispositivo.IdDispositivo}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Dispositivo = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [And(@"Alterar o Dispositivo")]
        public async Task AlterarDispositivo()
        {
            expectedResult = ModelResultFactory.UpdateSucessResult<Dispositivo>(_Dispositivo);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/cadastro/Dispositivo/{_Dispositivo.IdDispositivo}", _Dispositivo);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Dispositivo = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [When(@"Consultar o Dispositivo")]
        public async Task ConsultarDispositivo()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/cadastro/Dispositivo/consult", new PagingQueryParam<Dispositivo> { ObjFilter = _Dispositivo });

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [Then(@"posso deletar o Dispositivo")]
        public async Task DeletarDispositivo()
        {
            expectedResult = ModelResultFactory.DeleteSucessResult<Dispositivo>();

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/cadastro/Dispositivo/{_Dispositivo.IdDispositivo}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Dispositivo = null;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }
    }
}
