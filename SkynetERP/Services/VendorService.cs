using Microsoft.EntityFrameworkCore;
using SkynetERP.Data;
using SkynetERP.Models;
using SkynetERP.Pages;

namespace SkynetERP.Services;

public class VendorService
{
    private readonly ApplicationDbContext _context;

    public VendorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public List<VendorModel> GetAllVendors()
    {
        return _context.Vendors
            .Select(v => new VendorModel
            {
                Id = v.Id,
                CompanyName = v.CompanyName,
                Category = v.Category,
                ContactPerson = v.ContactPerson,
                Address = v.Address,
                Phone = v.Phone,
                AnnualSpend = v.AnnualSpend
            })
            .ToList();
    }

    public VendorModel? GetVendorById(int id)
    {
        var vendor = _context.Vendors.Find(id);
        if (vendor == null) return null;

        return new VendorModel
        {
            Id = vendor.Id,
            CompanyName = vendor.CompanyName,
            Category = vendor.Category,
            ContactPerson = vendor.ContactPerson,
            Address = vendor.Address,
            Phone = vendor.Phone,
            AnnualSpend = vendor.AnnualSpend
        };
    }

    public void AddVendor(VendorModel vendorModel)
    {
        var vendor = new Vendor
        {
            CompanyName = vendorModel.CompanyName,
            Category = vendorModel.Category,
            ContactPerson = vendorModel.ContactPerson,
            Address = vendorModel.Address,
            Phone = vendorModel.Phone,
            AnnualSpend = vendorModel.AnnualSpend
        };

        _context.Vendors.Add(vendor);
        _context.SaveChanges();
    }

    public bool DeleteVendor(int id)
    {
        var vendor = _context.Vendors.Find(id);
        if (vendor != null)
        {
            _context.Vendors.Remove(vendor);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool UpdateVendor(VendorModel vendorModel)
    {
        var vendor = _context.Vendors.Find(vendorModel.Id);
        if (vendor != null)
        {
            vendor.CompanyName = vendorModel.CompanyName;
            vendor.Category = vendorModel.Category;
            vendor.ContactPerson = vendorModel.ContactPerson;
            vendor.Address = vendorModel.Address;
            vendor.Phone = vendorModel.Phone;
            vendor.AnnualSpend = vendorModel.AnnualSpend;

            _context.SaveChanges();
            return true;
        }
        return false;
    }
}

