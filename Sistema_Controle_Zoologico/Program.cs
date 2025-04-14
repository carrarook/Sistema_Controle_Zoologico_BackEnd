using Microsoft.EntityFrameworkCore;
using Sistema_Controle_Zoologico.Data;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Builder;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Configuração de Autenticação com Cookies
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None; // Permitir cookies cross-origin
        options.ExpireTimeSpan = TimeSpan.FromDays(7);
    });

// Configuração de Sessão
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Tempo de sessão
});

// Adicionar serviços ao container
builder.Services.AddControllersWithViews();

// Configuração do banco de dados SQLite
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configuração de JSON para preservar referências
builder.Services.AddControllers().AddJsonOptions(x =>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Configuração de CORS para permitir a comunicação com o frontend
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy
            .WithOrigins("http://localhost:3000", "https://sistemazoologicofront.vercel.app") // Permitir CORS para esses domínios
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); // Permitir envio de cookies
});

// Configuração do Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sistema de Controle Zoológico API",
        Version = "v1",
        Description = "API para gerenciamento de um zoológico",
        Contact = new OpenApiContact
        {
            Name = "Brunin",
            Email = "bãodemais@dominio.com"
        }
    });
});

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Habilita a política de CORS
app.UseCors("AllowReactApp");

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

// Habilita a sessão
app.UseSession();

// Habilitar o Swagger e Swagger UI em ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Habilita o Swagger
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sistema de Controle Zoológico API v1");
        c.RoutePrefix = string.Empty; // O Swagger UI estará disponível em / (raiz)
    });
}

// Mapeamento das rotas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Criar e popular o banco de dados
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.EnsureCreated();
        DbSeeder.Initialize(services);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao criar/popular o banco de dados.");
    }
}

app.Run();
