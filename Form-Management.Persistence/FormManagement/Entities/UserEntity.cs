namespace Form_Management.Persistence.FormManagement.Entities;

public class UserEntity
{
    public long Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public required string Email { get; set; }

    public required string Password { get; set; }

    public ICollection<RoleEntity> Roles { get; set; } = [];
}