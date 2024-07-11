using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception.ResourceErrorsMessage;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;

namespace Journey.Application;

public class EditActivityUseCase
{
    public ResponseActivityJson Execute(Guid tripId, Guid activityId, RequestEditActivityJson request)
    {
        var dbContext = new JourneyDbContext();

        var trip = dbContext
            .Trips
            .FirstOrDefault(trip => trip.Id == tripId);

        var activity = dbContext
            .Activities
            .FirstOrDefault(activity => activity.Id == activityId && activity.TripId == tripId);

        ValidateRequest(activity, request);

        Validate(trip, activity, request);

        dbContext.Activities.Update(activity!);
        dbContext.SaveChanges();

        return new ResponseActivityJson
        {
            Id = activity!.Id,
            Name = activity.Name,
            Date = activity.Date,
            Status = (Communication.Enums.ActivityStatus)activity.Status,
        };
    }

    private void ValidateRequest(Activity? activity, RequestEditActivityJson request)
    {
        if (activity is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.ACTIVITY_NOT_FOUND);
        }

        if (request.Name != null && request.Name.Length > 0)
        {
            activity!.Name = request.Name;
        }
        else
        {
            request.Name = activity!.Name;
        }

        if (request.Date != null)
        {
            activity!.Date = (DateTime)request.Date;
        }
        else
        {
            request.Date = activity!.Date;
        }
    }

    public void Validate(Trip? trip, Activity? activity, RequestEditActivityJson request)
    {
        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.TRIP_NOT_FOUND);
        }

        var validator = new EditActivityValidator();

        var result = validator.Validate(request);

        if (!(request.Date >= trip.StartDate && request.Date <= trip.EndDate))
        {
            result.Errors.Add(new ValidationFailure("Date", ResourceErrorsMessage.DATE_NOT_WITHIN_TRAVEL_PERIOD));
        }

        if (result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();
            throw new ErrorOnValidationException(errorMessages);
        }
    }
}
