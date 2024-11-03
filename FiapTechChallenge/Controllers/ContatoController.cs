using Core.Input;
using Core.Interfaces.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FiapTechChallengeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoController : ControllerBase
    {        
        private readonly IContatoService _contatoService;        

        public ContatoController(IContatoService contatoService)
        {
            _contatoService = contatoService;
        }

        #region GET

        /// <summary>
        /// Consulta de contatos por filtro de região
        /// </summary>
        /// <param name="regiao">O região a ser definido. Valores esperados: Sul, Sudeste, Norte, Nordeste e Centro-Oeste.</param>
        /// <returns></returns>        
        /// <response code="200">Sucesso na execução ao retornar os contatos</response>        
        /// <response code="500">Não foi possível retornar as informações</response>
        [HttpGet("RetornaContatoPelaRegiao")]
        public async Task<IActionResult> RetornaContatoPelaRegiao(string regiao)
        {
            try
            {
                var x = await _contatoService.ObterContatoPelaRegiaoAsync(regiao);
                return Ok(x);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retorna todos os contatos cadastrado
        /// </summary>
        /// <returns></returns>        
        /// <response code="200">Sucesso na execução ao retornar os contatos</response>        
        /// <response code="500">Não foi possível retornar as informações</response>
        [HttpGet("RetornaTodosContatos")]
        //[Authorize]
        public async Task<IActionResult> RetornaTodosContatos()
        {
            try
            {
                var contatosDto = await _contatoService.ObterTodosContatoAsync();

                return Ok(contatosDto);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Retorna um contato pelo Id cadastrado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("RetornaContatoPorId")]
        //[Authorize(Roles = PemissaoSistema.Atendimento)]
        [AllowAnonymous]
        public async Task<IActionResult> RetornaContatoPorId(int id)
        {
            try
            {
                return Ok(await _contatoService.ObterPorIdAsync(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion End GET

        #region POST

        /// <summary>
        /// Cadastra um novo contato na base de dados
        /// </summary>
        /// <remarks> 
        /// Exemplo de requisição:
        /// Obs.: Não é necessário enviar o Id nessa requisição
        ///     {
        ///         "Username": "Nome do contato",
        /// 	    "Password": "Senha do contato",
        /// 	    "PermissaoSistema": "Tipo de Permissão"
        ///     }
        /// </remarks>
        /// <param name="input">Objeto contato</param>
        /// <returns>Retorna o contato cadastrado</returns>
        /// <response code="201">contato cadastrado na base de dados</response>
        /// <response code="400">Falha ao processar a requisição</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [HttpPost("CadastraContato")]
        [AllowAnonymous]
        public async Task<IActionResult> CadastraContato(ContatoInput input)
        {
            try
            {
                await _contatoService.CadastraContato(input);
                return Ok();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion End POST

        #region PUT

        /// <summary>
        /// Atualiza um contato por Id
        /// </summary>
        [HttpPut("AtualizacaoContato")]
        public async Task<IActionResult> Put([FromBody] ContatoUpdateInput input)
        {
            try
            {
                await _contatoService.AlterarCadastraContatoAsync(input);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion End PUT

        #region DELETE

        /// <summary>
        /// Deleta um contato por Id
        /// </summary>
        [HttpDelete("DeleteContato")]
        public async Task<IActionResult> DeleteContato(int id)
        {
            try
            {
                await _contatoService.DeletarAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion End DELETE
    }
}
