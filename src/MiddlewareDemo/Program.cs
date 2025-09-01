var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// add custom middleware to the container
builder.Services.AddTransient<CustomMiddleware>();
//builder.Services.AddCustomMiddleware();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

// Use Middleware: Append middleware to pipeline
app.Use(async (context, next) =>
{
    Console.WriteLine("Executing middleware before next()");
    await next.Invoke();  // Calling the next middleware in the pipeline
    Console.WriteLine("Executing middleware after next()");
});

// use custom middleware in pipeline
app.UseMiddleware<CustomMiddleware>();
//app.UseCustomMiddleware();

// Map Middleware: split middleware pipeline for a specific route
app.Map("/admin", adminApp =>
{
    adminApp.Use(async (context, next) =>
    {
        Console.WriteLine("Admin Area Middleware: Before next()");
        await next();
        Console.WriteLine("Admin Area Middleware: After next()");
    });

    adminApp.Run(async context =>
    {
        await context.Response.WriteAsync("Welcome to the Admin Dashboard!\n");
    });
});

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

// Run Middleware: terminates the middleware pipeline
app.Run();

// Custom Middleware class
public class CustomMiddleware(ILogger<CustomMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        logger.LogInformation("Before request from custom middleware");
        await next(context);
        logger.LogInformation("After request from custom middleware");
    }
}

// extension methods for readable custom middleware methods
public static class CustomMiddleExtensions
{
    public static IApplicationBuilder UseCustomMiddleware(this IApplicationBuilder app)
        => app.UseMiddleware<CustomMiddleware>();
    public static IServiceCollection AddCustomMiddleware(this IServiceCollection services)
    {
        services.AddTransient<CustomMiddleware>();
        return services;
    }
}
