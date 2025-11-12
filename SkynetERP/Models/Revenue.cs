using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Revenue
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 100000000)]
    public decimal Amount { get; set; }

    [Required]
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    [Required]
    public int AccountId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [StringLength(100)]
    public string ReferenceNumber { get; set; } = string.Empty;

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    [StringLength(100)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("AccountId")]
    public Account? Account { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}

