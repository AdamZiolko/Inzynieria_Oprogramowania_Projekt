using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectBookworm.Data;
using ProjectBookworm.Areas.Identity.Data;

var builder = WebApplication.CreateBuilder(args);

// Pobranie po��czenia z baz� danych
var connectionString = builder.Configuration.GetConnectionString("AuthDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AuthDbContextConnection' not found.");

// Rejestracja kontekstu bazy danych
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Rejestracja to�samo�ci z rolami
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders(); // U�yj tej linii, aby doda� domy�lne dostawc�w token�w

// Dodanie us�ug do kontenera
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

// Konfiguracja opcji to�samo�ci
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false;
});

var app = builder.Build();

// Tworzenie r�l po uruchomieniu aplikacji
await using (var scope = app.Services.CreateAsyncScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Sprawdzenie, czy role istniej�, je�li nie, utworzenie ich
    if (!await roleManager.RoleExistsAsync("Administrator"))
    {
        await roleManager.CreateAsync(new IdentityRole("Administrator"));
    }

    if (!await roleManager.RoleExistsAsync("U�ytkownik"))
    {
        await roleManager.CreateAsync(new IdentityRole("U�ytkownik"));
    }
}

// Mapowanie tras
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "bookDetails",
    pattern: "book/{title}",
    defaults: new { controller = "Book", action = "Details" });

// Konfiguracja potoku ��da� HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
