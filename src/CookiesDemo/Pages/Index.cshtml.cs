using CookiesDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CookiesDemo.Pages;

public class IndexModel(ILogger<IndexModel> logger) : PageModel
{
    private readonly ILogger<IndexModel> _logger = logger;
    public List<Todo> Todos { get; set; } = [];

    [BindProperty]
    public required Todo NewTodo { get; set; }

    private List<Todo> GetTodosFromCookie()
    {
        return JsonSerializer.Deserialize<List<Todo>>(Request.Cookies[nameof(Todo)] ?? "[]") ?? [];
    }

    private void PersistTodosToCookie(List<Todo> todos)
    {
        Response.Cookies.Append(nameof(Todo), JsonSerializer.Serialize(todos));
        _logger.LogInformation("Added new TODO: {Todo}", NewTodo.Name);
    }

    public IActionResult OnGet()
    {
        Todos = GetTodosFromCookie();
        return Page();
    }

    public IActionResult OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Todos = GetTodosFromCookie();
        Todos.Add(NewTodo);
        
        PersistTodosToCookie(Todos);
        return RedirectToPage();
    }
}
