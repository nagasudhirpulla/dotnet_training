using CookiesDemo.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace CookiesDemo.Pages;

public class DeleteModel : PageModel
{
    [BindProperty]
    public required string Todo { get; set; }
    [BindProperty]
    public required int TodoInd { get; set; }

    public IActionResult OnGet(int id)
    {
        var todo = (JsonSerializer.Deserialize<List<Todo>>(Request.Cookies[nameof(Todo)] ?? "[]") ?? []).ElementAt(id);
        if (todo == null)
        {
            return new NotFoundResult();
        }
        Todo = todo.Name;
        TodoInd = id;
        return Page();
    }

    public IActionResult OnPost()
    {
        var newTodos = JsonSerializer.Deserialize<List<Todo>>(Request.Cookies[nameof(Todo)] ?? "[]") ?? [];
        newTodos.RemoveAt(TodoInd);
        Response.Cookies.Append(nameof(Todo), JsonSerializer.Serialize(newTodos));
        return RedirectToPage("/Index");
    }
}
