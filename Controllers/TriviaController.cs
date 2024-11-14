using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using MulaApi.Models;

namespace MulaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [Authorize(Policy = "AdminOnly")]
    public class TriviaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly HttpClient _httpClient;

        public TriviaController(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // GET: api/Trivia/questions/{categoryId}
        [HttpGet("questions/{categoryId}")]
        public async Task<IActionResult> GetTrivia(int categoryId)
        {
            try
            {
                var url = $"https://opentdb.com/api.php?amount=10&category={categoryId}&type=multiple";
                var response = await _httpClient.GetAsync(url);
                if (!response.IsSuccessStatusCode)
                {
                    return BadRequest("No se pudo obtener la trivia.");
                }

                var content = await response.Content.ReadAsStringAsync();
                var triviaData = JsonConvert.DeserializeObject<TriviaResponseDTO>(content);
                return Ok(triviaData.Results);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }


        [HttpPost("games")]
        public async Task<IActionResult> Register(GameResult gameResult)
        {
            _context.GameResults.Add(gameResult);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Register), new { id = gameResult.Id }, gameResult);
        }
    }
}
