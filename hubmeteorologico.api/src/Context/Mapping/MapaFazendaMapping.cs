using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class MapaFazendaMapping : IEntityTypeConfiguration<MapaFazenda>
{
    public void Configure(EntityTypeBuilder<MapaFazenda> builder)
    {
        builder.ToTable("MapaFazenda", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FazendaId).IsRequired();

        builder.Property(c => c.AnoAgricolaId).IsRequired();

        builder.Property(c => c.Centroide).HasColumnType("geometry(Point, 4326)").IsRequired();

        builder.Property(c => c.Envelope).HasColumnType("geometry(Point, 4326)").IsRequired();

        builder
            .Property(c => c.EnvelopeInterpolacao)
            .HasColumnType("geometry(Point, 4326)")
            .IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder.Property(c => c.SedeFazendaId).HasDefaultValue(0);

        builder.HasOne(c => c.Fazenda).WithMany().HasForeignKey(c => c.FazendaId);

        builder.HasOne(c => c.AnoAgricola).WithMany().HasForeignKey(c => c.AnoAgricolaId);
    }
}
