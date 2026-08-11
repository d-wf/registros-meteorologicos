using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class FonteMeteorologicaMapping : IEntityTypeConfiguration<FonteMeteorologica>
{
    public void Configure(EntityTypeBuilder<FonteMeteorologica> builder)
    {
        builder.ToTable("FonteMeteorologica", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome).IsRequired().HasDefaultValue(string.Empty);

        builder.Property(c => c.TipoFonteMeteorologica).IsRequired();

        builder.Property(c => c.Usuario).IsRequired();

        builder.Property(c => c.Senha).IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");
    }
}
