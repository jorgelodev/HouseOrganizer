using AutoMapper;
using HouseOrganizer.DTOs.Casa;
using HouseOrganizer.Entities;
using HouseOrganizer.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HouseOrganizer.Controllers
{
    [ApiController]
    [Route("casa")]
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

        [HttpGet("obter-por-id/{id}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            var casa = _repository.ObterPorId(id);

            if(casa == null)
                return NotFound("Casa não encontrada");

            return Ok(_mapper.Map<CasaDTO>(casa));
        }

        [HttpGet("listar")]
        public IActionResult Listar()
        {
            var casas = _repository.ObterTodos();

            if(casas == null)
                return NotFound("Nenhuma casa cadastrada");

            return Ok(_mapper.Map<List<CasaDTO>>(casas));
        }

        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromBody]CadastrarCasaDTO casaDTO)
        {
            var casa = _mapper.Map<Casa>(casaDTO);

            _repository.Cadastrar(casa);

            return Ok("Casa Cadastrada com sucesso");
        }
        [HttpPut("atualizar")]
        public IActionResult Atualizar([FromBody] AlterarCasaDTO casaDTO)
        {
            var casa = _mapper.Map<Casa>(casaDTO);

            _repository.Atualizar(casa);

            return Ok("Casa Atualizada com sucesso");
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult Deletar([FromRoute] int id)
        {
            _repository.Deletar(id);

            return Ok("Casa excluída com sucesso");
        }

    }
}
