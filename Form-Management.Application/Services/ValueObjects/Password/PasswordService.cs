using Form_Management.Application.Interfaces.Services.ValueObjects.Password;
using Form_Management.Application.Services.ValueObjects.Password.Validation;
using TPassword = Form_Management.Domain.Models.User.ValueObjects.Password;

namespace Form_Management.Application.Services.ValueObjects.Password;

public class PasswordService : IPasswordService
{
    public async Task<bool> IsPasswordNotPwnedAsync(TPassword password, CancellationToken cancellationToken)
    {
        return await IsPasswordNotPwnedService.IsPasswordNotPwnedAsync(password, cancellationToken);
    }
}