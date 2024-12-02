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
    [FeatureFile("./BDD/Features/ControlarProdutos.feature")]
    public class ProdutoControllerTest : Feature, IClassFixture<ComponentTestsBase>
    {
        private readonly ApiTestFixture _apiTest;
        private ModelResult expectedResult;
        Produto _Produto;

        /// <summary>
        /// Construtor da classe de teste.
        /// </summary>
        public ProdutoControllerTest(ComponentTestsBase data)
        {
            _apiTest = data._apiTest;
        }
        private class ActionResult
        {
            public List<string> Messages { get; set; }
            public List<string> Errors { get; set; }
            public Produto Model { get; set; }
            public bool IsValid { get; set; }
        }

        [Given(@"Recebendo um Produto na lanchonete")]
        public void PrepararProduto()
        {
            _Produto = new Produto
            {
                Nome = $"X-Burger {DateTime.Now}",
                Preco = 15,
                Descricao = "Sem tomate",
                Categoria = "LANCHE"
            };
        }

        [And(@"Adicionar o Produto")]
        public async Task AdicionarProduto()
        {
            expectedResult = ModelResultFactory.InsertSucessResult<Produto>(_Produto);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/cadastro/Produto", _Produto);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);

            _Produto = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);

            Assert.True(true);
        }

        [And(@"Encontrar o Produto")]
        public async Task EncontrarProduto()
        {
            expectedResult = ModelResultFactory.SucessResult(_Produto);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.GetAsync(
                $"api/cadastro/Produto/{_Produto.IdProduto}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Produto = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [And(@"Alterar o Produto")]
        public async Task AlterarProduto()
        {
            expectedResult = ModelResultFactory.UpdateSucessResult<Produto>(_Produto);

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/cadastro/Produto/{_Produto.IdProduto}", _Produto);

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Produto = actualResult.Model;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }

        [When(@"Consultar o Produto")]
        public async Task ConsultarProduto()
        {
            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.PostAsJsonAsync(
                $"api/cadastro/Produto/consult", new PagingQueryParam<Produto> { ObjFilter = _Produto });

            var responseContent = await response.Content.ReadAsStringAsync();
            dynamic actualResult = JsonConvert.DeserializeObject(responseContent);

            Assert.True(actualResult.content != null);
        }

        [Then(@"posso deletar o Produto")]
        public async Task DeletarProduto()
        {
            expectedResult = ModelResultFactory.DeleteSucessResult<Produto>();

            var client = _apiTest.GetClient();
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/cadastro/Produto/{_Produto.IdProduto}");

            var responseContent = await response.Content.ReadAsStringAsync();
            var actualResult = JsonConvert.DeserializeObject<ActionResult>(responseContent);
            _Produto = null;

            Assert.Equal(expectedResult.IsValid, actualResult.IsValid);
            Assert.Equal(expectedResult.Messages, actualResult.Messages);
            Assert.Equal(expectedResult.Errors, actualResult.Errors);
        }
    }
}
