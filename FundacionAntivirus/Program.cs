using FundacionAntivirus.Config;
using FundacionAntivirus.Data;
using FundacionAntivirus.Services;
using FundacionAntivirus.Interfaces;
using FundacionAntivirus.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//Habilitar los CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:5173")
                .AllowAnyHeader()//Permitir cualquier header
                .AllowAnyMethod();//Permitir cualquier metodo
        });
});

// Agregar controladores y vistas
builder.Services.AddControllersWithViews();

// Configuración de Swagger
builder.Services.ConfigureSwagger();

// Configuración de servicios personalizados
builder.Services.ConfigureServices(builder.Configuration);

// AutoMapper para mapeo de objetos
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Inyección de dependencias de servicios
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IOpportunityService, OpportunityService>();
builder.Services.AddScoped<IInstitutionService, InstitutionService>(); // Se conserva también
builder.Services.AddScoped<IOpportunityInstitutionService, OpportunityInstitutionService>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();

// Configuración de la conexión a PostgreSQL desde appsettings.json (Buena práctica)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias para los repositorios
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IOpportunityRepository, OpportunityRepository>();

var app = builder.Build();

// Configuración de Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Antivirus V1");
});

//Pagina de inicio con swagger
app.Use(async (context, next) =>
{
    if (context.Request.Path.Value == "/")
    {
        context.Response.Redirect("/swagger");
    }
    await next();

});

// Configuración del pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

// Configuración de archivos estáticos (si aplica)
app.MapStaticAssets();

// Configuración de rutas
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();