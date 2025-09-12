using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// Add Cookie Authentication service
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Specify the path to the login page
        options.AccessDeniedPath = "/Account/AccessDenied"; // Specify the path for access denied
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60); // Set the cookie expiration time
        options.SlidingExpiration = true; // Enable sliding expiration
    });
// AddAuthentication adds required services for authentication. It also specifies the default authentication scheme to be used for authentication.
// AddCookie provides an authentication handler (that uses cookies) for the 'CookieAuthenticationDefaults.AuthenticationScheme' scheme.

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

app.UseAuthentication();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
