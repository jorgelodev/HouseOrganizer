using HouseOrganizer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HouseOrganizer.Controllers
{
    [ApiController]
    [Route("casa")]
    public class CasaController : ControllerBase
    {
        
        private readonly ILogger<CasaController> _logger;

        public CasaController(ILogger<CasaController> logger)
        {
            _logger = logger;
        }

        [HttpGet("obter-por-id/{id}")]
        public IActionResult ObterPorId([FromRoute] int id)
        {
            return Ok(id);
        }

        [HttpGet("listar")]
        public IActionResult Listar()
        {
            return Ok();
        }
        [HttpPost("cadastrar")]
        public IActionResult Cadastrar([FromForm]Casa casa)
        {
            return Ok(casa);
        }
        [HttpPut("atualizar")]
        public IActionResult Atualizar(Casa casa)
        {
            return Ok(casa);
        }

        [HttpDelete("deletar")]
        public IActionResult Deletar(Casa casa)
        {
            return Ok(casa);
        }

    }
}
