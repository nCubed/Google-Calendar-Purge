using Google.GData.Calendar;
using Google.GData.Client;

namespace nCubed.GooCal.Common
{
    /// <summary>
    /// Adapter for the Google <see cref="Google.GData.Calendar.CalendarService"/> class.
    /// </summary>
    public interface IGoogleCalendarService
    {
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