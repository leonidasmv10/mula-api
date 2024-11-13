using System.ComponentModel.DataAnnotations;

namespace MulaApi.Models
{
    public class User
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        // [MaxLength(20)]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public ICollection<GameResult>? GameResults { get; set; }
    }
}
