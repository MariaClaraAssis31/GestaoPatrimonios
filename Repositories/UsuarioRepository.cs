using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly GestaoPatrimoniosContext _context;

        public UsuarioRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Usuario> Listar()
        {
            return _context.Usuario.OrderBy(usuario => usuario.Nome).ToList();
        }
    }
}
