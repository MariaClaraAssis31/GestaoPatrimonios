using System;
using System.Collections.Generic;

namespace Gestao_Patrimonio.Domains;

public partial class TipoAlteracao
{
    public Guid TipoAlteracaoID { get; set; }

    public string? NomeTipo { get; set; }
}
