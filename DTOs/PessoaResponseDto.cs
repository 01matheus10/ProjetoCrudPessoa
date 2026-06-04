namespace ProjetoCrudPessoa.DTOs
{
    public class PessoaResponseDto
    {
        public int Id { get; set; }

        public string Nome { get; set; } = string.Empty;

        public string CPF { get; set; } = string.Empty;

        public int Idade { get; set; }

        public DateTime DataNascimento { get; set; }

        public int Status { get; set; }
    }
}