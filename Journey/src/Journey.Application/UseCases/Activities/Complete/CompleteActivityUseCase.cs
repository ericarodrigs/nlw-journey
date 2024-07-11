using Journey.Exception.ExceptionsBase;
using Journey.Exception.ResourceErrorsMessage;
using Journey.Infrastructure;
using Journey.Infrastructure.Enums;

namespace Journey.Application;

public class CompleteActivityUseCase
{
    public void Execute(Guid tripId, Guid activityId)
    {
        var dbContext = new JourneyDbContext();

        var activity = dbContext
            .Activities
            .FirstOrDefault(activity => activity.Id == activityId && activity.TripId == tripId);

        if (activity is null)
        {
            throw new NotFoundException(ResourceErrorsMessage.ACTIVITY_NOT_FOUND);
        }

        activity.Status = ActivityStatus.Done;

        dbContext.Activities.Update(activity);
        dbContext.SaveChanges();
    }
}
