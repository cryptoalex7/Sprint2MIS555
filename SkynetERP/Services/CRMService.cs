using Microsoft.EntityFrameworkCore;
using SkynetERP.Data;
using SkynetERP.Models;

namespace SkynetERP.Services;

public class CRMService
{
    private readonly ApplicationDbContext _context;

    public CRMService(ApplicationDbContext context)
    {
        _context = context;
    }

    // Customer methods
    public List<Customer> GetAllCustomers()
    {
        return _context.Customers
            .Include(c => c.Reviews)
            .OrderByDescending(c => c.LastContact ?? c.CreatedAt)
            .ToList();
    }

    public Customer? GetCustomerById(int id)
    {
        return _context.Customers
            .Include(c => c.Reviews)
            .FirstOrDefault(c => c.Id == id);
    }

    public void AddCustomer(Customer customer)
    {
        customer.CreatedAt = DateTime.Now;
        if (customer.LastContact == null)
        {
            customer.LastContact = DateTime.Now;
        }
        _context.Customers.Add(customer);
        _context.SaveChanges();
    }

    public bool UpdateCustomer(Customer customer)
    {
        var existingCustomer = _context.Customers.Find(customer.Id);
        if (existingCustomer != null)
        {
            existingCustomer.Name = customer.Name;
            existingCustomer.Company = customer.Company;
            existingCustomer.Email = customer.Email;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Status = customer.Status;
            existingCustomer.LastContact = customer.LastContact;
            existingCustomer.Notes = customer.Notes;
            
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeleteCustomer(int id)
    {
        var customer = _context.Customers
            .Include(c => c.Reviews)
            .FirstOrDefault(c => c.Id == id);
        
        if (customer != null)
        {
            // Delete associated reviews
            _context.CustomerReviews.RemoveRange(customer.Reviews);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public void UpdateLastContact(int customerId)
    {
        var customer = _context.Customers.Find(customerId);
        if (customer != null)
        {
            customer.LastContact = DateTime.Now;
            _context.SaveChanges();
        }
    }

    // Customer Review methods
    public List<CustomerReview> GetAllReviews()
    {
        return _context.CustomerReviews
            .Include(r => r.Customer)
            .Where(r => r.IsPublished)
            .OrderByDescending(r => r.ReviewDate)
            .ToList();
    }

    public List<CustomerReview> GetReviewsByCustomerId(int customerId)
    {
        return _context.CustomerReviews
            .Where(r => r.CustomerId == customerId && r.IsPublished)
            .OrderByDescending(r => r.ReviewDate)
            .ToList();
    }

    public CustomerReview? GetReviewById(int id)
    {
        return _context.CustomerReviews
            .Include(r => r.Customer)
            .FirstOrDefault(r => r.Id == id);
    }

    public void AddReview(CustomerReview review)
    {
        review.ReviewDate = DateTime.Now;
        _context.CustomerReviews.Add(review);
        
        // Update customer's last contact date
        var customer = _context.Customers.Find(review.CustomerId);
        if (customer != null)
        {
            customer.LastContact = DateTime.Now;
        }
        
        _context.SaveChanges();
    }

    public bool UpdateReview(CustomerReview review)
    {
        var existingReview = _context.CustomerReviews.Find(review.Id);
        if (existingReview != null)
        {
            existingReview.Title = review.Title;
            existingReview.ReviewText = review.ReviewText;
            existingReview.Rating = review.Rating;
            existingReview.ReviewerName = review.ReviewerName;
            existingReview.IsPublished = review.IsPublished;
            
            _context.SaveChanges();
            return true;
        }
        return false;
    }

    public bool DeleteReview(int id)
    {
        var review = _context.CustomerReviews.Find(id);
        if (review != null)
        {
            _context.CustomerReviews.Remove(review);
            _context.SaveChanges();
            return true;
        }
        return false;
    }
}

