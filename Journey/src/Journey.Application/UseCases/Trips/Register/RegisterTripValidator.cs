using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception.ResourceErrorsMessage;

namespace Journey.Application;

public class RegisterTripValidator : AbstractValidator<RequestRegisterTripJson>
{
    public RegisterTripValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorsMessage.NAME_EMPTY);

        RuleFor(request => request.StartDate.Date)
            .GreaterThanOrEqualTo(DateTime.UtcNow.Date)
            .WithMessage(ResourceErrorsMessage.DATE_TRIP_MUST_BE_LATER_THAN_TODAY);

        RuleFor(request => request)
            .Must(request => request.EndDate.Date >= request.StartDate.Date)
            .WithMessage(ResourceErrorsMessage.NAME_EMPTY);
    }
}
