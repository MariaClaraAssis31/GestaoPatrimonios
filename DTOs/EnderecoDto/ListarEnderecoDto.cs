namespace Gestao_Patrimonio.DTOs.EnderecoDto
{
    public class ListarEnderecoDto
    {
        public Guid EnderecoID { get; set; }
        public string Logradouro { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string Complemento { get; set; } = string.Empty;
    }
}
