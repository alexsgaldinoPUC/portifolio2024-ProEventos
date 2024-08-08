using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProEventos.API.Util.Services.Contratos.Uploads;
using ProEventos.API.Util.Services.Implementacao.Uploads;
using ProEventos.Application.Servicos.Contratos.Eventos;
using ProEventos.Application.Servicos.Contratos.Lotes;
using ProEventos.Application.Servicos.Contratos.Usuarios;
using ProEventos.Application.Servicos.Implementacao.Eventos;
using ProEventos.Application.Servicos.Implementacao.Lotes;
using ProEventos.Application.Servicos.Implementacao.Usuarios;
using ProEventos.Domain.Models.Usuarios;
using ProEventos.Persistence.Data;
using ProEventos.Persistence.Interfaces.Contratos.Eventos;
using ProEventos.Persistence.Interfaces.Contratos.Geral;
using ProEventos.Persistence.Interfaces.Contratos.Lotes;
using ProEventos.Persistence.Interfaces.Contratos.Usuarios;
using ProEventos.Persistence.Interfaces.Implementacao.Eventos;
using ProEventos.Persistence.Interfaces.Implementacao.Geral;
using ProEventos.Persistence.Interfaces.Implementacao.Lotes;
using ProEventos.Persistence.Interfaces.Implementacao.Usuarios;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Add context of project
builder.Services.AddDbContext<ProEventosContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

//Add Identity
builder.Services.AddIdentityCore<Usuario>(options =>
                    {
                        options.Password.RequireDigit = false;
                        options.Password.RequireNonAlphanumeric = false;
                        options.Password.RequireLowercase = false;
                        options.Password.RequireUppercase = false;
                        options.Password.RequiredLength = 4;
                    })
                .AddRoles<Papel>()
                .AddRoleManager<RoleManager<Papel>>()
                .AddSignInManager<SignInManager<Usuario>>()
                .AddRoleValidator<RoleValidator<Papel>>()
                .AddEntityFrameworkStores<ProEventosContext>()
                .AddDefaultTokenProviders();

// Add Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

// Add controllers
builder.Services.AddControllers()
                .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()))
                .AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Add Services and pesistence
builder.Services.AddScoped<IEventoServices, EventoServices>()
                .AddScoped<ILoteServices, LoteServices>()
                .AddScoped<IUploadServices, UploadServices>()
                .AddScoped<ITokenServices, TokenServices>()
                .AddScoped<IAccountServices, AccountServices>();

builder.Services.AddScoped<IGeralPersistence, GeralPersistence>()
                .AddScoped<IEventoPersistence, EventoPersistence>()
                .AddScoped<ILotePersistence, LotePersistence>()
                .AddScoped<IUsuarioPersistence, UsuarioPersistence>();

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
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "ProEventos.API", Version = "v1"});
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header usando Bearer. Entre com 'Bearer' + [espaço] + token para autenticação. Exemplo: Bearer 12345ttttgdffdv",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
