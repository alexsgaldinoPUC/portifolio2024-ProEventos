using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using ProEventos.API.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add context of project
builder.Services.AddDbContext<DataContext>(
    options => options.UseSqlite(builder.Configuration.GetConnectionString("Default"))
    );

// Add controllers
builder.Services.AddControllers();

// Add cors polyce
builder.Services.AddCors();

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

app.UseAuthorization();

// Add Cors Polyces
app.UseCors( 
    options => options.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyOrigin()
    );

app.MapControllers();

app.Run();
