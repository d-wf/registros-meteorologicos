using System;

namespace HubMeteorologico.Api.Entities;

public class AnoAgricola
{
    public int Id { get; set; }
    public int Codigo { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
    public DateTime DataFinal { get; set; }
    public DateTime DataInicial { get; set; }
}
