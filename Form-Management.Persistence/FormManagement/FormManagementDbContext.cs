using Form_Management.Persistence.FormManagement.Configurations;
using Microsoft.EntityFrameworkCore;
using Form_Management.Domain.Models.User;

namespace Form_Management.Persistence.FormManagement;

public class FormManagementDbContext(DbContextOptions<FormManagementDbContext> options) 
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public string UsersEmailIndex { get; } = "IX_Users_Email";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        base.OnModelCreating(modelBuilder);
    }
}