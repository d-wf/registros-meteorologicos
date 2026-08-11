namespace HubMeteorologico.Api.Models;

public class RegistroInterpoladoResponse
{
    public DateTime DataHora { get; set; }
    public int FazendaId { get; set; }
    public int MapaFazendaLavouraId { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public double VolumeChuva { get; set; }
    public double Temperatura { get; set; }
    public double UmidadeRelativaAr { get; set; }
    public double PressaoAtmosferica { get; set; }
    public double DirecaoVento { get; set; }
    public double VelocidadeVento { get; set; }
    public double RadiacaoSolar { get; set; }
    public double Evapotranspiracao { get; set; }
}
