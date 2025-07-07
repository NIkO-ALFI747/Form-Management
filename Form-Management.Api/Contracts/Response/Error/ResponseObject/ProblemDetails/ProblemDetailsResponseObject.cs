using MvcProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Form_Management.Api.Contracts.Response.Error.ResponseObject.ProblemDetails;

public class ProblemDetailsResponseObject<TErrorsResponseObject> : MvcProblemDetails
{
    public const string DETAIL = "See the 'errors' property for more details.";

    public ProblemDetailsResponseObject(string instance, string traceIdentifier,
        TErrorsResponseObject errors, string title,
        int status, string detail = DETAIL
        ) : base()
    {
        Status = status;
        Title = title;
        Detail = detail;
        Instance = instance;
        Extensions.Add("traceId", traceIdentifier);
        Extensions.Add("errors", errors);
    }
}