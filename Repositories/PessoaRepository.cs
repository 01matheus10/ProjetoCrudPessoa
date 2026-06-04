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

        public async Task<List<Pessoa>> ListarAsync(int page, int pageSize)
        {
            return await _context.Pessoas
                .Where(x => x.Status == 1)
                .OrderBy(x => x.Id)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> ContarAsync()
        {
            return await _context.Pessoas
                .CountAsync(x => x.Status == 1);
        }
    }
}