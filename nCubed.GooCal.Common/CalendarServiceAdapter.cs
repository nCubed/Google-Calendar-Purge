using Google.GData.Calendar;

namespace nCubed.GooCal.Common
{
    /// <summary>
    /// Adapater class to expose a subset of the Google <see cref="CalendarService"/>
    /// as an interface.
    /// </summary>
    internal class CalendarServiceAdapter : CalendarService, IGoogleCalendarService
    {
        public CalendarServiceAdapter( string applicationName )
            : base( applicationName )
        { }
    }
}