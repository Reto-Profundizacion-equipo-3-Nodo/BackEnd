using FundAntivirus.Data;
using FundAntivirus.Interfaces;
using FundAntivirus.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FundAntivirus.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;
        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.User.FindAsync(id);
        }
        public async Task<(bool Success, string Message)> CreateAsync(User User)
        {
            try
            {

                bool emailExists = await _context.User.AnyAsync(u => u.Email == User.Email);
                if (emailExists)
                {
                    return (false, "Email already exists");
                }
                await _context.User.AddAsync(User);
                await _context.SaveChangesAsync();

                return (true, "User created successfully");
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is PostgresException pgEx && pgEx.SqlState == "23505")
                {
                    return (false, "Email already exists, please try another one.");
                }

                return (false, "An unexpected error occurred.");
            }
            catch (Exception ex)
            {
                return (false, "An unexpected error occurred." + ex.Message);
            }
        }
        public async Task<User> UpdateAsync(int id, UpdateUserDto model)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                throw new Exception("User not found");
            }
            User.Name = model.Name;
            User.Email = model.Email;
            User.Role = model.Role;
            await _context.SaveChangesAsync();
            return User;
        }
        public async Task DeleteAsync(int id)
        {
            var User = await _context.User.FindAsync(id);
            if (User == null)
            {
                throw new Exception("User not found");
            }
            _context.User.Remove(User);
            await _context.SaveChangesAsync();
        }
    }
}
