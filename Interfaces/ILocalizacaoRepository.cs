using Gestao_Patrimonio.Domains;

namespace Gestao_Patrimonio.Interfaces
{
    public interface ILocalizacaoRepository
    {
        List<Localizacao> Listar();
        Localizacao BuscarPorId(Guid localizacaoId);
        void Adicionar(Localizacao localizacao);
        bool AreaExiste(Guid areaId);
        void Atualizar(Localizacao localizacao);
    }
}
