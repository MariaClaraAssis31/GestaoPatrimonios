using System;
using System.Collections.Generic;

namespace Gestao_Patrimonio.Domains;

public partial class Area
{
    public Guid AreaID { get; set; }

    public string NomeArea { get; set; } = null!;

    public virtual ICollection<Localizacao> Localizacao { get; set; } = new List<Localizacao>();
}
