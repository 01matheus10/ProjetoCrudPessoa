namespace ProjetoCrudPessoa.DTOs
{
    public class PessoaCreateDto
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public int Idade { get; set; }
        public DateTime DataNascimento { get; set; }
    }
}
