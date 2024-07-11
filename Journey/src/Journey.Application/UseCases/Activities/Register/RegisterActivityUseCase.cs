using FluentValidation.Results;
using Journey.Communication.Requests;
using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception.ResourceErrorsMessage;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application.UseCases.Activities.Register;

public class RegisterActivityUseCase
{
    public ResponseActivityJson Execute(Guid tripId, RequestRegisterActivityJson request)
    {
        var dbContext = new JourneyDbContext();
        var trip = dbContext
            .Trips
            .FirstOrDefault(trip => trip.Id == tripId);

        Validate(trip, request);

        var activity = new Activity
        {
            Name = request.Name,
            Date = request.Date,
            TripId = tripId,
        };

        dbContext.Activities.Add(activity);
        dbContext.SaveChanges();

        return new ResponseActivityJson
        {
            Id = activity.Id,
            Name = activity.Name,
            Date = activity.Date,
            Status = (Communication.Enums.ActivityStatus)activity.Status,
        };
    }

    public void Validate(Trip? trip, RequestRegisterActivityJson request)
    {
        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.TRIP_NOT_FOUND);
        }

        var validator = new RegisterActivityValidator();
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
