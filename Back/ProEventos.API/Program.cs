using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProEventos.API.Util.Services.Contratos.Uploads;
using ProEventos.API.Util.Services.Implementacao.Uploads;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Contratos.Lotes;
using ProEventos.Application.Servicos.Implementacao.Eventos;
using ProEventos.Application.Servicos.Implementacao.Lotes;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using ProEventos.Persistence.Interfaces.Contratos.Lotes;
using ProEventos.Persistence.Interfaces.Implementacao.Eventos;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;
using ProEventos.Persistence.Interfaces.Implementacao.Lotes;

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
builder.Services.AddScoped<IEventoServices, EventoServices>()
                .AddScoped<ILoteServices, LoteServices>()
                .AddScoped<IUploadServices, UploadServices>();

builder.Services.AddScoped<IGeralPersistence, GeralPersistence>()
                .AddScoped<IEventoPersistence, EventoPersistence>()
                .AddScoped<ILotePersistence, LotePersistence>();

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

// Add Upload Config
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Recursos")),
    RequestPath = new PathString("/Recursos")
});

app.UseAuthorization();

app.MapControllers();

app.Run();
