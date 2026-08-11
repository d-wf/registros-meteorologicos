using HubMeteorologico.Api.Clients;
using HubMeteorologico.Api.Context;
using HubMeteorologico.Api.Entities;
using HubMeteorologico.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HubMeteorologico.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/registros-meteorologicos")]
public class RegistrosMeteorologicosController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IEstacaoClient _estacaoClient;

    public RegistrosMeteorologicosController(AppDbContext context, IEstacaoClient estacaoClient)
    {
        _context = context;
        _estacaoClient = estacaoClient;
    }

    /// <summary>
    /// Consulta e persiste registros meteorológico da API externa e persiste o resultado.
    /// </summary>
    /// <param name="equipamento">ID do Equipamento</param>
    /// <param name="dataHora">Data e hora da medição</param>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status502BadGateway)]
    public async Task<IActionResult> AtualizarRegistros(
        [FromQuery(Name = "Equipamento")] int equipamento,
        [FromQuery(Name = "DataHora")] DateTime dataHora
    )
    {
        List<RegistroMeteorologicoResponse> registros = null;

        if (equipamento <= 0 || dataHora == default)
            return BadRequest(
                new { mensagem = "Os parâmetros 'Equipamento' e 'DataHora' são obrigatórios." }
            );

        var equip = await _context
            .Equipamentos.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == equipamento);

        if (equip == null)
            return NotFound(
                new
                {
                    mensagem = $"Equipamento com ID {equipamento} não foi encontrado no cadastro.",
                }
            );

        try
        {
            var response = await _estacaoClient.GetRegistrosMeteorologicosAsync(
                equip.Codigo,
                dataHora
            );

            if (response != null)
                registros = response;
        }
        catch (HttpRequestException)
        {
            return StatusCode(
                StatusCodes.Status502BadGateway,
                new { mensagem = "Falha ao se comunicar com a API externa." }
            );
        }

        if (registros == null)
            return NotFound(new { mensagem = "Nenhum registro encontrado." });

        _context.RegistrosMeteorologicos.AddRange(
            registros.Select(r => new RegistroMeteorologico
            {
                DataHora = dataHora,
                EquipamentoId = equip.Id,
                FazendaId = equip.FazendaId,
                Consolidada = r.Consolidada,
                PressaoAtmosferica = r.PressaoAtmosferica,
                UmidadeRelativaAr = r.UmidadeRelativaAr,
                VolumeChuva = r.VolumeChuva.GetValueOrDefault(),
                Temperatura = r.Temperatura,
                DirecaoVento = r.DirecaoVento,
                VelocidadeVento = r.VelocidadeVento,
                PontoOrvalho = r.PontoOrvalho,
                Bateria = r.Bateria,
                FolhaMolhada = r.FolhaMolhada,
                Versao = r.Versao.ToString() ?? "1",
                RadiacaoSolar = r.RadiacaoSolar,
                AnoAgricolaId =
                    _context.AnosAgricolas.FirstOrDefault(a => a.Codigo == r.AnoAgricolaCodigo)?.Id
                    ?? 0,
                TemperaturaMaxima = r.TemperaturaMaxima,
                TemperaturaMinima = r.TemperaturaMinima,
                VelocidadeVentoPico = r.VelocidadeVentoPico,
                Evapotranspiracao = r.Evapotranspiracao,
                CreatorUsername = User.Identity?.Name ?? "HubMeteorologico.Api",
                CreationTime = DateTime.Now,
            })
        );
        await _context.SaveChangesAsync();

        return Ok();
    }
}
