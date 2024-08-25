// Använder EF Core och Books Data Model
using Microsoft.EntityFrameworkCore;
using Books.Data;

// Förbereder applikationen och inkluderar SQLite som vald databas
var builder = WebApplication.CreateBuilder(args);

// Lägg till MVC-controllers med vyer som en tjänst
builder.Services.AddControllersWithViews();

// Konfigurera databaskontexten för att använda SQLite, med anslutningssträngen från konfigurationen
builder.Services.AddDbContext<BooksDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultDbConnection")) // Databastjänst med standardanslutning
);

var app = builder.Build();

// Kontrollera om miljön är utveckling, annars använd felhanteringssida och HTTP Strict Transport Security (HSTS)
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Tvingar användning av HTTPS och skyddar mot Man-in-the-Middle-attacker
}

// Middleware för att hantera HTTPS-omdirigering, statiska filer, routing och auktorisering
app.UseHttpsRedirection(); // Omdirigerar HTTP-förfrågningar till HTTPS
app.UseStaticFiles(); // Möjliggör åtkomst till statiska filer (CSS, JavaScript, bilder, etc.)
app.UseRouting(); // Aktiverar routing av inkommande förfrågningar till rätt controller och action
app.UseAuthorization(); // Hanterar användarens behörigheter

// Konfigurerar standardrutt för controllers i applikationen
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"); // Standardrutt: Home controller med Index action, id är valfritt

// Kör applikationen
app.Run();
