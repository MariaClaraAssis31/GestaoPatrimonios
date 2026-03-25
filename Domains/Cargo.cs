using System;
using System.Collections.Generic;

namespace Gestao_Patrimonio.Domains;

public partial class Cargo
{
    public Guid CargoID { get; set; }

    public string NomeCargo { get; set; } = null!;

    public virtual ICollection<Usuario> Usuario { get; set; } = new List<Usuario>();
}
