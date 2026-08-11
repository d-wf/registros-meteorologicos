using HubMeteorologico.Api.Context.Mapping;
using HubMeteorologico.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace HubMeteorologico.Api.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }

    // DbSets
    public DbSet<AnoAgricola> AnosAgricolas { get; set; }
    public DbSet<Equipamento> Equipamentos { get; set; }
    public DbSet<Fazenda> Fazendas { get; set; }
    public DbSet<FonteMeteorologica> FonteMeteorologica { get; set; }
    public DbSet<FonteMeteorologicaFazenda> FonteMeteorologicaFazenda { get; set; }
    public DbSet<FusoHorario> FusoHorarios { get; set; }
    public DbSet<MapaFazenda> MapaFazenda { get; set; }
    public DbSet<MapaFazendaLavoura> MapaFazendaLavoura { get; set; }
    public DbSet<MapaFazendaLavouraInterpolacao> MapaFazendaLavouraInterpolacao { get; set; }
    public DbSet<RegistroInterpolado> RegistrosInterpolados { get; set; }
    public DbSet<RegistroMeteorologico> RegistrosMeteorologicos { get; set; }
    public DbSet<SedeFazenda> SedesFazenda { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AnoAgricolaMapping());
        modelBuilder.ApplyConfiguration(new EquipamentosMapping());
        modelBuilder.ApplyConfiguration(new FazendaMapping());
        modelBuilder.ApplyConfiguration(new FonteMeteorologicaMapping());
        modelBuilder.ApplyConfiguration(new FonteMeteorologicaFazendaMapping());
        modelBuilder.ApplyConfiguration(new FusoHorariosMapping());
        modelBuilder.ApplyConfiguration(new MapaFazendaMapping());
        modelBuilder.ApplyConfiguration(new MapaFazendaLavouraMapping());
        modelBuilder.ApplyConfiguration(new MapaFazendaLavouraInterpolacaoMapping());
        modelBuilder.ApplyConfiguration(new RegistroInterpoladoMapping());
        modelBuilder.ApplyConfiguration(new RegistroMeteorologicoMapping());
        modelBuilder.ApplyConfiguration(new SedesFazendaMapping());

        base.OnModelCreating(modelBuilder);
    }
}
