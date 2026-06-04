using ProjetoCrudPessoa.Domain;

namespace ProjetoCrudPessoa.Repositories
{
    public interface IPessoaRepository
    {
        Task AdicionarAsync(Pessoa pessoa);

        Task<Pessoa?> ObterPorIdAsync(int id);

        Task AtualizarAsync(Pessoa pessoa);

        Task RemoverAsync(Pessoa pessoa);

        Task<List<Pessoa>> ListarAsync(int page, int pageSize);

        Task<int> ContarAsync();

        Task<bool> ExisteCpfAsync(string cpf);

        Task<bool> ExisteCpfEmOutroRegistroAsync(int id, string cpf);
    }
}