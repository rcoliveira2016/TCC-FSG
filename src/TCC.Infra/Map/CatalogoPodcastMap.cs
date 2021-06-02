using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TCC.Negocio.Entidade;

namespace TCC.Infra.Map
{
    internal class CatalogoPodcastMap : IEntityTypeConfiguration<CatalogoPodcast>
    {
        public void Configure(EntityTypeBuilder<CatalogoPodcast> builder)
        {
            builder.ToTable("CatalogosPodcasts");

            builder.HasKey(o => o.Id);

            builder.Property(t => t.Nome)
                    .IsRequired();

            builder.Property(t => t.NomeEpisodio)
                    .IsRequired();

            builder.Property(t => t.DataCadastro)
                   .IsRequired();

            builder.Property(t => t.UrlSitePodcast);
            builder.Property(t => t.DataCadastro);

            builder.Property(t => t.Transcricao)
                    .HasColumnType("VARCHAR(max)");

            builder.Property(t => t.Audio)
                    .IsRequired();
        }
    }
}
