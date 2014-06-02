using System;
using System.Collections.Generic;

namespace nCubed.GooCal.Common
{
    internal class CalendarPurge : ICalendarPurge
    {
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
            throw new NotImplementedException();
        }
    }
}
