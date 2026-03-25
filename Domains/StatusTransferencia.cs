using System;
using System.Collections.Generic;

namespace Gestao_Patrimonio.Domains;

public partial class StatusTransferencia
{
    public Guid StatusTransferenciaID { get; set; }

    public string NomeStatus { get; set; } = null!;

    public virtual ICollection<SolictacaoTransferencia> SolictacaoTransferencia { get; set; } = new List<SolictacaoTransferencia>();
}
