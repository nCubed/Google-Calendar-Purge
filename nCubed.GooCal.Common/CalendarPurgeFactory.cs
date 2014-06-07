namespace nCubed.GooCal.Common
{
    public static class CalendarPurgeFactory
    {
        /// <summary>
        /// Calendar Purge Factory.
        /// </summary>
        /// <param name="credentials">The credentials to log into the calendar.</param>
        /// <returns>An <see cref="ICalendarPurge"/> object that has been authenticated against Google Services.</returns>
        /// <exception cref="Google.GData.Client.InvalidCredentialsException"></exception>
        public static ICalendarPurge Create( ICalendarCredentials credentials )
        {
            var service = new CalendarServiceAdapter( "GooCalPurge" );

            service.setUserCredentials( credentials.UserName, credentials.Password );

            // The query will throw an InvalidCredentialsException if credentials cannot be authenticated.
            service.QueryClientLoginToken();

            var purge = new CalendarPurge( service, credentials.CalendarUrl );

            return purge;
        }
    }
}