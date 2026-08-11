using HubMeteorologico.Api.Context;
using HubMeteorologico.Api.Enums;
using HubMeteorologico.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HubMeteorologico.Api.Controllers;

[ApiController]
[Route("api/v1/equipamentos")]
[Authorize("equipamentos")]
public class EquipamentosController : ControllerBase
{
    private readonly AppDbContext _context;

    public EquipamentosController(AppDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Lista equipamentos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<EquipamentoResponse>>> Get(int quantidadeRegistros = 100)
    {
        var equipamentos = await _context
            .Equipamentos.AsNoTracking()
            .Take(quantidadeRegistros)
            .ToListAsync();

        var response = EquipamentoResponse.Parse(equipamentos);
        return Ok(response);
    }

    /// <summary>
    /// Lista Estações Meteorológicas
    /// </summary>
    [HttpGet("estacoes")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<EquipamentoResponse>>> GetEstacoes(
        int quantidadeRegistros = 100
    )
    {
        var equipamentos = await _context
            .Equipamentos.AsNoTracking()
            .Where(i => i.TipoEquipamento == TipoEquipamento.EstacaoMeteorologica)
            .Take(quantidadeRegistros)
            .ToListAsync();

        var response = EquipamentoResponse.Parse(equipamentos);
        return Ok(response);
    }

    /// <summary>
    /// Lista Pluviômetros
    /// </summary>
    [HttpGet("pluviometros")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<List<EquipamentoResponse>>> GetPluviometros(
        int quantidadeRegistros = 100
    )
    {
        var equipamentos = await _context
            .Equipamentos.AsNoTracking()
            .Where(i => i.TipoEquipamento == TipoEquipamento.Pluviometro)
            .Take(quantidadeRegistros)
            .ToListAsync();

        var response = EquipamentoResponse.Parse(equipamentos);
        return Ok(response);
    }

    /// <summary>
    /// Obtém os dados de uma Estação Meteorológica por ID.
    /// </summary>
    [HttpGet("estacoes/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquipamentoResponse>> GetEstacao(int id)
    {
        var equipamento = await _context
            .Equipamentos.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (equipamento is null)
            return NotFound(new { mensagem = "Equipamento não encontrado." });

        if (equipamento.TipoEquipamento != TipoEquipamento.EstacaoMeteorologica)
            return BadRequest(
                new { mensagem = "O equipamento informado não é uma Estação Meteorológica." }
            );

        var response = EquipamentoResponse.Parse(equipamento);

        return Ok(response);
    }

    /// <summary>
    /// Obtém os dados de um Pluviômetro por ID.
    /// </summary>
    [HttpGet("pluviometros/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<EquipamentoResponse>> GetPluviometro(int id)
    {
        var equipamento = await _context
            .Equipamentos.AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);

        if (equipamento is null)
            return NotFound(new { mensagem = "Equipamento não encontrado." });

        if (equipamento.TipoEquipamento != TipoEquipamento.Pluviometro)
            return BadRequest(new { mensagem = "O equipamento informado não é um Pluviômetro." });

        var response = EquipamentoResponse.Parse(equipamento);

        return Ok(response);
    }

    /// <summary>
    /// Atualiza dados de um equipamento Estação Meteorológica
    /// </summary>
    [HttpPut("estacoes/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarEstacao(
        int id,
        [FromBody] EquipamentoEstacaoUpdateRequest request
    )
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);

        if (equipamento is null)
            return NotFound(new { mensagem = "Equipamento não encontrado." });

        if (equipamento.TipoEquipamento != TipoEquipamento.EstacaoMeteorologica)
            return BadRequest(
                new { mensagem = "O equipamento informado não é uma Estação Meteorológica." }
            );

        try
        {
            if (!string.IsNullOrWhiteSpace(request.Codigo))
                equipamento.Codigo = request.Codigo;

            if (!string.IsNullOrWhiteSpace(request.Nome))
                equipamento.Nome = request.Nome;

            if (!string.IsNullOrWhiteSpace(request.Modelo))
                equipamento.Modelo = request.Modelo;

            if (request.SedeFazendaId != 0)
                equipamento.SedeFazendaId = request.SedeFazendaId;

            if (request.FazendaId != 0)
                equipamento.FazendaId = request.FazendaId;

            if (request.FonteMeteorologica != 0)
                equipamento.FonteMeteorologica = request.FonteMeteorologica;
            equipamento.Ativo = request.Ativo;
            equipamento.ModifierUsername = User.Identity?.Name ?? "HubMeteorologico.Api";
            equipamento.ModificationTime = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    /// <summary>
    /// Atualiza dados de um equipamento Pluviômetro.
    /// </summary>
    [HttpPut("pluviometros/{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AtualizarPluviometro(
        int id,
        [FromBody] EquipamentoPluviometroUpdateRequest request
    )
    {
        var equipamento = await _context.Equipamentos.FindAsync(id);

        if (equipamento is null)
            return NotFound(new { mensagem = "Equipamento não encontrado." });

        if (equipamento.TipoEquipamento != TipoEquipamento.Pluviometro)
            return BadRequest(new { mensagem = "O equipamento informado não é um Pluviômetro." });

        try
        {
            if (!string.IsNullOrWhiteSpace(request.Codigo))
                equipamento.Codigo = request.Codigo;

            if (!string.IsNullOrWhiteSpace(request.Modelo))
                equipamento.Modelo = request.Modelo;

            if (request.SedeFazendaId != 0)
                equipamento.SedeFazendaId = request.SedeFazendaId;

            if (request.FazendaId != 0)
                equipamento.FazendaId = request.FazendaId;

            if (request.FonteMeteorologica != 0)
                equipamento.FonteMeteorologica = request.FonteMeteorologica;
            equipamento.Ativo = request.Ativo;
            equipamento.ModifierUsername = User.Identity?.Name ?? "HubMeteorologico.Api";
            equipamento.ModificationTime = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }
}
