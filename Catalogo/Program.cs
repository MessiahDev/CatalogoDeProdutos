using Catalogo.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseMySql(mySqlConnection,
            ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("Versão 1.0", new OpenApiInfo
    {
        Title = "Api Catálogo de Produtos",
        Version = "Versão 1.0",
        Description = "CRUD de Produtos e Categorias.",
        Contact = new OpenApiContact
        {
            Name = "Alex Alle",
            Email = "alexmessias_18@yahoo.com.br",
        },
    });
});



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
