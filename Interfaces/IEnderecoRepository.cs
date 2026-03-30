using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface IEnderecoRepository
    {
        List<Endereco> Listar();
        Endereco BuscarPorId(Guid enderecoId);
        void Adicionar(Endereco endereco);
        void Atualizar(Endereco endereco);
        Endereco BuscarPorLogradouroENumero(string Logradouro);
        bool BairroExiste(Guid bairroId);
    }
}
