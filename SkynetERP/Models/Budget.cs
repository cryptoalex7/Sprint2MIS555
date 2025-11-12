using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Budget
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    public int CategoryId { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    [Range(0.01, 100000000)]
    public decimal BudgetedAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal SpentAmount { get; set; } = 0;

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    [StringLength(50)]
    public string Period { get; set; } = "Monthly"; // Monthly, Quarterly, Yearly

    [StringLength(500)]
    public string Notes { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // Navigation property
    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }

    [NotMapped]
    public decimal RemainingAmount => BudgetedAmount - SpentAmount;
}

