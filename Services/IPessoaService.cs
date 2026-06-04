using ProjetoCrudPessoa.DTOs;

namespace ProjetoCrudPessoa.Services
{
    public interface IPessoaService
    {
        Task AdicionarAsync(PessoaCreateDto dto);

        Task<bool> AtualizarAsync(int id, PessoaPatchDto dto);

        Task<bool> RemoverAsync(int id);

        Task<object> ListarAsync(int page, int pageSize);
    }
}