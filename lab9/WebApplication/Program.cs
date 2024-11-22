using WebApplication.Services;
using DBWebApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebApplication.Services;

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;  // Додаємо можливість показувати версії API в відповіді
    options.AssumeDefaultVersionWhenUnspecified = true; // Використовуємо версію за замовчуванням
    options.DefaultApiVersion = new ApiVersion(2, 0);  // Встановлюємо версію за замовчуванням
});

var dbProvider = "Postgres";
var dbHost = "localhost";
var dbName = "kpp";

string connectionString = null;

switch (dbProvider)
{
    case "SqlServer":
        connectionString = $"Server={dbHost};Database={dbName};Trusted_Connection=True;TrustServerCertificate=True;";
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        break;

    case "Postgres":
        var dbUser = "postgres";
        var dbPassword = "postgres";
        connectionString = $"Host={dbHost};Database={dbName};Username={dbUser};Password={dbPassword};";
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        break;

    case "Sqlite":
        connectionString = $"Data Source={dbHost};";
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));
        break;

    case "InMemory":
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseInMemoryDatabase("HealthcareDb"));
        break;

    default:
        throw new Exception("Unsupported database type.");
}

// HTTP Client configuration
builder.Services.AddHttpClient("ApiClient", client =>
{
    var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];
    client.BaseAddress = new Uri(apiBaseUrl ?? "http://localhost:5243/dbapi/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

// Authentication & Authorization
builder.Services.AddAuthentication("AuthScheme")
    .AddCookie("AuthScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());
});

// Services
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<Auth0UserService>();

// CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("ApiCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:5243")
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Error handling
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Middleware pipeline
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// CORS
app.UseCors("ApiCorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run application
app.Run("http://0.0.0.0:5230");
