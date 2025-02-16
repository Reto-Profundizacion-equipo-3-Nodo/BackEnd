using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using FundAntivirus.Data;
using FundAntivirus.Services;
using FundAntivirus.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ==================== CONFIGURACIÓN DE SERVICIOS ====================
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// ==================== CONFIGURACIÓN DE MIDDLEWARES ====================
ConfigureMiddleware(app);

app.Run();

// ==================== MÉTODOS AUXILIARES ====================
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // ========= CONFIGURACIÓN DE AUTENTICACIÓN JWT =========
    var jwtConfig = configuration.GetSection("Jwt");
    var jwtKey = jwtConfig["Key"] ?? throw new InvalidOperationException("JWT Key is missing.");
    var key = Encoding.UTF8.GetBytes(jwtKey);

    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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

    // ========= CONFIGURACIÓN DE BASE DE DATOS =========
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    // ========= REGISTRO DE SERVICIOS =========
    services.AddScoped<AuthService>();
    services.AddScoped<ICategoryRepository, CategoryRepository>();

    // ========= CONFIGURACIÓN DE CONTROLADORES =========
    services.AddControllers().AddDataAnnotationsLocalization();

    // ========= CONFIGURACIÓN DE SWAGGER =========
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "FundAntivirus API",
            Version = "v1",
            Description = "API para la gestión de FundAntivirus"
        });

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
}

void ConfigureMiddleware(WebApplication app)
{
    // ========= SWAGGER (Solo en entorno de desarrollo) =========
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "FundAntivirus API v1");
        });
    }

    // ========= AUTENTICACIÓN Y AUTORIZACIÓN =========
    app.UseAuthentication();
    app.UseAuthorization();

    // ========= MIDDLEWARE PERSONALIZADO PARA VALIDACIÓN DE MODELO =========
    app.Use(async (context, next) =>
    {
        var actionContext = context.RequestServices.GetRequiredService<Microsoft.AspNetCore.Mvc.Infrastructure.IActionContextAccessor>()
            .ActionContext;

        if (actionContext?.ModelState != null && !actionContext.ModelState.IsValid)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsJsonAsync(new
            {
                message = "Solicitud inválida",
                errors = actionContext.ModelState
                    .Where(x => x.Value!.Errors.Any())
                    .ToDictionary(k => k.Key, v => v.Value!.Errors.Select(e => e.ErrorMessage).ToArray())
            });
            return;
        }

        await next();
    });

    // ========= MAPEO DE CONTROLADORES =========
    app.MapControllers();
}

// Clase parcial necesaria para las pruebas de integración
public partial class Program { }