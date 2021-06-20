using Microsoft.EntityFrameworkCore;
using TCC.Infra.Map;
using TCC.Negocio.Entidade;

namespace TCC.Infra.Context
{
    public class TccContext : DbContext
    {
        public TccContext(DbContextOptions<TccContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new CatalogoPodcastMap());
        }
    }
}
