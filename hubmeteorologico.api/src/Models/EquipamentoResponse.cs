using HubMeteorologico.Api.Entities;
using HubMeteorologico.Api.Enums;
using NetTopologySuite.Geometries;

namespace HubMeteorologico.Api.Models;

public class EquipamentoResponse
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
    public bool Ativo { get; set; } = true;
    public int SedeFazendaId { get; set; } = 0;

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public static List<EquipamentoResponse> Parse(List<Equipamento> equipamentos) =>
        equipamentos.Select(e => EquipamentoResponse.Parse(e)).ToList();

    public static EquipamentoResponse Parse(Equipamento e)
    {
        return new EquipamentoResponse
        {
            Id = e.Id,
            FazendaId = e.FazendaId,
            TipoEquipamento = e.TipoEquipamento,
            FonteMeteorologica = e.FonteMeteorologica,
            Codigo = e.Codigo,
            Nome = e.Nome,
            Modelo = e.Modelo,
            CreatorUsername = e.CreatorUsername,
            CreationTime = e.CreationTime,
            ModificationTime = e.ModificationTime,
            ModifierUsername = e.ModifierUsername,
            Ativo = e.Ativo,
            SedeFazendaId = e.SedeFazendaId,
            Latitude = e.Coordenada?.Y,
            Longitude = e.Coordenada?.X,
        };
    }
}
