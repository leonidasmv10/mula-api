namespace MulaApi.Models
{
    public class UserStatsDto
    {
        public int UserId { get; set; }
        public int TotalGamesPlayed { get; set; }
        public double AverageCorrectAnswers { get; set; }
        public double SuccessRate { get; set; }

    }

    public class UserCategoryStatsDto
    {
        public int CategoryId { get; set; }
        public double AverageCorrectAnswers { get; set; }
    }

    public class GlobalRankingDto
    {
        public int UserId { get; set; }
        public double AverageCorrectAnswers { get; set; }
    }

    public class CategoryRankingDto
    {
        public int UserId { get; set; }
        public double AverageCorrectAnswers { get; set; }
    }


}
