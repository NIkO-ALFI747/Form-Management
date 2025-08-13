using FluentValidation.Results;
using Form_Management.Api.Contracts.Response.Error.ResponseObject.ValidationErrors;
using Mapster;

namespace Form_Management.Api.Contracts.Response.Error.Mapping;

public class ErrorResponseMappingConfig
{
    public static void RegisterValidationMappings<TErrorObject>()
        where TErrorObject : class
    {
        TypeAdapterConfig<ValidationFailure, ValidationErrorResponseObject<TErrorObject>>.NewConfig()
            .Map(dest => dest.InternalError, src => src.CustomState as TErrorObject)
            .Map(dest => dest.ErrorCode, src => src.ErrorCode)
            .Map(dest => dest.ErrorMessage, src => src.ErrorMessage)
            .Map(dest => dest.InvalidField, src => src.PropertyName);

        TypeAdapterConfig<IEnumerable<ValidationFailure>, Dictionary<string, List<ValidationErrorResponseObject<TErrorObject>>>>.NewConfig()
            .MapWith(src => src
                .GroupBy(error => error.PropertyName)
                .ToDictionary(
                    group => group.Key,
                    group => group.Adapt<List<ValidationErrorResponseObject<TErrorObject>>>()
                )
            );

        TypeAdapterConfig<IEnumerable<ValidationFailure>, ValidationErrorsResponseObject<ValidationErrorResponseObject<TErrorObject>>>.NewConfig()
            .ConstructUsing(src => new ValidationErrorsResponseObject<ValidationErrorResponseObject<TErrorObject>>(
                src.Adapt<Dictionary<string, List<ValidationErrorResponseObject<TErrorObject>>>>()));
    }
}