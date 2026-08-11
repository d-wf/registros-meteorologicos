using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class MapaFazendaLavouraMapping : IEntityTypeConfiguration<MapaFazendaLavoura>
{
    public void Configure(EntityTypeBuilder<MapaFazendaLavoura> builder)
    {
        builder.ToTable("MapaFazendaLavoura", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.MapaFazendaId).IsRequired();

        builder.Property(c => c.CodigoLavoura).IsRequired();

        builder.Property(c => c.Area).IsRequired();
        builder.Property(e => e.Poligono).HasColumnType("geometry(Point, 4326)").IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder.HasOne(c => c.MapaFazenda).WithMany().HasForeignKey(c => c.MapaFazendaId);
    }
}
