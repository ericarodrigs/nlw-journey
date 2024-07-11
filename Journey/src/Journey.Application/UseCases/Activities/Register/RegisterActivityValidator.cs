using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception.ResourceErrorsMessage;

namespace Journey.Application;

public class RegisterActivityValidator : AbstractValidator<RequestRegisterActivityJson>
{
    public RegisterActivityValidator()
    {
        RuleFor(request => request.Name)
            .NotEmpty()
            .WithMessage(ResourceErrorsMessage.NAME_EMPTY);

    }
}
