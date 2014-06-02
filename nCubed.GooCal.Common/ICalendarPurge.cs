using System;
using System.Collections.Generic;

namespace nCubed.GooCal.Common
{
    /// <summary>
    /// Interface for purging events from a Google Calendar.
    /// </summary>
    public interface ICalendarPurge
    {
        /// <summary>
        /// Purges all events from a Google Calendar.
        /// </summary>
        /// <returns>Returns names of calendar events that were deleted.</returns>
        IEnumerable<string> PurgeAll();

        /// <summary>
        /// Purges events from a Google Calendar within a specified date range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>Returns names of calendar events that were deleted.</returns>
        IEnumerable<string> Purge( DateTime start, DateTime end );

        /// <summary>
        /// Checks whether a calendar has any events.
        /// </summary>
        /// <returns><c>true</c> if the calendar has any events, otherwise <c>false</c>.</returns>
        bool HasEvents();
    }
}
