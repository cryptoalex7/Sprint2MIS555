using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Delivery
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int VendorId { get; set; }

    [ForeignKey("VendorId")]
    public Vendor Vendor { get; set; } = null!;

    [Required]
    [StringLength(200)]
    public string DeliveryNumber { get; set; } = string.Empty;

    [Required]
    public DateTime DeliveryDate { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [StringLength(100)]
    public string? Status { get; set; } = "Pending"; // Pending, Confirmed, Completed

    [StringLength(500)]
    public string? PhotoPath { get; set; } // Path to uploaded delivery photo

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [StringLength(100)]
    public string? CreatedBy { get; set; }
}

