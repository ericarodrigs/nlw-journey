using System.Net;

namespace Journey.Exception.ExceptionsBase;

public class ErrorOnValidationException : JourneyException
{
    private readonly IList<string> _errorMessages;
    public ErrorOnValidationException(List<string> message) : base(string.Empty)
    {
        _errorMessages = message;
    }

    public override IList<string> GetErrorMessages()
    {
        return _errorMessages;
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.BadRequest;
    }
}
