using Microsoft.EntityFrameworkCore;
using BlazorApp1.Data;
using BlazorApp1.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Habilitar CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permite qualquer origem
              .AllowAnyMethod()  // Permite qualquer método HTTP (GET, POST, etc.)
              .AllowAnyHeader(); // Permite qualquer cabeçalho
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseInMemoryDatabase("ProductDb")
);

// Adicionar serviços para controllers (API)
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Habilitar o CORS
app.UseCors("AllowAllOrigins");

app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// Habilitar o mapeamento de controladores para aceitar requisições REST
app.MapControllers();  // Isso mapeia as rotas da API

app.Run();
