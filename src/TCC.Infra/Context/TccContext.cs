using Microsoft.EntityFrameworkCore;
using TCC.Negocio.Entidade;

namespace TCC.Infra.Context
{
    public class TccContext : DbContext
    {
        public TccContext(DbContextOptions<TccContext> options)
            : base(options)
        {
        }

        public DbSet<CatalogoPodcast> CatalogosPodcasts { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogoPodcast>().ToTable("CatalogosPodcasts");
        }
    }
}
