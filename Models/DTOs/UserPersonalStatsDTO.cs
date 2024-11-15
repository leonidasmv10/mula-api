namespace MulaApi.Models
{
    public class UserStatsDto
    {
        public int UserId { get; set; }
        public int AverageCorrectAnswers { get; set; }
    }

    public class UserCategoryStatsDto
    {
        public int CategoryId { get; set; }
        public int AverageCorrectAnswers { get; set; }
    }

    public class GlobalRankingDto
    {
        public int UserId { get; set; }
        public int AverageCorrectAnswers { get; set; }
    }

    public class CategoryRankingDto
    {
        public int UserId { get; set; }
        public int AverageCorrectAnswers { get; set; }
    }


}
