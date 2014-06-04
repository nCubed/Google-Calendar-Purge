namespace nCubed.GooCal.Common
{
    public static class CalendarPurgeFactory
    {
        public static ICalendarPurge Create( ICalendarCredentials credentials )
        {
            var service = new CalendarServiceAdapter( "GooCalPurge" );

            service.setUserCredentials( credentials.UserName, credentials.Password );

            // The query will throw an InvalidCredentialsException if credentials
            // are incorrect.
            service.QueryClientLoginToken();

            var purge = new CalendarPurge( service, credentials.CalendarUrl );

            return purge;
        }
    }
}