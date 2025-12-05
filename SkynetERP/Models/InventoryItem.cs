using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class InventoryItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string ItemName { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Category { get; set; } = string.Empty;

    [Required]
    public int Quantity { get; set; }

    [Required]
    public int ReorderLevel { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [StringLength(200)]
    public string? Supplier { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(50)]
    public string? Location { get; set; }

    [StringLength(50)]
    public string? SKU { get; set; }

    public DateTime LastUpdated { get; set; } = DateTime.Now;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [StringLength(100)]
    public string? CreatedBy { get; set; }

    // Computed property for low stock status
    [NotMapped]
    public bool IsLowStock => Quantity <= ReorderLevel;

    // Computed property for stock status
    [NotMapped]
    public string StockStatus
    {
        get
        {
            if (Quantity <= 0) return "Out of Stock";
            if (Quantity <= ReorderLevel) return "Low Stock";
            return "In Stock";
        }
    }
}

