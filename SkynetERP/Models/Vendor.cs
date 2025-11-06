using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Vendor
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string CompanyName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string Category { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string ContactPerson { get; set; } = string.Empty;

    [Required]
    [StringLength(200)]
    public string Address { get; set; } = string.Empty;

    [Required]
    [StringLength(20)]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0, 100000000)]
    public decimal AnnualSpend { get; set; }
}

