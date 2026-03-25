using System.ComponentModel.DataAnnotations;

namespace Gestao_Patrimonio.DTOs.AreaDto
{
    public class CriarAreaDto
    {
        [Required(ErrorMessage = "O nome da área é obrigatório. ")]
       [StringLength(50, ErrorMessage = "O nome da área deve ter no máximo 50 caracteres. ")]

        public string NomeArea { get; set; } = string.Empty;
        //string.Empty significa que o nulo é extremamente proibdo
       // string? significa que pode ser null
       // null? significa que não é tão confiavel
    }
}
