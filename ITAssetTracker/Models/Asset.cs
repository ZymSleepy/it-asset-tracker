using System.ComponentModel.DataAnnotations;
using System.Net.Sockets;

namespace ITAssetTracker.Models
{
    public class Asset
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required, StringLength(50)]
        public string Type { get; set; } = string.Empty; // e.g. Laptop, Monitor, Printer

        [Required]
        public string Status { get; set; } = "Available"; // Available, Assigned, Retired

        [StringLength(100)]
        public string? AssignedTo { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}