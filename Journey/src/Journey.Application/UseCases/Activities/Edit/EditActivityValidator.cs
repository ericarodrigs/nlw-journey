using FluentValidation;
using Journey.Communication.Requests;
using Journey.Exception.ResourceErrorsMessage;

namespace Journey.Application;

public class EditActivityValidator : AbstractValidator<RequestEditActivityJson>
{
    public EditActivityValidator()
    {
        RuleFor(request => request.Name)
            .NotNull()
            .WithMessage(ResourceErrorsMessage.NAME_EMPTY);

    }
}
