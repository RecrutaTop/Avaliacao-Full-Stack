using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Banco de Dados em Memória
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("FornecedoresDb"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowCors", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/v1.json");
    });
}

app.UseCors("AllowCors");
app.UseHttpsRedirection();

// --- ENDPOINTS

app.MapGet("/api/fornecedores", async (AppDbContext db) =>
{
    var fornecedores = await db.Fornecedores.ToListAsync();
    return Results.Ok(fornecedores);
})
.WithName("GetFornecedores");

app.MapGet("/api/fornecedores/{id}", async (int id, AppDbContext db) =>
{
    var fornecedor = await db.Fornecedores.FindAsync(id);
    if (fornecedor == null) return Results.NotFound();

    return Results.Ok(fornecedor);
})
.WithName("GetFornecedorById");

app.MapPost("/api/fornecedores", async (Fornecedor fornecedor, AppDbContext db) =>
{
    fornecedor.DataInclusao = DateTime.Now;

    // Validação básica
    if (string.IsNullOrWhiteSpace(fornecedor.NomeFornecedor) || fornecedor.NomeFornecedor.Length > 200)
        return Results.BadRequest("Nome do fornecedor é obrigatório e deve ter no máximo 200 caracteres.");

    db.Fornecedores.Add(fornecedor);
    await db.SaveChangesAsync();

    return Results.Created($"/api/fornecedores/{fornecedor.Id}", fornecedor);
})
.WithName("CreateFornecedor");

app.MapPut("/api/fornecedores/{id}", async (int id, Fornecedor fornecedorAtualizado, AppDbContext db) =>
{
    var fornecedor = await db.Fornecedores.FindAsync(id);
    if (fornecedor == null) return Results.NotFound();

    // Validação básica
    if (string.IsNullOrWhiteSpace(fornecedorAtualizado.NomeFornecedor) || fornecedorAtualizado.NomeFornecedor.Length > 200)
        return Results.BadRequest("Nome do fornecedor é obrigatório e deve ter no máximo 200 caracteres.");

    fornecedor.NomeFornecedor = fornecedorAtualizado.NomeFornecedor;
    fornecedor.Cnpj = fornecedorAtualizado.Cnpj;
    fornecedor.Endereco = fornecedorAtualizado.Endereco;
    fornecedor.Cep = fornecedorAtualizado.Cep;
    fornecedor.DataAberturaEmpresa = fornecedorAtualizado.DataAberturaEmpresa;

    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("UpdateFornecedor");

app.MapDelete("/api/fornecedores/{id}", async (int id, AppDbContext db) =>
{
    var fornecedor = await db.Fornecedores.FindAsync(id);
    if (fornecedor == null) return Results.NotFound();

    db.Fornecedores.Remove(fornecedor);
    await db.SaveChangesAsync();

    return Results.NoContent();
})
.WithName("DeleteFornecedor");

// Cria um escopo para inicializar o banco de dados com alguns dados de teste
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.EnsureCreated();

    if (!db.Fornecedores.Any())
    {
        db.Fornecedores.Add(new Fornecedor
        {
            NomeFornecedor = "Empresa Teste LTDA",
            Cnpj = "12.345.678/0001-99",
            Endereco = "Rua Teste, 123",
            Cep = "12345-678",
            DataAberturaEmpresa = new DateTime(2020, 1, 1),
            DataInclusao = DateTime.Now
        });
        db.SaveChanges();
    }
}

app.Run();

// --- CLASSES E CONTEXTO ---

public class Fornecedor
{
    public int Id { get; set; }
    public string NomeFornecedor { get; set; } = string.Empty;
    public string Cnpj { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string Cep { get; set; } = string.Empty;
    public DateTime DataAberturaEmpresa { get; set; }
    public DateTime DataInclusao { get; set; }
}

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Fornecedor> Fornecedores { get; set; }
}