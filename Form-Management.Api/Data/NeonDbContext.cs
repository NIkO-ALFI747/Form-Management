using Form_Management.Api.Configurations;
using Form_Management.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Form_Management.Api.Data;

public class NeonDbContext(DbContextOptions<NeonDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}