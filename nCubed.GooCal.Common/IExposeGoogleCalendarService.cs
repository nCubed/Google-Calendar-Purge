using Google.GData.Calendar;

namespace nCubed.GooCal.Common
{
    /// <summary>
    /// Interface for exposing the <see cref="CalendarService"/>.
    /// </summary>
    internal interface IExposeGoogleCalendarService
    {
        /// <summary>
        /// Exposes the Google <see cref="CalendarService"/>; primarily exposed to enable 
        /// integration testing access to the already authenticated Google service.
        /// </summary>
        CalendarService GoogleCalendarService { get; }
    }
}