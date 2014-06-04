using Google.GData.Calendar;
using Google.GData.Client;

namespace nCubed.GooCal.Common
{
    /// <summary>
    /// Adapter for the Google <see cref="CalendarService"/> class
    /// </summary>
    public interface IGoogleCalendarService
    {
        // ReSharper disable once InconsistentNaming
        /// <summary>
        /// Sets the user credentials.
        /// </summary>
        /// <param name="userName">The Google username.</param>
        /// <param name="password">The user's password.</param>
        void setUserCredentials( string userName, string password );

        /// <summary>
        /// Queries the Google Calendar.
        /// </summary>
        /// <param name="feedQuery">The event query.</param>
        EventFeed Query( EventQuery feedQuery );

        /// <summary>
        /// Queries the Google Calendar.
        /// </summary>
        /// <param name="feedQuery">The feed query.</param>
        AtomFeed Query( FeedQuery feedQuery );
    }
}