using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebScraperTesteAPI.Data;
using WebScraperTesteAPI.Services;

namespace WebScraperTesteAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlimentosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ScraperService _scraperService;

        public AlimentosController(AppDbContext context, ScraperService scraperService)
        {
            _context = context;
            _scraperService = scraperService;
        }

        /// <summary>
        /// Extraçaõ de alimentos da web.
        /// </summary>
        /// <param name="pagina">Número da página a ser extraido (default é 1).</param>
        /// <returns>Lista de alimentos extraídos.</returns>
        [HttpGet("scrape")]
        public async Task<IActionResult> ScrapeAlimentos(int pagina = 1)
        {
            if (pagina < 1)
            {
                return BadRequest("A página deve ser maior ou igual a 1.");
            }

            try
            {
                var alimentos = await _scraperService.ExtrairAlimentosAsync(pagina);
                if (alimentos == null || !alimentos.Any())
                {
                    return NotFound("Nenhum alimento encontrado.");
                }

                _context.Alimentos.AddRange(alimentos);
                await _context.SaveChangesAsync();

                return Ok(alimentos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao buscar alimentos: {ex.Message}");
            }
        }

    }
}
