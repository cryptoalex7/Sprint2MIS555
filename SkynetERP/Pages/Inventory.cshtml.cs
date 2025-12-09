using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using SkynetERP.Services;
using SkynetERP.Models;
using System.Security.Claims;

namespace SkynetERP.Pages;

[Authorize(Roles = "Admin,InventoryManager")]
public class InventoryModel : PageModel
{
    private readonly ILogger<InventoryModel> _logger;
    private readonly InventoryService _inventoryService;

    public InventoryModel(ILogger<InventoryModel> logger, InventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }

    public List<InventoryItem> InventoryItems { get; set; } = new();
    public List<string> Categories { get; set; } = new();

    // Metric properties
    public int TotalStockLevel { get; set; }
    public decimal InventoryValue { get; set; }
    public decimal InventoryTurnover { get; set; }
    public int AgingInventoryCount { get; set; }
    public int SlowMovingItemsCount { get; set; }
    public int LowStockCount { get; set; }
    public int OutOfStockCount { get; set; }

    // Filter property
    [BindProperty(SupportsGet = true)]
    public string? CategoryFilter { get; set; }

    public void OnGet()
    {
        try
        {
            LoadData();
            CalculateMetrics();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading inventory data");
            TempData["Error"] = "Error loading inventory data";
            InventoryItems = new List<InventoryItem>();
            Categories = new List<string>();
        }
    }

    public IActionResult OnPostCreate(
        [FromForm] string ItemName,
        [FromForm] string Category,
        [FromForm] int Quantity,
        [FromForm] int ReorderLevel,
        [FromForm] decimal UnitPrice,
        [FromForm] string? Supplier,
        [FromForm] string? Description,
        [FromForm] string? Location,
        [FromForm] string? SKU)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(ItemName))
            {
                TempData["Error"] = "Item name is required";
                LoadData();
                CalculateMetrics();
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Category))
            {
                TempData["Error"] = "Category is required";
                LoadData();
                CalculateMetrics();
                return Page();
            }
            if (Quantity < 0)
            {
                TempData["Error"] = "Quantity cannot be negative";
                LoadData();
                CalculateMetrics();
                return Page();
            }
            if (UnitPrice < 0)
            {
                TempData["Error"] = "Unit price cannot be negative";
                LoadData();
                CalculateMetrics();
                return Page();
            }

            var item = new InventoryItem
            {
                ItemName = ItemName.Trim(),
                Category = Category.Trim(),
                Quantity = Quantity,
                ReorderLevel = ReorderLevel,
                UnitPrice = UnitPrice,
                Supplier = Supplier?.Trim(),
                Description = Description?.Trim(),
                Location = Location?.Trim(),
                SKU = SKU?.Trim(),
                CreatedBy = User.FindFirst(ClaimTypes.Name)?.Value ?? "System"
            };

            _inventoryService.AddItem(item);
            _logger.LogInformation("Inventory item added: {ItemName}", item.ItemName);
            TempData["Success"] = $"Inventory item '{item.ItemName}' added successfully";
            
            // Preserve filter on redirect
            return RedirectToPage(new { CategoryFilter });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding inventory item: {Error}", ex.Message);
            TempData["Error"] = $"Error adding inventory item: {ex.Message}";
            LoadData();
            CalculateMetrics();
            return Page();
        }
    }

    public IActionResult OnPostEdit(
        [FromForm] int Id,
        [FromForm] string ItemName,
        [FromForm] string Category,
        [FromForm] int Quantity,
        [FromForm] int ReorderLevel,
        [FromForm] decimal UnitPrice,
        [FromForm] string? Supplier,
        [FromForm] string? Description,
        [FromForm] string? Location,
        [FromForm] string? SKU)
    {
        try
        {
            // Validate required fields
            if (string.IsNullOrWhiteSpace(ItemName))
            {
                TempData["Error"] = "Item name is required";
                LoadData();
                CalculateMetrics();
                return Page();
            }
            if (string.IsNullOrWhiteSpace(Category))
            {
                TempData["Error"] = "Category is required";
                LoadData();
                CalculateMetrics();
                return Page();
            }
            if (Quantity < 0)
            {
                TempData["Error"] = "Quantity cannot be negative";
                LoadData();
                CalculateMetrics();
                return Page();
            }
            if (ReorderLevel < 0)
            {
                TempData["Error"] = "Reorder level cannot be negative";
                LoadData();
                CalculateMetrics();
                return Page();
            }
            if (UnitPrice < 0)
            {
                TempData["Error"] = "Unit price cannot be negative";
                LoadData();
                CalculateMetrics();
                return Page();
            }

            var item = new InventoryItem
            {
                Id = Id,
                ItemName = ItemName.Trim(),
                Category = Category.Trim(),
                Quantity = Quantity,
                ReorderLevel = ReorderLevel,
                UnitPrice = UnitPrice,
                Supplier = Supplier?.Trim(),
                Description = Description?.Trim(),
                Location = Location?.Trim(),
                SKU = SKU?.Trim()
            };

            if (_inventoryService.UpdateItem(item))
            {
                _logger.LogInformation("Inventory item updated: {ItemName}", item.ItemName);
                TempData["Success"] = $"Inventory item '{item.ItemName}' updated successfully";
            }
            else
            {
                TempData["Error"] = "Inventory item not found";
            }
            
            // Preserve filter on redirect
            return RedirectToPage(new { CategoryFilter });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating inventory item: {Error}", ex.Message);
            TempData["Error"] = $"Error updating inventory item: {ex.Message}";
            LoadData();
            CalculateMetrics();
            return Page();
        }
    }

    public IActionResult OnPostDelete([FromForm] int id)
    {
        try
        {
            var item = _inventoryService.GetItemById(id);
            if (item != null && _inventoryService.DeleteItem(id))
            {
                _logger.LogInformation("Inventory item deleted: {ItemName}", item.ItemName);
                TempData["Success"] = $"Inventory item '{item.ItemName}' deleted successfully";
            }
            else
            {
                TempData["Error"] = "Inventory item not found";
            }
            
            // Preserve filter on redirect
            return RedirectToPage(new { CategoryFilter });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting inventory item: {Error}", ex.Message);
            TempData["Error"] = $"Error deleting inventory item: {ex.Message}";
            return RedirectToPage(new { CategoryFilter });
        }
    }

    private void LoadData()
    {
        InventoryItems = _inventoryService.GetItemsByCategory(CategoryFilter);
        Categories = _inventoryService.GetAllCategories();
    }

    private void CalculateMetrics()
    {
        TotalStockLevel = _inventoryService.GetTotalStockLevel();
        InventoryValue = _inventoryService.GetInventoryValue();
        InventoryTurnover = _inventoryService.GetInventoryTurnover();
        AgingInventoryCount = _inventoryService.GetAgingInventoryCount();
        SlowMovingItemsCount = _inventoryService.GetSlowMovingItemsCount();
        LowStockCount = _inventoryService.GetLowStockCount();
        OutOfStockCount = _inventoryService.GetOutOfStockCount();
    }
}

