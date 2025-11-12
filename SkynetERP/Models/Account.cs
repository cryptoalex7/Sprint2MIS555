using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string AccountName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string AccountType { get; set; } = string.Empty; // Checking, Savings, Credit, Cash, etc.

    [StringLength(100)]
    public string BankName { get; set; } = string.Empty;

    [StringLength(50)]
    public string AccountNumber { get; set; } = string.Empty;

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Balance { get; set; }

    [StringLength(200)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

