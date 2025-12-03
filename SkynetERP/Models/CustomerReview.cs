using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkynetERP.Models;

public class CustomerReview
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int CustomerId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string ReviewText { get; set; } = string.Empty;

    [Range(1, 5)]
    public int Rating { get; set; } = 5; // 1-5 stars

    [StringLength(100)]
    public string? ReviewerName { get; set; }

    public DateTime ReviewDate { get; set; } = DateTime.Now;

    public bool IsPublished { get; set; } = true;

    // Navigation property
    [ForeignKey("CustomerId")]
    public Customer? Customer { get; set; }
}

