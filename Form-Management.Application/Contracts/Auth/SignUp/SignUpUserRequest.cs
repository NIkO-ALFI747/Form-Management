using Form_Management.Application.Contracts.Users;

namespace Form_Management.Application.Contracts.Auth.SignUp;

public record SignUpUserRequest(string Name, string Email, string Password, string Role = "User")
    : AddUserRequest(Name, Email, Password, Role);