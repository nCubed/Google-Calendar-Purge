namespace nCubed.GooCal
{
    /// <summary>
    /// Interface for authenticating to a specific Google Calendar.
    /// </summary>
    public interface ICalendarCredentials
    {
        /// <summary>
        /// The Google account user name.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// The Google account password.
        /// </summary>
        string Password { get; }

        /// <summary>
        /// The URL of a specific calendar.
        /// </summary>
        string CalendarUrl { get; }
    }
}
