using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class RegistroMeteorologicoMapping : IEntityTypeConfiguration<RegistroMeteorologico>
{
    public void Configure(EntityTypeBuilder<RegistroMeteorologico> builder)
    {
        builder.ToTable("RegistrosMeteorologicos", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.DataHora).IsRequired();

        builder.Property(c => c.FazendaId).IsRequired();

        builder.Property(c => c.EquipamentoId).IsRequired();

        builder.Property(c => c.Consolidada).IsRequired();

        builder.Property(c => c.VolumeChuva).HasDefaultValue(0.0);

        builder.Property(c => c.AnoAgricolaId).HasDefaultValue(0);

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder.HasOne(c => c.Fazenda).WithMany().HasForeignKey(c => c.FazendaId);

        builder.HasOne(c => c.Equipamento).WithMany().HasForeignKey(c => c.EquipamentoId);

        builder.HasOne(c => c.AnoAgricola).WithMany().HasForeignKey(c => c.AnoAgricolaId);
    }
}
