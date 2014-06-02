using System;
using Google.GData.Calendar;

namespace nCubed.GooCal.Common
{
    internal class CalendarConnection : ICalendarConnection
    {
        public ICalendarPurge Connect( ICalendarCredentials credentials )
        {
            var service = new CalendarService( "GooCalPurge" );

            service.setUserCredentials( credentials.UserName, credentials.Password );

            // The query will throw an InvalidCredentialsException if credentials
            // are incorrect.
            service.QueryClientLoginToken();

            return null;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
