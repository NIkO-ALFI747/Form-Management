using Form_Management.Persistence.FormManagement.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Form_Management.Persistence.FormManagement.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Name).HasMaxLength(100);
        builder.Property(u => u.Email).HasMaxLength(100);
        builder.Property(u => u.Password).HasMaxLength(100);
        builder.HasIndex("Email").IsUnique();
        
        builder.HasMany(u => u.Roles)
            .WithMany(r => r.Users)
            .UsingEntity<UserRoleEntity>(
                l => l.HasOne<RoleEntity>().WithMany().HasForeignKey(ur => ur.RoleId),
                r => r.HasOne<UserEntity>().WithMany().HasForeignKey(ur => ur.UserId)
            );
    }
}