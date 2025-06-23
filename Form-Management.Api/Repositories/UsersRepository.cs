using Form_Management.Api.DataAccess;
using Form_Management.Api.Interfaces.Repositories;
using Form_Management.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Form_Management.Api.Repositories;

public class UsersRepository(FormManagementDbContext context) : IUsersRepository
{
    private readonly FormManagementDbContext _context = context;

    public async Task Add(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.AsNoTracking().ToListAsync() ?? [];
    }

    public async Task<User> GetByEmail(string email)
    {
        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email)
            ?? throw new Exception();
        return user;
    }

    public async Task DeleteMultiple(int[] ids)
    {
        await _context.Users
        .Where(u => ids.Contains(u.Id))
        .ExecuteDeleteAsync();
    }
}