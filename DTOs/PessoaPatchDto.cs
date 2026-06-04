namespace ProjetoCrudPessoa.DTOs
{
    public class PessoaPatchDto
    {
        public string? Nome { get; set; }

        public string? CPF { get; set; }

        public int? Idade { get; set; }

        public DateTime? DataNascimento { get; set; }
    }
}