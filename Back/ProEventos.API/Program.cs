using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Implementacao.Eventos;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using ProEventos.Persistence.Interfaces.Implementacao.Eventos;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add context of project
builder.Services.AddDbContext<ProEventosContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("Default"))
    );

// Add controllers
builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Services and pesistence
builder.Services.AddScoped<IEventosServices, EventosServices>();

builder.Services.AddScoped<IGeralPersistence, GeralPersistence>();
builder.Services.AddScoped<IEventosPersistence, EventosPersistence>();

// Add cors polyce
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("http://localhost:4200") // Adicione a URL do seu frontend Angular
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

// Add Cors Polyces
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
