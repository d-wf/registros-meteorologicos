using System;

namespace HubMeteorologico.Api.Entities;

public class RegistroMeteorologico
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public int FazendaId { get; set; }
    public int EquipamentoId { get; set; }
    public bool Consolidada { get; set; }
    public double? PressaoAtmosferica { get; set; }
    public double? UmidadeRelativaAr { get; set; }
    public double VolumeChuva { get; set; } = 0.0;
    public double? Temperatura { get; set; }
    public double? DirecaoVento { get; set; }
    public double? VelocidadeVento { get; set; }
    public double? PontoOrvalho { get; set; }
    public double? Bateria { get; set; }
    public double? FolhaMolhada { get; set; }
    public string? Versao { get; set; }
    public double? RadiacaoSolar { get; set; }
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
    public int AnoAgricolaId { get; set; } = 0;
    public double? TemperaturaMaxima { get; set; }
    public double? TemperaturaMinima { get; set; }
    public double? VelocidadeVentoPico { get; set; }
    public double? Evapotranspiracao { get; set; }

    public Fazenda? Fazenda { get; set; }
    public Equipamento? Equipamento { get; set; }
    public AnoAgricola? AnoAgricola { get; set; }
}
