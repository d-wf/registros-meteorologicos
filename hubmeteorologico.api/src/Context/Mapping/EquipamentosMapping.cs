using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class EquipamentosMapping : IEntityTypeConfiguration<Equipamento>
{
    public void Configure(EntityTypeBuilder<Equipamento> builder)
    {
        builder.ToTable("Equipamentos", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FazendaId).IsRequired();

        builder
            .Property(e => e.TipoEquipamento)
            .HasConversion<int>()
            .IsRequired()
            .HasComment("Estação Meteorológica = 1; Pluviômetro = 2");

        builder.Property(c => c.FonteMeteorologica).IsRequired();

        builder.Property(c => c.Codigo).IsRequired();

        builder.Property(c => c.Nome).HasMaxLength(50).IsRequired();

        builder.Property(c => c.Modelo).IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder.Property(c => c.Ativo).HasDefaultValue(true);

        builder.Property(c => c.SedeFazendaId).HasDefaultValue(0);

        builder.Property(e => e.Coordenada).HasColumnType("geometry(Point, 4326)");
    }
}
