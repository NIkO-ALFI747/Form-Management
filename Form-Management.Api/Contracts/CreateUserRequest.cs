using System.ComponentModel.DataAnnotations;

namespace Form_Management.Api.Contracts;

public record CreateUserRequest
(
    [Required] string Name,
    [Required] string Email,
    [Required] string Password
);