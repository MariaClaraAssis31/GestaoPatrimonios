using System;
using System.Collections.Generic;

namespace Gestao_Patrimonio.Domains;

public partial class Bairro
{
    public Guid BairroID { get; set; }

    public string? NomeBairro { get; set; }

    public Guid? CidadeID { get; set; }

    public virtual Cidade? Cidade { get; set; }

    public virtual ICollection<Endereco> Endereco { get; set; } = new List<Endereco>();
}
