using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FundAntivirus.Data;
using FundAntivirus.Services;
using FundAntivirus.Repositories; // Se agregan los repositorios
using System.Text;

// ==========================================================
// FundAntivirus API - Configuración principal
// Este archivo inicializa la autenticación JWT, el contexto 
// de base de datos, los servicios, repositorios y Swagger.
// ==========================================================

var builder = WebApplication.CreateBuilder(args);

// 🔹 Obtener configuración JWT desde appsettings.json
var jwtConfig = builder.Configuration.GetSection("Jwt");

// 🔹 Validar que la clave JWT no sea nula
var jwtKey = jwtConfig["Key"] ?? throw new InvalidOperationException("JWT Key is missing.");
var key = Encoding.UTF8.GetBytes(jwtKey);

// 🔹 Configurar autenticación con JWT
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtConfig["Issuer"],
            ValidAudience = jwtConfig["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

// 🔹 Configurar DbContext con PostgreSQL
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Registrar servicios de la aplicación
builder.Services.AddScoped<AuthService>();

// 🔹 Registrar repositorios y servicios relacionados con categorías y oportunidades
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOpportunityService, OpportunityService>();
builder.Services.AddScoped<IOpportunityRepository, OpportunityRepository>();

// 🔹 Agregar soporte para controladores en la API
builder.Services.AddControllers();

// 🔹 Configurar Swagger para documentación de la API con autenticación JWT
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "FundAntivirus API",
        Version = "v1",
        Description = "API para la gestión de FundAntivirus"
    });

    // Configuración de seguridad para Swagger
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Ingrese el token en el formato: Bearer {token}",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
    };

    options.AddSecurityDefinition("Bearer", securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { securityScheme, new string[] { } }
    });
});

var app = builder.Build();

// 🔹 Configurar middlewares y entorno de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "FundAntivirus API v1");
    });
}

// 🔹 Activar autenticación y autorización en la API
app.UseAuthentication();
app.UseAuthorization();

// 🔹 Mapear controladores de la API
app.MapControllers();

// 🔹 Ejecutar la aplicación
app.Run();