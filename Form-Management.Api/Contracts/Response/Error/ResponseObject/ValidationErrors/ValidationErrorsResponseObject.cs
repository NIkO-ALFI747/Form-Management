﻿namespace Form_Management.Api.Contracts.Response.Error.ResponseObject.ValidationErrors;

public record ValidationErrorsResponseObject<TValidationErrorResponseObject>(Dictionary<string, List<TValidationErrorResponseObject>> ValidationsErrors);