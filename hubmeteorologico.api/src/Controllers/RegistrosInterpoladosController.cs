using HubMeteorologico.Api.Context;
using HubMeteorologico.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HubMeteorologico.Api.Controllers;

[ApiController]
[Route("api/v1/registros-interpolados")]
[Authorize("administrador")]
public class RegistrosInterpoladosController : ControllerBase
{
    private readonly AppDbContext _context;

    public RegistrosInterpoladosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Este recurso tem o objetivo retornar a lista de registros interpolados.
    /// </summary>
    /// <param name="fazenda">ID da Fazenda</param>
    /// <param name="lavoura">ID da Lavoura</param>
    /// <param name="dataHora">Data e hora do registro</param>
    [HttpGet("{fazenda:int}/{lavoura:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<RegistroInterpoladoResponse>>> GetRegistrosInterpolados(
        [FromRoute] int fazenda,
        [FromRoute] int lavoura,
        [FromQuery] DateTime dataHora
    )
    {
        var data = new DateTime(
            dataHora.Year,
            dataHora.Month,
            dataHora.Day,
            dataHora.Hour,
            0,
            0,
            dataHora.Kind
        );
        var dados = await _context
            .RegistrosInterpolados.AsNoTracking()
            .Where(r =>
                r.FazendaId == fazenda && r.MapaFazendaLavouraId == lavoura && r.DataHora == data
            )
            .Select(r => new
            {
                r.DataHora,
                r.FazendaId,
                r.MapaFazendaLavouraId,
                r.Coordenada,
                r.VolumeChuva,
                r.Temperatura,
                r.UmidadeRelativaAr,
                r.PressaoAtmosferica,
                r.DirecaoVento,
                r.VelocidadeVento,
                r.RadiacaoSolar,
                r.Evapotranspiracao,
            })
            .ToListAsync();

        if (!dados.Any())
            return NotFound(new { mensagem = "Nenhum registro encontrado." });

        var response = dados
            .Select(r => new RegistroInterpoladoResponse
            {
                DataHora = r.DataHora,
                FazendaId = r.FazendaId,
                MapaFazendaLavouraId = r.MapaFazendaLavouraId,
                Latitude = r.Coordenada?.Y ?? 0,
                Longitude = r.Coordenada?.X ?? 0,
                VolumeChuva = r.VolumeChuva,
                Temperatura = r.Temperatura,
                UmidadeRelativaAr = r.UmidadeRelativaAr,
                PressaoAtmosferica = r.PressaoAtmosferica,
                DirecaoVento = r.DirecaoVento,
                VelocidadeVento = r.VelocidadeVento,
                RadiacaoSolar = r.RadiacaoSolar,
                Evapotranspiracao = r.Evapotranspiracao,
            })
            .ToList();

        return Ok(response);
    }
}
