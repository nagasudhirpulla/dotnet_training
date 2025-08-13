namespace CookiesDemo.Entities;

public class Todo
{
    public required string Name { get; set; }
    public bool IsDone { get; set; } = false;
}
