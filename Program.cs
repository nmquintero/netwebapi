using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projectef;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDbContext<TareasContext>(p => p.UseInMemoryDatabase("TareasDB"));
//conexion para mysql 
// Configure services
    builder.Services.AddDbContext<TareasContext>(options =>
    {
        options.UseMySQL("Server=127.0.0.1; Port=8889; Database=tareas; Uid=root; Pwd=root");
    });

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet("/dbconexion", async ([FromServices] TareasContext dbContext) => 
{
    dbContext.Database.EnsureCreated();
    return Results.Ok("Base de datos en memoria: " + dbContext.Database.IsInMemory());
});

app.Run();