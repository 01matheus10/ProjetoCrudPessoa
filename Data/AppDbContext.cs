using Microsoft.EntityFrameworkCore;
using ProjetoCrudPessoa.Domain;

namespace ProjetoCrudPessoa.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}