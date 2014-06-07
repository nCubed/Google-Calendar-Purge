using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Calendar;

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
            throw new NotImplementedException();
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
