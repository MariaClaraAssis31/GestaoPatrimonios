using System;
using System.Collections.Generic;

namespace Gestao_Patrimonio.Domains;

public partial class Patrimonio
{
    public Guid PatrimonioID { get; set; }

    public string? Denominacao { get; set; }

    public string? NumerPatrimonio { get; set; }

    public decimal? Valor { get; set; }

    public string? Imagem { get; set; }

    public Guid LocalizacaoID { get; set; }

    public Guid TipoPatimonioID { get; set; }

    public Guid StatusPatrimonioID { get; set; }

    public virtual Localizacao Localizacao { get; set; } = null!;

    public virtual ICollection<LogPatrimonio> LogPatrimonio { get; set; } = new List<LogPatrimonio>();

    public virtual ICollection<SolictacaoTransferencia> SolictacaoTransferencia { get; set; } = new List<SolictacaoTransferencia>();

    public virtual StatusPatrimonio StatusPatrimonio { get; set; } = null!;

    public virtual TipoPatrimonio TipoPatimonio { get; set; } = null!;
}
