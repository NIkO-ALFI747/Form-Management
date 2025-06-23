using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Form_Management.Api.DataAccess;

public class DataProtectionKeyDbContext(DbContextOptions<DataProtectionKeyDbContext> options) 
    : DbContext(options), IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}