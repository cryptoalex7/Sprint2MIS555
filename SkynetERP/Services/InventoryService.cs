using Microsoft.EntityFrameworkCore;
using SkynetERP.Data;
using SkynetERP.Models;

namespace SkynetERP.Services;

public class InventoryService
{
    private readonly ApplicationDbContext _context;

    public InventoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Get all inventory items
    public List<InventoryItem> GetAllItems()
    {
        return _context.InventoryItems
            .OrderBy(i => i.ItemName)
            .ToList();
    }

    // Get items by category
    public List<InventoryItem> GetItemsByCategory(string? category)
    {
        var query = _context.InventoryItems.AsQueryable();
        
        if (!string.IsNullOrWhiteSpace(category))
        {
            query = query.Where(i => i.Category == category);
        }
        
        return query.OrderBy(i => i.ItemName).ToList();
    }

    // Get item by ID
    public InventoryItem? GetItemById(int id)
    {
        return _context.InventoryItems.Find(id);
    }

    // Add new item
    public void AddItem(InventoryItem item)
    {
        item.CreatedAt = DateTime.Now;
        item.LastUpdated = DateTime.Now;
        _context.InventoryItems.Add(item);
        _context.SaveChanges();
    }

    // Update item
    public bool UpdateItem(InventoryItem item)
    {
        var existingItem = _context.InventoryItems.Find(item.Id);
        if (existingItem != null)
        {
            existingItem.ItemName = item.ItemName;
            existingItem.Category = item.Category;
            existingItem.Quantity = item.Quantity;
            existingItem.ReorderLevel = item.ReorderLevel;
            existingItem.UnitPrice = item.UnitPrice;
            existingItem.Supplier = item.Supplier;
            existingItem.Description = item.Description;
            existingItem.Location = item.Location;
            existingItem.SKU = item.SKU;
            existingItem.LastUpdated = DateTime.Now;
            
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    // Delete item
    public bool DeleteItem(int id)
    {
        var item = _context.InventoryItems.Find(id);
        if (item != null)
        {
            _context.InventoryItems.Remove(item);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    // Get all unique categories
    public List<string> GetAllCategories()
    {
        return _context.InventoryItems
            .Select(i => i.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToList();
    }

    // Calculate metrics
    public int GetTotalStockLevel()
    {
        return _context.InventoryItems.Sum(i => (int?)i.Quantity) ?? 0;
    }

    public decimal GetInventoryValue()
    {
        return _context.InventoryItems.Sum(i => (decimal?)(i.Quantity * i.UnitPrice)) ?? 0;
    }

    public int GetLowStockCount()
    {
        return _context.InventoryItems.Count(i => i.Quantity <= i.ReorderLevel);
    }

    public int GetOutOfStockCount()
    {
        return _context.InventoryItems.Count(i => i.Quantity <= 0);
    }

    // Get aging inventory (items not updated in last 90 days)
    public int GetAgingInventoryCount()
    {
        var cutoffDate = DateTime.Now.AddDays(-90);
        return _context.InventoryItems.Count(i => i.LastUpdated < cutoffDate);
    }

    // Get slow-moving items (items with quantity > 0 but last updated > 60 days ago)
    public int GetSlowMovingItemsCount()
    {
        var cutoffDate = DateTime.Now.AddDays(-60);
        return _context.InventoryItems.Count(i => i.Quantity > 0 && i.LastUpdated < cutoffDate);
    }

    // Calculate inventory turnover (simplified: total value / average value)
    public decimal GetInventoryTurnover()
    {
        var totalValue = GetInventoryValue();
        var itemCount = _context.InventoryItems.Count();
        
        if (itemCount == 0) return 0;
        
        var averageValue = totalValue / itemCount;
        if (averageValue == 0) return 0;
        
        // Simplified turnover calculation
        return totalValue / averageValue;
    }
}

