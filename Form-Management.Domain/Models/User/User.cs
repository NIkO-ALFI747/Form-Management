using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Models.User.ValueObjects;
using Form_Management.Domain.Models.ValueObjects;

namespace Form_Management.Domain.Models.User;

public class User : Entity<long>
{
    public FilledString Name { get; private set; }

    public Email Email { get; private set; }

    public Password Password { get; private set; }

    private User() {
        Name = FilledString.Create("PlaceholderName").Value;
        Email = Email.Create("placeholder@example.com").Value;
        Password = Password.Create("PlaceholderP@ssw0rd!1").Value;
    }

    private User(FilledString name, Email email, Password password)
    {
        Name = name;
        Email = email;
        Password = password;
    }

    public static Result<User, ValueObjectValidationError> Create(string? name, string? email, string? password) =>
        FilledString.Create(name)
        .Bind(nameValue => Email.Create(email)
        .Bind(emailValue => Password.Create(password)
        .Map(passwordValue => new User(nameValue, emailValue, passwordValue))));
}