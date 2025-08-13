using Form_Management.Application.Contracts.Users;
using Form_Management.Domain.Models.User;
using Mapster;

namespace Form_Management.Application.Mapping;

public class ModelResponseMappingConfig()
{
    public static void RegisterModelMappings()
    {
        TypeAdapterConfig<User, GetAllUsersResponse>
            .NewConfig()
            .Map(dest => dest.Id, src => (int)src.Id)
            .Map(dest => dest.Name, src => src.Name != null ? src.Name.Value : string.Empty);
    }
}