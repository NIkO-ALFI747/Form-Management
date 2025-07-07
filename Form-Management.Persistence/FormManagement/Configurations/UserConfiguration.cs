using Form_Management.Domain.Models.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Form_Management.Persistence.FormManagement.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users").HasKey(u => u.Id);

        builder
            .ComplexProperty(u => u.Name, nb =>
            {
                nb.Property(n => n.Value).HasColumnName("Name").HasMaxLength(100);
            })
            .ComplexProperty(u => u.Email, eb =>
            {
                eb.Property(e => e.Value).HasColumnName("Email").HasMaxLength(100);
            })
            .ComplexProperty(u => u.Password, pb =>
            {
                pb.Property(p => p.Value).HasColumnName("Password").HasMaxLength(100);
            });

        builder.Property<string>("EmailValue").HasColumnName("Email");

        builder
            .HasIndex("EmailValue")
            .IsUnique();
    }
}