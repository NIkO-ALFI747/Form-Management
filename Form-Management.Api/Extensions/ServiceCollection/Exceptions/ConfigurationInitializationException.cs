using Form_Management.Domain.Errors.Error;

namespace Form_Management.Api.Extensions.ServiceCollection.Exceptions;

public class ConfigurationInitializationException : Exception
{
    public AbstractError? Error { get; }

    public ConfigurationInitializationException(AbstractError error)
        : base($"Application startup configuration failed: {error.Code} - {error.Message}")
    {
        Error = error;
    }

    public ConfigurationInitializationException(AbstractError error, string message)
        : base(message)
    {
        Error = error;
    }

    public ConfigurationInitializationException(string message)
        : base(message)
    {}
}