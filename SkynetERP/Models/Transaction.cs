using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    [StringLength(20)]
    public string Type { get; set; } = string.Empty; // Revenue or Expense

    [Required]
    public DateTime TransactionDate { get; set; } = DateTime.Now;

    [Required]
    public int AccountId { get; set; }

    public int? CategoryId { get; set; }

    [StringLength(100)]
    public string ReferenceNumber { get; set; } = string.Empty;

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    [StringLength(50)]
    public string Status { get; set; } = "Pending"; // Pending, Completed, Cancelled

    [StringLength(100)]
    public string CreatedBy { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation properties
    [ForeignKey("AccountId")]
    public Account? Account { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}

