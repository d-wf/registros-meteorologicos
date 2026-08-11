using HubMeteorologico.Api.Enums;
using NetTopologySuite.Geometries;

namespace HubMeteorologico.Api.Models;

public class EquipamentoBaseUpdateRequest
{
    public int FazendaId { get; set; }
    public int FonteMeteorologica { get; set; }
    public string Codigo { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public bool Ativo { get; set; } = true;
    public int SedeFazendaId { get; set; } = 0;
}
