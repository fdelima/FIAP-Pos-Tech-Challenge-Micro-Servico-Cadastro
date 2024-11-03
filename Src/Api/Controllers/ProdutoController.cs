using FIAP.Pos.Tech.Challenge.Domain;
using FIAP.Pos.Tech.Challenge.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller dos Produtos cadastrados
    /// </summary>
    [Route("api/cadastro/[Controller]")]
    public class ProdutoController : ApiController
    {
        private readonly IProdutoController _controller;

        /// <summary>
        /// Construtor do controller dos Produtos cadastrados
        /// </summary>
        public ProdutoController(IProdutoController controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Retorna os Produtos cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Produto>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Produto> param = new PagingQueryParam<Produto>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Retorna as categorias dos produtos cadastrados
        /// </summary>
        [HttpGet("categorias")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<KeyValuePair<short, string>>> GetCategorias()
        {
            return await _controller.GetCategoriasAsync();
        }

        /// <summary>
        /// Recupera o Produto cadastrado pelo seu Id
        /// </summary>
        /// <returns>Produto encontrada</returns>
        /// <response code="200">Produto encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Produto cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _controller.FindByIdAsync(id));
        }

        /// <summary>
        ///  Consulta os Produtos cadastrados no sistema da categoria informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos Produtos</param>
        /// <returns>Retorna as Produtos cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos Produtos recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos Produtos cadastrados</response>
        [HttpGet("categoria/{categoria}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Produto>> FindByCategoria(string categoria)
        {
            PagingQueryParam<Produto> param = new PagingQueryParam<Produto>()
            {
                CurrentPage = 1,
                Take = 10,
                ObjFilter = new Produto
                {
                    Categoria = categoria
                }
            };
            return await _controller.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        ///  Consulta os Produtos cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos Produtos</param>
        /// <returns>Retorna as Produtos cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos Produtos recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos Produtos cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Produto>> Consult(PagingQueryParam<Produto> param)
        {
            return await _controller.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o Produto cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Produto cadastrado.</returns>
        /// <response code="200">Produto inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Produto.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Produto model)
        {
            return ExecuteCommand(await _controller.PostAsync(model));
        }

        /// <summary>
        /// Altera o Produto cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Produto cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Produto cadastrado.</returns>
        /// <response code="200">Produto alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Produto.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Produto model)
        {
            return ExecuteCommand(await _controller.PutAsync(id, model));
        }

        /// <summary>
        /// Deleta o Produto cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Produto cadastrado.</param>
        /// <returns>Retorna o result do Produto cadastrado.</returns>
        /// <response code="200">Produto deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Produto.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {
            return ExecuteCommand(await _controller.DeleteAsync(id));
        }

    }
}