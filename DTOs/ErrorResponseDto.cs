namespace ProjetoCrudPessoa.DTOs
{
    public class ErrorResponseDto
    {
        public bool Sucesso { get; set; } = false;

        public string Mensagem { get; set; } = string.Empty;

        public List<string> Erros { get; set; } = new();
    }
}