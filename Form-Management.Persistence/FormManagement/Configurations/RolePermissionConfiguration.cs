using Form_Management.Domain.AuthEnums;
using Form_Management.Persistence.FormManagement.Entities;
using Form_Management.Persistence.FormManagement.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Form_Management.Persistence.FormManagement.Configurations;

public class RolePermissionConfiguration(AuthorizationOptions authorizationOptions) : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorizationOptions = authorizationOptions;

    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        builder.HasKey(rp => new { rp.RoleId, rp.PermissionId });
        builder.HasData(ParseRolePermissions());
    }

    private RolePermissionEntity[] ParseRolePermissions()
    {
        var result = _authorizationOptions.RolePermissions
            .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                })
            )
            .ToArray();
        return result;
    }
}
