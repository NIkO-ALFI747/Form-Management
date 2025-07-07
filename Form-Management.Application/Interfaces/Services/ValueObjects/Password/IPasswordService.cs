using TPassword = Form_Management.Domain.Models.User.ValueObjects.Password;

namespace Form_Management.Application.Interfaces.Services.ValueObjects.Password;

public interface IPasswordService
{
    Task<bool> IsPasswordNotPwnedAsync(TPassword password, CancellationToken cancellationToken);
}