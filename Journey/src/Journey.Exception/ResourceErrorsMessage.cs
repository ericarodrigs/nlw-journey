namespace Journey.Exception.ResourceErrorsMessage;

public class ResourceErrorsMessage
{
    public static string NAME_EMPTY => "The name cannot be empty";
    public static string DATE_TRIP_MUST_BE_LATER_THAN_TODAY => "The date of the trip must be later than today";
    public static string END_DATE_TRIP_MUST_BE_LATER_THAN_START_DATE => "The end date of the trip must be equal to or later than the start date";
    public static string UNKNOWN_ERROR => "Unknown error";
    public static string TRIP_NOT_FOUND => "Trip not found";
    public static string DATE_NOT_WITHIN_TRAVEL_PERIOD => "The date you selected for the activity is not within the travel period";
    public static string ACTIVITY_NOT_FOUND => "Activity not found";

}
