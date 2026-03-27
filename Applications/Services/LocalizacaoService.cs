using Gestao_Patrimonio.Applications.Regras;
using Gestao_Patrimonio.Domains;
using Gestao_Patrimonio.DTOs.LocalizacaoDto;
using Gestao_Patrimonio.Interfaces;
using Gestao_Patrimonios.Exceptions;

namespace Gestao_Patrimonio.Applications.Services
{
    public class LocalizacaoService
    {
        private readonly ILocalizacaoRepository _repository;

        public LocalizacaoService(ILocalizacaoRepository repository)
        {
            _repository = repository;
        }

        public List<ListarLocalizacaoDto> Listar()
        {
            List<Localizacao> localizacoes = _repository.Listar();

            List<ListarLocalizacaoDto> localizacoesDto = localizacoes.Select(localizacao => new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            }).ToList();

            return localizacoesDto;
        }

        public ListarLocalizacaoDto BuscarPorId(Guid localizacaoId)
        {
            Localizacao localizacao = _repository.BuscarPorId(localizacaoId);

            if(localizacao == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            return new ListarLocalizacaoDto
            {
                LocalizacaoID = localizacao.LocalizacaoID,
                NomeLocal = localizacao.NomeLocal,
                LocalSAP = localizacao.LocalSAP,
                DescricaoSAP = localizacao.DescricaoSAP,
                AreaID = localizacao.AreaID
            };
        }

        public void Adicionar(CriarLocalizacaoDto dto)
        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localExistente = _repository.BuscarPorNome(dto.NomeLocal, dto.AreaID);

            if(localExistente != null)
            {
                throw new DomainException("Já existe um local cadastrado com esse nome nessa área");
            }

            if(!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Área informada não existe");
            }

            Localizacao localizacao = new Localizacao
            {
                NomeLocal = dto.NomeLocal,
                LocalSAP = dto.LocalSAP,
                DescricaoSAP = dto.DescricaoSAP,
                AreaID = dto.AreaID
            };

             _repository.Adicionar(localizacao);
        }

        public void Atualizar(Guid localizacaoId,  CriarLocalizacaoDto dto)

        {
            Validar.ValidarNome(dto.NomeLocal);

            Localizacao localizacaoBanco = _repository.BuscarPorId
                (localizacaoId);

            if(localizacaoBanco == null)
            {
                throw new DomainException("Localização não encontrada");
            }

            Localizacao localExistente = _repository.BuscarPorNome(dto.NomeLocal, dto.AreaID);

            if (localExistente != null)
            {
                throw new DomainException("Já existe um local cadastrado com esse nome nessa área");
            }

            if (!_repository.AreaExiste(dto.AreaID))
            {
                throw new DomainException("Área informada não existe");
            }

            localizacaoBanco.NomeLocal = dto.NomeLocal;
            localizacaoBanco.LocalSAP = dto.LocalSAP;
            localizacaoBanco.DescricaoSAP = dto.DescricaoSAP;
            localizacaoBanco.AreaID = dto.AreaID;

            _repository.Atualizar(localizacaoBanco);
        }
    }
}
