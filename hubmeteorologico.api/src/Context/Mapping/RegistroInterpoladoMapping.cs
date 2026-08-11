using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HubMeteorologico.Api.Context.Mapping;

public class RegistroInterpoladoMapping : IEntityTypeConfiguration<RegistroInterpolado>
{
    public void Configure(EntityTypeBuilder<RegistroInterpolado> builder)
    {
        builder.ToTable("RegistrosInterpolados", "public");

        builder.HasKey(c => new
        {
            c.DataHora,
            c.FazendaId,
            c.MapaFazendaId,
            c.MapaFazendaLavouraId,
            c.MapaFazendaLavouraInterpolacaoId,
            c.AnoAgricolaId,
        });

        builder.Property(c => c.Coordenada).HasColumnType("geometry(Point, 4326)").IsRequired();

        builder.Property(c => c.Consolidada).IsRequired();

        builder.Property(c => c.VolumeChuva).IsRequired();

        builder.Property(c => c.PressaoAtmosferica).HasDefaultValue(0.0);

        builder.Property(c => c.UmidadeRelativaAr).HasDefaultValue(0.0);

        builder.Property(c => c.Temperatura).HasDefaultValue(0.0);

        builder.Property(c => c.DirecaoVento).HasDefaultValue(0.0);

        builder.Property(c => c.VelocidadeVento).HasDefaultValue(0.0);

        builder.Property(c => c.PontoOrvalho).HasDefaultValue(0.0);

        builder.Property(c => c.FolhaMolhada).HasDefaultValue(0.0);

        builder.Property(c => c.RadiacaoSolar).HasDefaultValue(0.0);

        builder.Property(c => c.CreatorUsername);

        builder.Property(c => c.ModifierUsername);

        builder
            .Property(c => c.CreationTime)
            .HasColumnType("timestamp without time zone")
            .IsRequired();

        builder.Property(c => c.ModificationTime).HasColumnType("timestamp without time zone");

        builder.Property(c => c.Evapotranspiracao).HasDefaultValue(0.0);

        builder.Property(c => c.TemperaturaMaxima).HasDefaultValue(0.0);

        builder.Property(c => c.TemperaturaMinima).HasDefaultValue(0.0);

        builder.Property(c => c.VelocidadeVentoPico).HasDefaultValue(0.0);
        builder.Property(c => c.DataHora).HasColumnType("timestamp without time zone");
    }
}
