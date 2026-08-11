namespace HubMeteorologico.Api.Models;

public class EquipamentoEstacaoUpdateRequest : EquipamentoBaseUpdateRequest
{
    public string Nome { get; set; } = string.Empty;
}
