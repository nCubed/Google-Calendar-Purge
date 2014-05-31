using System;

namespace nCubed.Google.Calendar
{
    /// <summary>
    /// Interface for connecting to a specific Google Calendar.
    /// </summary>
    public interface ICalendarConnection : IDisposable
    {
        /// <summary>
        /// Connects to a specific calendar given a set of credentials.
        /// </summary>
        /// <param name="credentials">Credentials to use when attempting to create a connection.</param>
        /// <returns>A calendar event assassin.</returns>
        ICalendarPurge Connect( ICalendarCredentials credentials );
    }
}
