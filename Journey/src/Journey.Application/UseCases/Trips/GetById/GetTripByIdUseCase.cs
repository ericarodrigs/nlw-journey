using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception.ResourceErrorsMessage;
using Journey.Infrastructure;
using Journey.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application;

public class GetTripByIdUseCase
{
public ResponseTripJson Execute(Guid id)
    {
        var dbContext = new JourneyDbContext();
        var trip = dbContext
            .Trips
            .Include(trip => trip.Activities)
            .FirstOrDefault(trip => trip.Id == id);

        if (trip is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.TRIP_NOT_FOUND);
        }

        return new ResponseTripJson
        {
            Id = trip.Id,
            Name = trip.Name,
            StartDate = trip.StartDate,
            EndDate = trip.EndDate,
            Activities = trip.Activities.Select(activity => new ResponseActivityJson
            {
                Id = activity.Id,
                Name = activity.Name,
                Date = activity.Date,
                Status = (Communication.Enums.ActivityStatus)activity.Status,
            }).ToList()
        };
    }
}
