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
    /// Controller dos Dispositivos cadastrados
    /// </summary>
    [Route("api/cadastro/[Controller]")]
    public class DispositivoController : ApiController
    {
        private readonly IController<Dispositivo> _controller;

        /// <summary>
        /// Construtor do controller dos Dispositivos cadastrados
        /// </summary>
        public DispositivoController(IController<Dispositivo> controller)
        {
            _controller = controller;
        }

        /// <summary>
        /// Retorna os Dispositivos cadastrados
        /// </summary>
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Dispositivo>> Get(int currentPage = 1, int take = 10)
        {
            PagingQueryParam<Dispositivo> param = new PagingQueryParam<Dispositivo>() { CurrentPage = currentPage, Take = take };
            return await _controller.GetItemsAsync(param, param.SortProp());
        }

        /// <summary>
        /// Recupera o Dispositivo cadastrado pelo seu Id
        /// </summary>
        /// <returns>Dispositivo encontrada</returns>
        /// <response code="200">Dispositivo encontrada ou nulo</response>
        /// <response code="400">Erro ao recuperar Dispositivo cadastrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> FindById(Guid id)
        {
            return ExecuteCommand(await _controller.FindByIdAsync(id));
        }

        /// <summary>
        ///  Consulta os Dispositivos cadastrados no sistema com o filtro informado.
        /// </summary>
        /// <param name="filter">Filtros para a consulta dos Dispositivos</param>
        /// <returns>Retorna as Dispositivos cadastrados a partir dos parametros informados</returns>
        /// <response code="200">Listagem dos Dispositivos recuperada com sucesso</response>
        /// <response code="400">Erro ao recuperar listagem dos Dispositivos cadastrados</response>
        [HttpPost("consult")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PagingQueryResult<Dispositivo>> Consult(PagingQueryParam<Dispositivo> param)
        {
            return await _controller.ConsultItemsAsync(param, param.ConsultRule(), param.SortProp());
        }

        /// <summary>
        /// Inseri o Dispositivo cadastrado.
        /// </summary>
        /// <param name="model">Objeto contendo as informações para inclusão.</param>
        /// <returns>Retorna o result do Dispositivo cadastrado.</returns>
        /// <response code="200">Dispositivo inserida com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para inserção do Dispositivo.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Post(Dispositivo model)
        {
            return ExecuteCommand(await _controller.PostAsync(model));
        }

        /// <summary>
        /// Altera o Dispositivo cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Dispositivo cadastrado.</param>
        /// <param name="model">Objeto contendo as informações para modificação.</param>
        /// <returns>Retorna o result do Dispositivo cadastrado.</returns>
        /// <response code="200">Dispositivo alterada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para alteração do Dispositivo.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ModelResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Put(Guid id, Dispositivo model)
        {
            return ExecuteCommand(await _controller.PutAsync(id, model));
        }

        /// <summary>
        /// Deleta o Dispositivo cadastrado.
        /// </summary>
        /// <param name="id">Identificador do Dispositivo cadastrado.</param>
        /// <returns>Retorna o result do Dispositivo cadastrado.</returns>
        /// <response code="200">Dispositivo deletada com sucesso.</response>
        /// <response code="400">Erros de validação dos parâmetros para deleção do Dispositivo.</response>
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