using System.ComponentModel.DataAnnotations;

namespace ITAssetTracker.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required, StringLength(150)]
        public string Title { get; set; } = string.Empty;

        [StringLength(1000)]
        public string? Description { get; set; }

        [Required]
        public string Priority { get; set; } = "Medium"; // Low, Medium, High

        [Required]
        public string Status { get; set; } = "Open"; // Open, In Progress, Resolved

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public int? AssetId { get; set; }
        public Asset? Asset { get; set; }
    }
}