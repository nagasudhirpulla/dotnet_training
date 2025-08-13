using CookiesDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Xml.Linq;

namespace CookiesDemo.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;

    public void OnGet()
    {
        // get TODOs string from cookies
        List<Todo> todoList = JsonSerializer.Deserialize<List<Todo>>(Request.Cookies[nameof(Todo)] ?? "[]") ?? [];
    }
}
