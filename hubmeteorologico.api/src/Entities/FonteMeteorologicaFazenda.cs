using System;

namespace HubMeteorologico.Api.Entities;

public class FonteMeteorologicaFazenda
{
    public int Id { get; set; }
    public int FazendaId { get; set; }
    public int FonteMeteorologicaId { get; set; }
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }

    public Fazenda? Fazenda { get; set; }
    public FonteMeteorologica? FonteMeteorologica { get; set; }
}
