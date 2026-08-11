using System;

namespace HubMeteorologico.Api.Entities;

public class Fazenda
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Sigla { get; set; } = string.Empty;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
    public string? IdExternoSolinftec { get; set; }
    public int? FusoHorarioId { get; set; }
    public bool MeteorologiaKhomp { get; set; } = true;
    public bool MeteorologiaMetos { get; set; } = true;
    public bool MeteorologiaSolinftec { get; set; } = true;
    public bool MeteorologiaZeus { get; set; } = true;
}
