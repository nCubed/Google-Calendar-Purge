using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nCubed.GooCal.Common;

namespace nCubed.GooCal.UnitTests
{
    [TestClass]
    public class CalendarPurgeTests
    {
        [TestMethod]
        public void Class_Implements_ICalendarPurge()
        {
            var type = typeof( CalendarPurge );

            bool isAssignable = typeof( ICalendarPurge ).IsAssignableFrom( type );

            Assert.IsTrue( isAssignable );
        }

        [TestMethod]
        [ExpectedException( typeof( NotImplementedException ) )]
        public void PurgeAll_NotImplemented()
        {
            var calPurge = new CalendarPurge();

            calPurge.PurgeAll();
        }

        [TestMethod]
        [ExpectedException( typeof( NotImplementedException ) )]
        public void Purge_NotImplemented()
        {
            var calPurge = new CalendarPurge();
            var start = new DateTime( 2014, 1, 1 );
            var end = new DateTime( 2014, 1, 31 );

            calPurge.Purge( start, end );
        }

        [TestMethod]
        [ExpectedException( typeof( NotImplementedException ) )]
        public void HasEvents_NotImplemented()
        {
            var calPurge = new CalendarPurge();

            calPurge.HasEvents();
        }

        [TestMethod]
        public void Class_IsInternal()
        {
            var type = typeof( CalendarPurge );

            bool isInternal = type.IsNotPublic;

            Assert.IsTrue( isInternal );
        }
    }
}
