using Aplicacion.Interfaces;
using Aplicacion.Servicios;
using Dominio.Interfaces;
using Infraestructura.Persistencia;
using Infraestructura.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add CORS configuration
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policyBuilder =>
    {
        policyBuilder.AllowAnyOrigin()
                     .AllowAnyMethod()
                     .AllowAnyHeader();
    });
});




// Obtener la cadena de conexión del archivo appsettings.json
var connectionString = builder.Configuration.GetConnectionString("mi_computador");
// Configurar Entity Framework Core con SQL Server
builder.Services.AddDbContext<ContextoDB>(options => options.UseSqlServer(connectionString));


// Add services to the container.
builder.Services.AddScoped<IPersonaRepositorio, PersonaRepositorio>();
builder.Services.AddScoped<IPersonaServicio, PersonaServicio>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

//configuracion de cores
app.UseCors("AllowOrigin");
app.UseCors(options =>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
