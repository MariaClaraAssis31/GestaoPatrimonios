using Gestao_Patrimonio.Contexts;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.Interfaces;

namespace Gestao_Patrimonio.Repositories
{
    public class EnderecoRepository : IEnderecoRepository
    {
        private readonly GestaoPatrimoniosContext _context;
        public EnderecoRepository(GestaoPatrimoniosContext context)
        {
            _context = context;
        }

        public List<Endereco> Listar()
        {
            return _context.Endereco.OrderBy(logradouro => logradouro.Logradouro).ToList();
        }

        public Endereco BuscarPorId(Guid enderecoId)
        {
            return _context.Endereco.Find(enderecoId);
        }

        public void Adicionar(Endereco endereco)
        {
            _context.Endereco.Add(endereco);
            _context.SaveChanges();
        }

        public void Atualizar(Endereco endereco)
        {
            if (endereco == null)
            {
                return;
            }
            Endereco enderecoBanco = _context.Endereco.Find(endereco.EnderecoID);

            if (enderecoBanco == null)
            {
                return;
            }

            enderecoBanco.Logradouro = endereco.Logradouro;
            _context.SaveChanges();
        }

        public Endereco BuscarPorLogradouroENumero(string logradouro, int? numero, Guid enderecoId)
        {
            return _context.Endereco.FirstOrDefault(endereco =>
            endereco.Logradouro.ToLower() == logradouro.ToLower() &&
            endereco.Numero == numero &&
            endereco.EnderecoID == enderecoId);
        }

        public bool BairroExiste(Guid bairroId)
        {
            return _context.Bairro.Any(bairro => bairro.BairroID == bairroId);
        }
    }
}