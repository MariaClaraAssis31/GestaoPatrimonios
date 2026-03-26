using Gestao_Patrimonio.Applications.Services;
using Gestao_Patrimonio.DTOs.LocalizacaoDto;
using Gestao_Patrimonios.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestao_Patrimonio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        private readonly LocalizacaoService _service;

        public LocalizacaoController(LocalizacaoService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<List<ListarLocalizacaoDto>> Listar()
        {
            List<ListarLocalizacaoDto> localizacoes = _service.Listar();
            return Ok(localizacoes);
        }

        [HttpGet("{Id}")]
        public ActionResult<ListarLocalizacaoDto> BuscarPorId(Guid Id)
        {
            try
            {
                ListarLocalizacaoDto localizacao = _service.BuscarPorId(Id);
                return Ok(localizacao);
            }
            catch(DomainException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Adicionar(CriarLocalizacaoDto dto)
        {
            try
            {
                _service.Adicionar(dto);
                return Created();
            }
            catch(DomainException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public ActionResult Atualizar(Guid Id, CriarLocalizacaoDto dto)
        {
            try
            {
                _service.Atualizar(Id, dto);
                return NoContent();
            }
            catch (DomainException ex)
            {
                return BadRequest(ex.Message);  
            }
        }
    }
}
