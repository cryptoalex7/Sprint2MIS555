using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Payment
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string PaymentNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string PayeeName { get; set; } = string.Empty;

    public int? PartnerId { get; set; }

    [Required]
    [StringLength(20)]
    public string PaymentType { get; set; } = "Inflow"; // Inflow or Outflow

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 100000000)]
    public decimal Amount { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; } = DateTime.Now;

    [Required]
    public int AccountId { get; set; }

    public int? InvoiceId { get; set; }

    [Required]
    [StringLength(50)]
    public string PaymentMethod { get; set; } = string.Empty; // Check, Wire Transfer, Credit Card, Cash, etc.

    [StringLength(50)]
    public string Status { get; set; } = "Pending"; // Pending, Completed, Failed, Cancelled

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

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

    [ForeignKey("InvoiceId")]
    public Invoice? Invoice { get; set; }

    [ForeignKey("PartnerId")]
    public Partner? Partner { get; set; }
}

