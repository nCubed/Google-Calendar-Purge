using System.Net;
using Google.GData.Calendar;
using Google.GData.Client;

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
            var service = new CalendarServiceAdapter( "GooCalPurge" )
            {
                Credentials = new GDataCredentials( credentials.UserName, credentials.Password )
            };

            Validate( credentials, service );

            var purge = new CalendarPurge( service, credentials.CalendarUrl );

            return purge;
        }

        private static void Validate( ICalendarCredentials credentials, CalendarServiceAdapter service )
        {
            var validator = new Validator( credentials, service );

            validator.Validate();
        }

        private class Validator
        {
            private readonly ICalendarCredentials _credentials;
            private readonly CalendarServiceAdapter _service;

            public Validator( ICalendarCredentials credentials, CalendarServiceAdapter service )
            {
                _credentials = credentials;
                _service = service;
            }

            public void Validate()
            {
                ValidateLogin();
                ValidateCalendarUrl();
            }

            private void ValidateLogin()
            {
                // The query will throw Google.GData.Client.InvalidCredentialsException if credentials cannot be authenticated.
                _service.QueryClientLoginToken();
            }

            /// <summary>
            /// Attempts to capture a Google.GData.Client.GDataRequestException.
            /// </summary>
            private void ValidateCalendarUrl()
            {
                var calQuery = new CalendarQuery( _credentials.CalendarUrl );

                try
                {
                    _service.Query( calQuery );
                }
                catch( GDataRequestException ex )
                {
                    // this will occur when the calendar URL is invalid, i.e.,
                    // the calendar hasn't been shared as public, but they inluced public in the url:
                    // http://www.google.com/calendar/feeds/UserName%40gmail.com/public/basic
                    // should be:
                    // http://www.google.com/calendar/feeds/UserName%40gmail.com/private/basic
                    // finally, in order to modify events, the url needs to have the full modifier as well:
                    // http://www.google.com/calendar/feeds/UserName%40gmail.com/private/full

                    // TODO: try correcting the URL by replacing /public/ with /private/ and re-authenticating, if fails, throw same exception as currenlty throwing.
                    // TODO: try correcting the URL by replacing /basic with /full and re-authenticating, if fails, throw same exception as currenlty throwing.

                    if( ex.InnerException is WebException && ex.InnerException.Message == "The remote server returned an error: (403) Forbidden." )
                    {
                        string msg = string.Format( "{0} : {1}", ex.InnerException.Message, ex.Message );
                        throw new AuthenticationException( msg, ex );
                    }

                    throw;
                }
            }

        }
    }
}