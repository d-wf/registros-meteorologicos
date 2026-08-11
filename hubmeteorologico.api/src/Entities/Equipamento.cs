using System;
using HubMeteorologico.Api.Enums;
using NetTopologySuite.Geometries;

namespace HubMeteorologico.Api.Entities;

public class Equipamento
{
    public int Id { get; set; }
    public int FazendaId { get; set; }
    public TipoEquipamento TipoEquipamento { get; set; }
    public int FonteMeteorologica { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Nome { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
    public Point? Coordenada { get; set; }
    public bool Ativo { get; set; } = true;
    public int SedeFazendaId { get; set; } = 0;
}
