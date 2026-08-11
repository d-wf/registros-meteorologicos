using System;
using NetTopologySuite.Geometries;

namespace HubMeteorologico.Api.Entities;

public class RegistroInterpolado
{
    public DateTime DataHora { get; set; }
    public int FazendaId { get; set; }
    public int MapaFazendaId { get; set; }
    public int MapaFazendaLavouraId { get; set; }
    public int MapaFazendaLavouraInterpolacaoId { get; set; }
    public int AnoAgricolaId { get; set; }
    public Point Coordenada { get; set; } = default!;
    public bool Consolidada { get; set; }
    public double VolumeChuva { get; set; }
    public double PressaoAtmosferica { get; set; } = 0.0;
    public double UmidadeRelativaAr { get; set; } = 0.0;
    public double Temperatura { get; set; } = 0.0;
    public double DirecaoVento { get; set; } = 0.0;
    public double VelocidadeVento { get; set; } = 0.0;
    public double PontoOrvalho { get; set; } = 0.0;
    public double FolhaMolhada { get; set; } = 0.0;
    public double RadiacaoSolar { get; set; } = 0.0;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
    public int? SedeFazendaId { get; set; }
    public double Evapotranspiracao { get; set; } = 0.0;
    public double TemperaturaMaxima { get; set; } = 0.0;
    public double TemperaturaMinima { get; set; } = 0.0;
    public double VelocidadeVentoPico { get; set; } = 0.0;

    public Fazenda? Fazenda { get; set; }
    public MapaFazenda? MapaFazenda { get; set; }
    public MapaFazendaLavoura? MapaFazendaLavoura { get; set; }
    public MapaFazendaLavouraInterpolacao? MapaFazendaLavouraInterpolacao { get; set; }
    public AnoAgricola? AnoAgricola { get; set; }
}
