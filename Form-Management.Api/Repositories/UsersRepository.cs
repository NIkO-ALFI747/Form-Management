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

    public async Task DeleteMultiple(int[] ids)
    {
        await _context.Users
        .Where(u => ids.Contains(u.Id))
        .ExecuteDeleteAsync();
    }
}