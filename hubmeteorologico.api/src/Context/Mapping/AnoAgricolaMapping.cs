using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping
{
    public class AnoAgricolaMapping : IEntityTypeConfiguration<AnoAgricola>
    {
        public void Configure(EntityTypeBuilder<AnoAgricola> builder)
        {
            builder.ToTable("AnosAgricolas", "public");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo).IsRequired();

            builder.Property(c => c.Nome).HasMaxLength(50).IsRequired();

            builder.Property(c => c.CreatorUsername).HasMaxLength(100);

            builder.Property(c => c.ModifierUsername).HasMaxLength(100);

            builder
                .Property(c => c.CreationTime)
                .HasColumnType("timestamp without time zone")
                .IsRequired();

            builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

            builder
                .Property(c => c.DataFinal)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValue(DateTime.MinValue);

            builder
                .Property(c => c.DataInicial)
                .HasColumnType("timestamp without time zone")
                .HasDefaultValue(DateTime.MinValue);
        }
    }
}
