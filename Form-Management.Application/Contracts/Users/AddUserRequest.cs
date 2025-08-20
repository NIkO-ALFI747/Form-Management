namespace Form_Management.Application.Contracts.Users;

public record AddUserRequest(string Name, string Email, string Password, string Role = "User");