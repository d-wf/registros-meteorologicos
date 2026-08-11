using System;
using NetTopologySuite.Geometries;

namespace HubMeteorologico.Api.Entities;

public class MapaFazendaLavoura
{
    public int Id { get; set; }
    public int MapaFazendaId { get; set; }
    public string CodigoLavoura { get; set; } = string.Empty;
    public float Area { get; set; }
    public Point Poligono { get; set; } = default!;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }

    public MapaFazenda? MapaFazenda { get; set; }
}
