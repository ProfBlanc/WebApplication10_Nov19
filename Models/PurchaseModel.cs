using System.ComponentModel.DataAnnotations;

namespace WebApplication10_Nov19.Models
{
    public class PurchaseModel
    {
        [Key]
        public int PurchaseId { get; set; }

        [Required]
        [Length(5, 50)]
        public string Name { get; set; } = string.Empty;

        [Range(0.5, 1000)]
        public double Price { get; set; }
    }
}
