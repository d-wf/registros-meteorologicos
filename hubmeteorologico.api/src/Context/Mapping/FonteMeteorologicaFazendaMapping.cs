using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class FonteMeteorologicaFazendaMapping : IEntityTypeConfiguration<FonteMeteorologicaFazenda>
{
    public void Configure(EntityTypeBuilder<FonteMeteorologicaFazenda> builder)
    {
        builder.ToTable("FonteMeteorologicaFazenda", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.FazendaId).IsRequired();

        builder.Property(c => c.FonteMeteorologicaId).IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder.HasOne(c => c.Fazenda).WithMany().HasForeignKey(c => c.FazendaId);

        builder
            .HasOne(c => c.FonteMeteorologica)
            .WithMany()
            .HasForeignKey(c => c.FonteMeteorologicaId);
    }
}
