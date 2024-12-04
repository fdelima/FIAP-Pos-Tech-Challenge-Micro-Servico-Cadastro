using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Entities;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Extensions;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Interfaces;
using FIAP.Pos.Tech.Challenge.Micro.Servico.Cadastro.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FIAP.Pos.Tech.Challenge.Api.Controllers
{
    //TODO: Controller :: 1 - Duplicar esta controller de exemplo e trocar o nome da entidade.
    /// <summary>
    /// Controller dos Clientes cadastrados
    /// </summary>
    [Route("api/cadastro/[Controller]")]
    public class ClienteController : ApiController
    {
        private readonly IController<Cliente> _controller;

        /// <summary>
        /// Construtor do controller dos Clientes cadastrados
        /// </summary>
        public ClienteController(IController<Cliente> controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Retorna os Clientes cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Cliente>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Cliente> param = new PagingQueryParam<Cliente>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Recupera o Cliente cadastrado pelo seu Id
        /// </summary>
        /// <returns>Cliente encontrada</returns>
        /// <response code="200">Cliente encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Cliente cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _controller.FindByIdAsync(id));
        }

        /// <summary>
        /// Recupera o Cliente cadastrado pelo seu cpf
        /// </summary>
        /// <returns>Cliente encontrada</returns>
        /// <response code="200">Cliente encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Cliente cadastrado</response>
        [HttpGet("cpf/{cpf}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Cliente>> FindByCpf(long cpf)
        {
            PagingQueryParam<Cliente> param = new PagingQueryParam<Cliente>()
            {
                CurrentPage = 1,
                Take = 10,
                ObjFilter = new Cliente
                {
                    Cpf = cpf
                }
            };
            return await _controller.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        ///  Consulta os Clientes cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos Clientes</param>
        /// <returns>Retorna as Clientes cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos Clientes recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos Clientes cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Cliente>> Consult(PagingQueryParam<Cliente> param)
        {
            return await _controller.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o Cliente cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Cliente cadastrado.</returns>
        /// <response code="200">Cliente inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Cliente.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Cliente model)
        {
            return ExecuteCommand(await _controller.PostAsync(model));
        }

        /// <summary>
        /// Altera o Cliente cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Cliente cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Cliente cadastrado.</returns>
        /// <response code="200">Cliente alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Cliente.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Cliente model)
        {
            return ExecuteCommand(await _controller.PutAsync(id, model));
        }

        /// <summary>
        /// Deleta o Cliente cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Cliente cadastrado.</param>
        /// <returns>Retorna o result do Cliente cadastrado.</returns>
        /// <response code="200">Cliente deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Cliente.</response>
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