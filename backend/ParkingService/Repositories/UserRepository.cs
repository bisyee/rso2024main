using ParkingService.Data;
using ParkingService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ParkingSpotContext _context;

        public UserRepository(ParkingSpotContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string mail)
        {
            
           return await _context.users.FirstOrDefaultAsync(u => u.email == mail);
        }

        public async Task<User> AddAsync(User user)
        {
            await _context.users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateAsync(User user)
        {
            var existingUser = await _context.users.FindAsync(user.id);
            if (existingUser == null)
            {
                return null;
            }

            existingUser.id = user.id;
            existingUser.password = user.password;
            existingUser.email = user.email;

            _context.users.Update(existingUser);
            await _context.SaveChangesAsync();

            return existingUser;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.users.FindAsync(id);
            if (user == null)
            {
                return false;
            }

            _context.users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
