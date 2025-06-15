using Form_Management.Api.Models;

namespace Form_Management.Api.Interfaces.Repositories;

public interface IUsersRepository
{
    Task Add(User user);
    Task<IEnumerable<User>> GetAll();
}