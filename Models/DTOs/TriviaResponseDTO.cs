using Newtonsoft.Json;
namespace MulaApi.Models
{
    public class TriviaResponseDTO
    {
        [JsonProperty("results")]
        public List<TriviaQuestion> Results { get; set; }
    }
}
