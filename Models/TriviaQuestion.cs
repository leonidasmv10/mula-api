using System.ComponentModel.DataAnnotations;

namespace MulaApi.Models
{
    public class TriviaQuestion
    {
        public int QuestionId { get; set; }
        public int CategoryId { get; set; }
        public string QuestionText { get; set; }
        public string CorrectAnswer { get; set; }
        public List<string> IncorrectAnswers { get; set; }
        public string Type { get; set; }
    }
}