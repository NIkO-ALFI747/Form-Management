using Form_Management.Persistence.FormManagement.Configurations;
using Form_Management.Persistence.FormManagement.Entities;
using Form_Management.Persistence.FormManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Form_Management.Persistence.FormManagement;

public class FormManagementDbContext(DbContextOptions<FormManagementDbContext> options, IOptions<AuthorizationOptions> authOptions) : DbContext(options)
{
    private readonly AuthorizationOptions _authOptions = authOptions?.Value ?? throw new ArgumentNullException(nameof(authOptions));

    public DbSet<UserEntity> Users { get; set; }
    
    public DbSet<RoleEntity> Roles { get; set; }

    public string UsersEmailIndex { get; } = "IX_Users_Email";

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(_authOptions));
    }
}
