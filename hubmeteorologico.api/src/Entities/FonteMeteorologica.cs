using System;

namespace HubMeteorologico.Api.Entities;

public class FonteMeteorologica
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int TipoFonteMeteorologica { get; set; }
    public string Usuario { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
}
