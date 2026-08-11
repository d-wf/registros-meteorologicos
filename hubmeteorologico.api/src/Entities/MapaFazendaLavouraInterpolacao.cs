using System;
using NetTopologySuite.Geometries;

namespace HubMeteorologico.Api.Entities;

public class MapaFazendaLavouraInterpolacao
{
    public int Id { get; set; }
    public int MapaFazendaLavouraId { get; set; }
    public Point Poligono { get; set; } = default!;
    public Point Centroide { get; set; } = default!;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }

    public MapaFazendaLavoura? MapaFazendaLavoura { get; set; }
}
