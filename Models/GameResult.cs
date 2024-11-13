using System.ComponentModel.DataAnnotations;

namespace MulaApi.Models
{
    public class GameResult
    {
        public int GameResultId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CorrectAnswers { get; set; }
        public int CategoryId { get; set; }
        public DateTime Date { get; set; }
    }
}
