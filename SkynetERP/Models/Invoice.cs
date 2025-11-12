using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Invoice
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string InvoiceNumber { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string CustomerName { get; set; } = string.Empty;

    public int? PartnerId { get; set; }

    [Required]
    [StringLength(20)]
    public string InvoiceType { get; set; } = "AR"; // AR (Accounts Receivable) or AP (Accounts Payable)

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 100000000)]
    public decimal Amount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PaidAmount { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; } // Amount - PaidAmount

    [Required]
    public DateTime InvoiceDate { get; set; } = DateTime.Now;

    [Required]
    public DateTime DueDate { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = "Pending"; // Pending, Paid, Overdue, Cancelled

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    [StringLength(200)]
    public string CustomerEmail { get; set; } = string.Empty;

    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    [ForeignKey("PartnerId")]
    public Partner? Partner { get; set; }

    // Navigation property for lines
    public List<InvoiceLine> InvoiceLines { get; set; } = new();
}

