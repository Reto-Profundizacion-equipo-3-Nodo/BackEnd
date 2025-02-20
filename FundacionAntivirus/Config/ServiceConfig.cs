using FundacionAntivirus.Data;
using FundacionAntivirus.Interface;
using FundacionAntivirus.Services;
using Microsoft.EntityFrameworkCore;

namespace FundacionAntivirus.Config
{
    public static class ServiceConfig
    {
        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Inyección de dependencias
            services.AddScoped<IUsers, UsersService>();
            
            // Configuración de la base de datos
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}