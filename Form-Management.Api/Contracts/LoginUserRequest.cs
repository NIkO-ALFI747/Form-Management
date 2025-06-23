using System.ComponentModel.DataAnnotations;

namespace Form_Management.Api.Contracts;

public record LoginUserRequest
(
    [Required] string Email,
    [Required] string Password
);