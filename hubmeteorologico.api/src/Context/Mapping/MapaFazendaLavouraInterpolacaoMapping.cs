using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class MapaFazendaLavouraInterpolacaoMapping
    : IEntityTypeConfiguration<MapaFazendaLavouraInterpolacao>
{
    public void Configure(EntityTypeBuilder<MapaFazendaLavouraInterpolacao> builder)
    {
        builder.ToTable("MapaFazendaLavouraInterpolacao", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.MapaFazendaLavouraId).IsRequired();

        builder.Property(c => c.Poligono).HasColumnType("geometry(Point, 4326)").IsRequired();

        builder.Property(c => c.Centroide).HasColumnType("geometry(Point, 4326)").IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder
            .HasOne(c => c.MapaFazendaLavoura)
            .WithMany()
            .HasForeignKey(c => c.MapaFazendaLavouraId);
    }
}
