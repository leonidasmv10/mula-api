using System.ComponentModel.DataAnnotations;

namespace MulaApi.Models
{
    public class UserRanking
    {
        public string Username { get; set; }
        public int GamesPlayed { get; set; }
        public double AverageScore { get; set; }
    }
}
