using System;
using NetTopologySuite.Geometries;

namespace HubMeteorologico.Api.Entities;

public class MapaFazenda
{
    public int Id { get; set; }
    public int FazendaId { get; set; }
    public int AnoAgricolaId { get; set; }
    public Point Centroide { get; set; } = default!;
    public string? CreatorUsername { get; set; }
    public DateTime CreationTime { get; set; }
    public DateTime? ModificationTime { get; set; }
    public string? ModifierUsername { get; set; }
    public Point Envelope { get; set; } = default!;
    public Point EnvelopeInterpolacao { get; set; } = default!;
    public int SedeFazendaId { get; set; } = 0;

    public Fazenda? Fazenda { get; set; }
    public AnoAgricola? AnoAgricola { get; set; }
}
