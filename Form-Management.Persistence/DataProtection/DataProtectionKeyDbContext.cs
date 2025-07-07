using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Form_Management.Persistence.DataProtection;

public class DataProtectionKeyDbContext(DbContextOptions<DataProtectionKeyDbContext> options) 
    : DbContext(options), IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }
}