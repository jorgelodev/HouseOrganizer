using AutoMapper;
using HouseOrganizer.DTOs.Casa;
using HouseOrganizer.Entities;
using HouseOrganizer.Entities.Enums;
using HouseOrganizer.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HouseOrganizer.Controllers
{
    [ApiController]
    [Route("casa")]
    [Authorize]
    public class CasaController : ControllerBase
    {

        private readonly ILogger<CasaController> _logger;
        private readonly IMapper _mapper;
        private readonly ICasaRepository _repository;

        public CasaController(ILogger<CasaController> logger, IMapper mapper, ICasaRepository repository)
        {
            _logger = logger;
            _mapper = mapper;
            _repository = repository;
        }
        /// <summary>
        /// Localiza Casa por Id.
        /// Só pode ser acessado por Dono.
        /// </summary>
        /// <param name="id">Id da Casa</param>
        /// <returns>Retorna DTO da Casa</returns>
        /// <remarks>
        /// 
        /// Retorna DTO da Casa
        /// 
        /// </remarks>
        /// <response code="200">Retorna Sucesso</response>
        /// <response code="401">Não Autenticado</response>
        /// <response code="403">Não Autorizado</response>
        [HttpGet("obter-por-id/{id}")]
        [Authorize(Roles = Permissoes.Dono)]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            var casa = _repository.ObterPorId(id);

            if (casa == null)
                return NotFound("Casa não encontrada");

            return Ok(_mapper.Map<CasaDTO>(casa));
        }
        /// <summary>
        /// Lista todas as casas.
        /// Recurso público.
        /// </summary>
        /// <returns>Retorna uma lista de todas as Casas cadastradas</returns>
        /// <remarks>
        /// 
        /// Retorna uma lista de todas as Casas cadastradas
        /// 
        /// </remarks>
        [HttpGet("listar")]
        [AllowAnonymous]        
        public IActionResult Listar()
        {
            var casas = _repository.ObterTodos();

            if (casas == null)
                return NotFound("Nenhuma casa cadastrada");

            return Ok(_mapper.Map<List<CasaDTO>>(casas));
        }
        /// <summary>
        /// Cadastra uma casa.
        /// </summary>
        /// <param name="casaDTO">DTO que Representa uma casa durante o Cadastro</param>
        /// <returns>Retorna mensagem de sucesso</returns>
        /// <remarks>
        /// 
        /// Retorna mensagem de sucesso
        /// 
        /// </remarks>
        [HttpPost("cadastrar")]
        [Authorize(Roles = $"{Permissoes.Administrador},{Permissoes.Dono}")]
        public IActionResult Cadastrar([FromBody]CadastrarCasaDTO casaDTO)
        {
            var casa = _mapper.Map<Casa>(casaDTO);

            _repository.Cadastrar(casa);

            return Ok("Casa Cadastrada com sucesso");
        }

        /// <summary>
        /// Atualiza Casa
        /// </summary>
        /// <param name="casaDTO">DTO que Representa uma casa durante a Atualização</param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// Retorna mensagem de sucesso
        /// 
        /// </remarks>
        [HttpPut("atualizar")]
        [Authorize(Roles = Permissoes.Administrador)]
        public IActionResult Atualizar([FromBody] AlterarCasaDTO casaDTO)
        {
            var casa = _mapper.Map<Casa>(casaDTO);

            _repository.Atualizar(casa);

            return Ok("Casa Atualizada com sucesso");
        }

        /// <summary>
        /// Deleta uma casa
        /// </summary>
        /// <param name="id">Id da Casa</param>
        /// <returns>Retorna mensagem de sucesso</returns>
        /// <remarks>
        /// 
        /// Retorna mensagem de sucesso
        /// 
        /// </remarks>
        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            _repository.Deletar(id);

            return Ok("Casa excluída com sucesso");
        }

    }
}
