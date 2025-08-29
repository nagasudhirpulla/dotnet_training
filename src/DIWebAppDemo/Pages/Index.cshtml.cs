using DIWebAppDemo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DIWebAppDemo.Pages;

public class IndexModel(IMessageWriter writer) : PageModel
{
    public IActionResult OnGet()
    {
        writer.Write("Index page reached");
        return Page();
    }
}
