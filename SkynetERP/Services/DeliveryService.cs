using Microsoft.EntityFrameworkCore;
using SkynetERP.Data;
using SkynetERP.Models;
using SkynetERP.Pages;

namespace SkynetERP.Services;

public class DeliveryService
{
    private readonly ApplicationDbContext _context;

    public DeliveryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<DeliveryModel> GetAllDeliveries()
    {
        return _context.Deliveries
            .Include(d => d.Vendor)
            .OrderByDescending(d => d.DeliveryDate)
            .Select(d => new DeliveryModel
            {
                Id = d.Id,
                VendorId = d.VendorId,
                VendorName = d.Vendor.CompanyName,
                DeliveryNumber = d.DeliveryNumber,
                DeliveryDate = d.DeliveryDate,
                Description = d.Description ?? string.Empty,
                Status = d.Status ?? "Pending",
                PhotoPath = d.PhotoPath ?? string.Empty,
                CreatedAt = d.CreatedAt,
                CreatedBy = d.CreatedBy ?? string.Empty
            })
            .ToList();
    }

    public List<DeliveryModel> GetDeliveriesByVendor(int vendorId)
    {
        return _context.Deliveries
            .Include(d => d.Vendor)
            .Where(d => d.VendorId == vendorId)
            .OrderByDescending(d => d.DeliveryDate)
            .Select(d => new DeliveryModel
            {
                Id = d.Id,
                VendorId = d.VendorId,
                VendorName = d.Vendor.CompanyName,
                DeliveryNumber = d.DeliveryNumber,
                DeliveryDate = d.DeliveryDate,
                Description = d.Description ?? string.Empty,
                Status = d.Status ?? "Pending",
                PhotoPath = d.PhotoPath ?? string.Empty,
                CreatedAt = d.CreatedAt,
                CreatedBy = d.CreatedBy ?? string.Empty
            })
            .ToList();
    }

    public DeliveryModel? GetDeliveryById(int id)
    {
        var delivery = _context.Deliveries
            .Include(d => d.Vendor)
            .FirstOrDefault(d => d.Id == id);
        
        if (delivery == null) return null;

        return new DeliveryModel
        {
            Id = delivery.Id,
            VendorId = delivery.VendorId,
            VendorName = delivery.Vendor.CompanyName,
            DeliveryNumber = delivery.DeliveryNumber,
            DeliveryDate = delivery.DeliveryDate,
            Description = delivery.Description ?? string.Empty,
            Status = delivery.Status ?? "Pending",
            PhotoPath = delivery.PhotoPath ?? string.Empty,
            CreatedAt = delivery.CreatedAt,
            CreatedBy = delivery.CreatedBy ?? string.Empty
        };
    }

    public void AddDelivery(DeliveryModel deliveryModel, string? photoPath = null)
    {
        var delivery = new Delivery
        {
            VendorId = deliveryModel.VendorId,
            DeliveryNumber = deliveryModel.DeliveryNumber,
            DeliveryDate = deliveryModel.DeliveryDate,
            Description = deliveryModel.Description,
            Status = deliveryModel.Status,
            PhotoPath = photoPath,
            CreatedAt = DateTime.Now,
            CreatedBy = deliveryModel.CreatedBy
        };

        _context.Deliveries.Add(delivery);
        _context.SaveChanges();
    }

    public bool UpdateDelivery(DeliveryModel deliveryModel, string? photoPath = null)
    {
        var delivery = _context.Deliveries.Find(deliveryModel.Id);
        if (delivery != null)
        {
            delivery.DeliveryNumber = deliveryModel.DeliveryNumber;
            delivery.DeliveryDate = deliveryModel.DeliveryDate;
            delivery.Description = deliveryModel.Description;
            delivery.Status = deliveryModel.Status;
            if (!string.IsNullOrEmpty(photoPath))
            {
                delivery.PhotoPath = photoPath;
            }
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeleteDelivery(int id)
    {
        var delivery = _context.Deliveries.Find(id);
        if (delivery != null)
        {
            // Delete photo file if exists
            if (!string.IsNullOrEmpty(delivery.PhotoPath))
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", delivery.PhotoPath.TrimStart('/'));
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
            }
            
            _context.Deliveries.Remove(delivery);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}

