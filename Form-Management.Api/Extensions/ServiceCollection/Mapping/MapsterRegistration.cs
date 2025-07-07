using Form_Management.Api.Contracts.Response.Error.Mapping;
using Form_Management.Application.Mapping;
using Form_Management.Domain.Errors.Validation;

namespace Form_Management.Api.Extensions.ServiceCollection.Mapping;

public static class MapsterRegistration
{
    public static void RegisterMapping()
    {
        ErrorResponseMappingConfig.RegisterValidationMappings<ValueObjectValidationError>();
        ModelResponseMappingConfig.RegisterModelMappings();
    }
}