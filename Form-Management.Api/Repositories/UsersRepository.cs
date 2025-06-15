using Form_Management.Api.Data;
using Form_Management.Api.Interfaces.Repositories;
using Form_Management.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Form_Management.Api.Repositories;

public class UsersRepository(NeonDbContext context) : IUsersRepository
{
    private readonly NeonDbContext _context = context;

    public async Task Add(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.AsNoTracking().ToListAsync() ?? [];
    }
}