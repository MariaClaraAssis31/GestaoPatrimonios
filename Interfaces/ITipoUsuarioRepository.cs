using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        List<TipoUsuario> Listar();
        TipoUsuario BuscarPorId(Guid tipoUsuarioId);
        TipoUsuario BuscarPorNome(string nomeTipo);
        void Adicionar(TipoUsuario tipoUsuario);
        void Atualizar(TipoUsuario tipoUsuario);
    }
}
