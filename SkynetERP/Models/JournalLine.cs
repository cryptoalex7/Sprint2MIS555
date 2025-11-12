using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class JournalLine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int JournalEntryId { get; set; }

    [Required]
    [StringLength(100)]
    public string AccountCode { get; set; } = string.Empty;

    [StringLength(200)]
    public string AccountName { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal DebitAmount { get; set; } = 0;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal CreditAmount { get; set; } = 0;

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    [ForeignKey("JournalEntryId")]
    public JournalEntry? JournalEntry { get; set; }
}

