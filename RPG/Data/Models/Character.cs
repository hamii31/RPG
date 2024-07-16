using System.ComponentModel.DataAnnotations;

namespace RPG.Data.Models
{
    public class Character
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public int Health { get; set; }
        [Required]
        public int Mana { get; set; }
        [Required]
        public int Damage { get; set; }
        [Required]
        public DateTime CreatedOn { get; set; }
    }
}
