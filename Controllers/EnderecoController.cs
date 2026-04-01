using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.EnderecoDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoController : ControllerBase
    {
        private readonly EnderecoService _service;

        public EnderecoController(EnderecoService service)
        {
            _service = service;
        }

        [Authorize]
        [HttpGet]
        public ActionResult<List<ListarEnderecoDto>> Listar()
        {
            List<ListarEnderecoDto> enderecos = _service.Listar();
            return Ok(enderecos);
        }

        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<ListarEnderecoDto> BuscarPorId(Guid id)
        {
            try
            {
                ListarEnderecoDto endereco = _service.BuscarPorId(id);
                return Ok(endereco);
            }
            catch (DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPost]
        public ActionResult Adicionar(CriarEnderecoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Coordenador")]
        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid id, CriarEnderecoDto dto)
        {
            try
            {
                _service.Atualizar(id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
