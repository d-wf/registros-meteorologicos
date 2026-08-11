using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class FusoHorariosMapping : IEntityTypeConfiguration<FusoHorario>
{
    public void Configure(EntityTypeBuilder<FusoHorario> builder)
    {
        builder.ToTable("FusoHorarios", "public");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Nome).IsRequired();

        builder.Property(c => c.Offset).IsRequired().HasDefaultValue(new TimeSpan(-3, 0, 0)); // corresponde ao '-03:00:00'

        builder.Property(c => c.Localidade).IsRequired();

        builder.Property(c => c.CreatorUsername).HasMaxLength(100);

        builder.Property(c => c.ModifierUsername).HasMaxLength(100);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");
    }
}
