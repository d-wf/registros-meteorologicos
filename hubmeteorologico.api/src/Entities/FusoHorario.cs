using System;

namespace HubMeteorologico.Api.Entities;

public class FusoHorario
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public TimeSpan Offset { get; set; } // Interval no PostgreSQL → TimeSpan em C#
    public string Localidade { get; set; } = string.Empty;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
}
