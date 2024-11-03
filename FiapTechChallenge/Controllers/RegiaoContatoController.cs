using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapTechChallengeApi.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class RegiaoContatoController : ControllerBase
    {
        private readonly IRegiaoContatoService _regiaoService;

        public RegiaoContatoController(IRegiaoContatoService regiaoService)
        {
            _regiaoService = regiaoService;
        }

        #region GET

        /// <summary>
        /// Retorna todas as regiões cadastrada
        /// </summary>
        /// <returns></returns>        
        /// <response code="200">Sucesso na execução ao retornar os contatos</response>        
        /// <response code="500">Não foi possível retornar as informações</response>
        [HttpGet("RetornaTodasRegioes")]
        public async Task<IActionResult> RetornaTodasRegioes()
        {
            try
            {
                return Ok(await _regiaoService.ObterTodosDtoAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retorna a região cadastrada pelo DDD
        /// </summary>
        /// <param name="ddd"></param>
        /// <returns></returns>        
        /// <response code="200">Sucesso na execução ao retornar o Id</response>        
        /// <response code="500">Não foi possível retornar o Id</response>
        [HttpGet("RetornaIdPorDDD")]
        [AllowAnonymous]
        public async Task<IActionResult> RetornaIdPorDDD(int ddd)
        {
            try
            {
                return Ok(await _regiaoService.RetornaIdPorDDD(ddd));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        } 

        #endregion End GET
    }
}
