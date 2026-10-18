using IdentityDemo.Entities;
using IdentityDemo.Infra;
using IdentityDemo.Infra.Data;
using IdentityDemo.Infra.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

// setup the application and identity framework database context
var connectionString = builder.Configuration.GetConnectionString("Sqlite");
builder.Services.AddDbContext<AppDbContext>((sp, options) =>
{
    options.UseSqlite(connectionString);
});


// implement IApplicationDbContext with the AppDbContext. This can be useful for clean architecture
// builder.Services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
// builder.Services.AddScoped<ApplicationDbContextInitialiser>();

// setup identity framework
builder.Services
    // services for user validation & db persistence, claims, validation, password validation & hashing, and more
    .AddIdentityCore<AppUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.Stores.SchemaVersion = IdentitySchemaVersions.Version3;
    })
    // services for role validation & db persistence
    .AddRoles<IdentityRole>()
    // services to add Entity Framework implementation of identity stores like user store, role store
    .AddEntityFrameworkStores<AppDbContext>()
    // services to abstract complex security procedures for sign-in, sign-out, cookies and sessions into simple methods
    .AddSignInManager()
    // creates custom claims for a logged user based on the logged-in User db entity
    .AddClaimsPrincipalFactory<ApplicationUserClaimsPrincipalFactory>()
    // required services to generate tokens for password resets, email confirmation, and 2FA
    .AddDefaultTokenProviders();

// adds authentication services and mentions sets the default authentication scheme to use the identity framework's application cookie scheme
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
})
// adds cookies authentication handler for the identity framework's application cookie scheme
.AddIdentityCookies();

// configures the application cookie options for the identity framework's application cookie scheme
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
    options.LogoutPath = "/Account/Logout";
});

// add a scoped service for the AppDbContextInitialiser to handle database initialization and seeding
builder.Services.AddScoped<AppDbContextInitialiser>();

// add email sender service for sending emails for email confirmation, password reset, and other identity-related notifications
builder.Services.AddTransient<IEmailSender, DummyEmailSender>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    // In development, we want to ensure the database is created and seeded with initial data
    await app.InitialiseDatabaseAsync();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
