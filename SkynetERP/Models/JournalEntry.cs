using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class JournalEntry
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string EntryNumber { get; set; } = string.Empty;

    [Required]
    public DateTime EntryDate { get; set; } = DateTime.Now;

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [StringLength(50)]
    public string Reference { get; set; } = string.Empty;

    [StringLength(50)]
    public string Status { get; set; } = "Draft"; // Draft, Posted, Reversed

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    [StringLength(100)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public DateTime? PostedAt { get; set; }

    // Navigation property
    public List<JournalLine> JournalLines { get; set; } = new();
}

