using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Calendar;
using Google.GData.Client;

namespace nCubed.GooCal.Common
{
    internal sealed class CalendarPurge : ICalendarPurge
    {
        private readonly IGoogleCalendarService _service;
        private readonly string _calendarUrl;

        public CalendarPurge( IGoogleCalendarService service, string calendarUrl )
        {
            _service = service;
            _calendarUrl = calendarUrl;
        }

        public IEnumerable<string> PurgeAll()
        {
            var query = new FeedQuery( _calendarUrl );

            AtomFeed feed = _service.Query( query );

            if( !feed.Entries.Any() )
            {
                return Enumerable.Empty<string>();
            }

            var deleted = new List<string>();

            while( feed.Entries.Any() )
            {
                foreach( AtomEntry entry in feed.Entries )
                {
                    entry.Delete();
                    deleted.Add( entry.Title.Text );
                }

                feed = _service.Query( query );
            }

            return deleted;
        }

        public IEnumerable<string> Purge( DateTime start, DateTime end )
        {
            throw new NotImplementedException();
        }

        public bool HasEvents()
        {
            var query = new EventQuery( _calendarUrl );

            EventFeed feed = _service.Query( query );

            return feed.Entries.Any();
        }
    }
}
