using HubMeteorologico.Api.Controllers;
using HubMeteorologico.Api.Models;

namespace HubMeteorologico.Api.Clients;

public interface IEstacaoClient
{
    Task<List<RegistroMeteorologicoResponse>?> GetRegistrosMeteorologicosAsync(
        string codigoEquipamento,
        DateTime dataHora
    );
}

public class EstacaoClient : IEstacaoClient
{
    private readonly HttpClient _httpClient;

    public async Task<List<RegistroMeteorologicoResponse>?> GetRegistrosMeteorologicosAsync(
        string codigoEquipamento,
        DateTime dataHora
    )
    {
        var response = await _httpClient.GetAsync(
            $"api/v1/estacoes/{codigoEquipamento}/medicoes?dataHora={dataHora:O}"
        );

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            return null;

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<List<RegistroMeteorologicoResponse>>();
    }
}
