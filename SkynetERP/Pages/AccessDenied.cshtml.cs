using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkynetERP.Pages;

public class AccessDeniedModel : PageModel
{
    private readonly ILogger<AccessDeniedModel> _logger;

    public AccessDeniedModel(ILogger<AccessDeniedModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        _logger.LogWarning("Access denied for user {User} to {Path}", 
            User.Identity?.Name ?? "Anonymous", 
            Request.Path);
    }
}
