using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.TipoUsuarioDto;
using Gestao_Patrimonio.Repositories;

namespace Gestao_Patrimonio.Applications.Services
{
    public class TipoUsuarioService 
    {
        private readonly TipoUsuarioRepository _repository;
        public TipoUsuarioService(TipoUsuarioRepository repository)
        {
            _repository = repository; 
        }

        public List<ListarTipoUsuarioDto> Listar()
        {
            List<TipoUsuario> tipoUsuarios = _repository.Listar();
            List<ListarTipoUsuarioDto> tipoUsuarioDtos = tipoUsuarios.Select(tipoUsuario = new ListarTipoUsuarioDto()
            {
                NomeTipo = tipoUsuario.NomeTipo
            }).ToList();
        }
    }
}
