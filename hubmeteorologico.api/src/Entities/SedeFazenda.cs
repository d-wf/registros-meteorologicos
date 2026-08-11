using System;

namespace HubMeteorologico.Api.Entities;

public class SedeFazenda
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Codigo { get; set; } = string.Empty;
    public int FazendaId { get; set; }
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }

    public Fazenda? Fazenda { get; set; }
}
