using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Calendar;
using Google.GData.Client;

namespace nCubed.GooCal.Common
{
    internal sealed class CalendarPurge : ICalendarPurge, IExposeGoogleCalendarService
    {
        private readonly IGoogleCalendarService _service;
        private readonly string _calendarUrl;

        #region Implementation of IExposeGoogleCalendarService

        /// <summary>
        /// Exposes the Google <see cref="CalendarService"/>; primarily exposed to enable 
        /// integration testing access to the already authenticated Google service.
        /// </summary>
        public CalendarService GoogleCalendarService
        {
            get { return (CalendarService)_service; }
        }

        #endregion

        public CalendarPurge( IGoogleCalendarService service, string calendarUrl )
        {
            _service = service;
            _calendarUrl = calendarUrl;
        }

        #region Implementation of ICalendarPurge

        /// <summary>
        /// Purges all events from a Google Calendar.
        /// </summary>
        /// <returns>Returns names of calendar events that were deleted.</returns>
        public IEnumerable<string> PurgeAll()
        {
            var query = new FeedQuery( _calendarUrl );

            AtomFeed feed = _service.Query( query );

            if( !feed.Entries.Any() )
            {
                return Enumerable.Empty<string>();
            }

            var deleted = new List<string>();

            // this is not recursion; creating a new method that recursively calls
            // itself because you think it's clever is simple ignorance of recursion.
            // there, i said it!
            while( feed.Entries.Any() )
            {
                foreach( AtomEntry entry in feed.Entries )
                {
                    entry.Delete();
                    deleted.Add( entry.Title.Text );
                }

                feed = _service.Query( query );
            }

            // TODO: deleted should return a small property bag struct with EventDate and EventName.
            return deleted;
        }

        /// <summary>
        /// Purges events from a Google Calendar within a specified date range.
        /// </summary>
        /// <param name="start">The start date.</param>
        /// <param name="end">The end date.</param>
        /// <returns>Returns names of calendar events that were deleted.</returns>
        public IEnumerable<string> Purge( DateTime start, DateTime end )
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Checks whether a calendar has any events.
        /// </summary>
        /// <returns><c>true</c> if the calendar has any events, otherwise <c>false</c>.</returns>
        public bool HasEvents()
        {
            var query = new EventQuery( _calendarUrl )
            {
                NumberToRetrieve = 1,
            };

            EventFeed feed = _service.Query( query );

            return feed.Entries.Any();
        }

        #endregion

    }
}
