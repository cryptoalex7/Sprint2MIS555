using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class InvoiceLine
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int InvoiceId { get; set; }

    [Required]
    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 100000000)]
    public decimal Quantity { get; set; } = 1;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 100000000)]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal TaxRate { get; set; } = 0;

    [Column(TypeName = "decimal(18,2)")]
    public decimal LineTotal { get; set; }

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    [ForeignKey("InvoiceId")]
    public Invoice? Invoice { get; set; }

    [NotMapped]
    public decimal Subtotal => Quantity * UnitPrice;

    [NotMapped]
    public decimal TaxAmount => Subtotal * (TaxRate / 100);
}

