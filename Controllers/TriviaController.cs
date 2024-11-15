using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("userStats/{userId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserPersonalStats(int userId)
        {
            var userStats = new List<UserStatsDto>();

            var connection = _context.Database.GetDbConnection();

            try
            {
                await connection.OpenAsync();
                var command = connection.CreateCommand();
                command.CommandText = "sp_GetUserPersonalStats";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                var parameter = command.CreateParameter();
                parameter.ParameterName = "@UserId";
                parameter.Value = userId;
                command.Parameters.Add(parameter);

                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        userStats.Add(new UserStatsDto
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            AverageCorrectAnswers = reader.GetInt32(reader.GetOrdinal("AverageCorrectAnswers"))
                        });
                    }
                }
            }
            finally
            {
                await connection.CloseAsync();
            }

            if (userStats.Count == 0)
            {
                return NotFound();
            }

            return Ok(userStats);
        }

        [HttpGet("user/{userId}/category-stats")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserCategoryStats(int userId)
        {
            var userCategoryStats = new List<UserCategoryStatsDto>();

            // Obtener la conexión de la base de datos
            var connection = _context.Database.GetDbConnection();

            try
            {
                // Abrir la conexión
                await connection.OpenAsync();

                // Crear el comando para ejecutar el procedimiento almacenado
                var command = connection.CreateCommand();
                command.CommandText = "sp_GetUserCategoryStats";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Agregar el parámetro del procedimiento almacenado
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@UserId";
                parameter.Value = userId;
                command.Parameters.Add(parameter);

                // Ejecutar el procedimiento y leer los resultados
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Crear un DTO para cada fila de los resultados
                        userCategoryStats.Add(new UserCategoryStatsDto
                        {
                            CategoryId = reader.GetInt32(reader.GetOrdinal("CategoryId")),
                            AverageCorrectAnswers = reader.GetInt32(reader.GetOrdinal("AverageCorrectAnswers")) // Obtener el promedio
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, si algo falla durante la ejecución
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al ejecutar el procedimiento: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                await connection.CloseAsync();
            }

            // Verificar si no se encontraron resultados
            if (userCategoryStats.Count == 0)
            {
                return NotFound();
            }

            // Devolver los resultados en formato JSON
            return Ok(userCategoryStats);
        }

        [HttpGet("global-ranking")]
        [AllowAnonymous]
        public async Task<IActionResult> GetGlobalRanking()
        {
            var globalRanking = new List<GlobalRankingDto>();

            // Obtener la conexión de la base de datos
            var connection = _context.Database.GetDbConnection();

            try
            {
                // Abrir la conexión
                await connection.OpenAsync();

                // Crear el comando para ejecutar el procedimiento almacenado
                var command = connection.CreateCommand();
                command.CommandText = "sp_GetGlobalRanking";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Ejecutar el procedimiento y leer los resultados
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Crear un DTO para cada fila de los resultados
                        globalRanking.Add(new GlobalRankingDto
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            AverageCorrectAnswers = reader.GetInt32(reader.GetOrdinal("AverageCorrectAnswers"))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, si algo falla durante la ejecución
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al ejecutar el procedimiento: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                await connection.CloseAsync();
            }

            // Verificar si no se encontraron resultados
            if (globalRanking.Count == 0)
            {
                return NotFound();
            }

            // Devolver los resultados en formato JSON
            return Ok(globalRanking);
        }

        [HttpGet("category/{categoryId}/ranking")]
        [AllowAnonymous]
        public async Task<IActionResult> GetCategoryRanking(int categoryId)
        {
            var categoryRanking = new List<CategoryRankingDto>();

            // Obtener la conexión de la base de datos
            var connection = _context.Database.GetDbConnection();

            try
            {
                // Abrir la conexión
                await connection.OpenAsync();

                // Crear el comando para ejecutar el procedimiento almacenado
                var command = connection.CreateCommand();
                command.CommandText = "sp_GetCategoryRanking";
                command.CommandType = System.Data.CommandType.StoredProcedure;

                // Agregar el parámetro del procedimiento almacenado
                var parameter = command.CreateParameter();
                parameter.ParameterName = "@CategoryId";
                parameter.Value = categoryId;
                command.Parameters.Add(parameter);

                // Ejecutar el procedimiento y leer los resultados
                using (var reader = await command.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        // Crear un DTO para cada fila de los resultados
                        categoryRanking.Add(new CategoryRankingDto
                        {
                            UserId = reader.GetInt32(reader.GetOrdinal("UserId")),
                            AverageCorrectAnswers = reader.GetInt32(reader.GetOrdinal("AverageCorrectAnswers"))
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores, si algo falla durante la ejecución
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error al ejecutar el procedimiento: {ex.Message}");
            }
            finally
            {
                // Cerrar la conexión
                await connection.CloseAsync();
            }

            // Verificar si no se encontraron resultados
            if (categoryRanking.Count == 0)
            {
                return NotFound();
            }

            // Devolver los resultados en formato JSON
            return Ok(categoryRanking);
        }


    }
}
