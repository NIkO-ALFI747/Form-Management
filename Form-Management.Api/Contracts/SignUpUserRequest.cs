using System.ComponentModel.DataAnnotations;

namespace Form_Management.Api.Contracts;

public record SignUpUserRequest
(
    [Required] string Name,
    [Required] string Email,
    [Required] string Password
);