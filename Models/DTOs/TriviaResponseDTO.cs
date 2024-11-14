using Newtonsoft.Json;
namespace MulaApi.Models
{
    // Modelo para mapear la respuesta de la API de Open Trivia
    public class TriviaResponseDTO
    {
        [JsonProperty("results")]
        public List<TriviaQuestion> Results { get; set; }
    }
}
