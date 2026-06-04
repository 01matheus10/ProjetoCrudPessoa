using Microsoft.EntityFrameworkCore;
using ProjetoCrudPessoa.Data;
using ProjetoCrudPessoa.Domain;

namespace ProjetoCrudPessoa.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly AppDbContext _context;

        public PessoaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Pessoa pessoa)
        {
            await _context.Pessoas.AddAsync(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task<Pessoa?> ObterPorIdAsync(int id)
        {
            return await _context.Pessoas
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AtualizarAsync(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Pessoa pessoa)
        {
            pessoa.Status = 0;

            _context.Pessoas.Update(pessoa);

            await _context.SaveChangesAsync();
        }

        public async Task<List<Pessoa>> ListarAsync(
            int page,
            int pageSize,
            string? nome,
            string? cpf)
        {
            var query = _context.Pessoas
                .Where(x => x.Status == 1)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(x =>
                    x.Nome.Contains(nome));
            }

            if (!string.IsNullOrWhiteSpace(cpf))
            {
                query = query.Where(x =>
                    x.CPF == cpf);
            }

            return await query
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync(
            string? nome,
            string? cpf)
        {
            var query = _context.Pessoas
                .Where(x => x.Status == 1)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                query = query.Where(x =>
                    x.Nome.Contains(nome));
            }

            if (!string.IsNullOrWhiteSpace(cpf))
            {
                query = query.Where(x =>
                    x.CPF == cpf);
            }

            return await query.CountAsync();
        }

        public async Task<bool> ExisteCpfAsync(string cpf)
        {
            return await _context.Pessoas
                .AnyAsync(x => x.CPF == cpf && x.Status == 1);
        }

        public async Task<bool> ExisteCpfEmOutroRegistroAsync(int id, string cpf)
        {
            return await _context.Pessoas
                .AnyAsync(x => x.Id != id &&
                               x.CPF == cpf &&
                               x.Status == 1);
        }
    }
}