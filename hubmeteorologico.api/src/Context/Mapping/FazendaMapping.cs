using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class FazendaMapping : IEntityTypeConfiguration<Fazenda>
{
    public void Configure(EntityTypeBuilder<Fazenda> builder)
    {
        builder.ToTable("Fazendas", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Codigo).IsRequired();

        builder.Property(c => c.Nome).HasMaxLength(100).IsRequired();

        builder.Property(c => c.Sigla).HasMaxLength(4).IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder.Property(c => c.IdExternoSolinftec);

        builder.Property(c => c.FusoHorarioId);

        builder.Property(c => c.MeteorologiaKhomp).HasDefaultValue(true);

        builder.Property(c => c.MeteorologiaMetos).HasDefaultValue(true);

        builder.Property(c => c.MeteorologiaSolinftec).HasDefaultValue(true);

        builder.Property(c => c.MeteorologiaZeus).HasDefaultValue(true);
    }
}
