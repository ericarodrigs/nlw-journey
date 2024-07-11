using Journey.Communication.Responses;
using Journey.Exception.ExceptionsBase;
using Journey.Exception.ResourceErrorsMessage;
using Journey.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Journey.Application;

public class DeleteTripByIdUseCase
{
    public void Execute(Guid id)
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

        dbContext.Trips.Remove(trip);
        dbContext.SaveChanges();
    }
}
