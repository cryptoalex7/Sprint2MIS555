using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class TaxRate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 100)]
    public decimal Rate { get; set; } // Percentage (e.g., 8.5 for 8.5%)

    [StringLength(50)]
    public string TaxType { get; set; } = string.Empty; // Sales, Income, VAT, etc.

    [StringLength(100)]
    public string Jurisdiction { get; set; } = string.Empty; // State, Country, etc.

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime EffectiveDate { get; set; } = DateTime.Now;

    public DateTime? ExpirationDate { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

