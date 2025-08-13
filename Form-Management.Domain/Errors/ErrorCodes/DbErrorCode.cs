using CSharpFunctionalExtensions;
using Form_Management.Domain.Errors.Validation;
using Form_Management.Domain.Models.ValueObjects.General;

namespace Form_Management.Domain.Errors.ErrorCodes;

public class DbErrorCode : EnumValueObject
{
    public static readonly DbErrorCode RecordsNotFound = new(nameof(RecordsNotFound));

    public static readonly DbErrorCode FailedToDelete = new(nameof(FailedToDelete));

    public static readonly DbErrorCode ValueAlreadyExist = new(nameof(ValueAlreadyExist));

    public static readonly DbErrorCode DatabaseError = new(nameof(DatabaseError));

    private static readonly DbErrorCode[] _all = [RecordsNotFound, FailedToDelete,
        ValueAlreadyExist, DatabaseError];

    private DbErrorCode(string value) : base(value) { }

    public static Result<EnumValueObject, ValueObjectValidationError> Create(string? value) => Create(value, _all);
}