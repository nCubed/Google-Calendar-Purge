using System;
using Google.GData.Calendar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nCubed.GooCal.Common;

namespace nCubed.GooCal.UnitTests
{
    [TestClass]
    public class CalendarPurgeTests
    {
        private Mock<IGoogleCalendarService> _service;
        private ICalendarPurge _calendarPurge;

        [TestInitialize]
        public void Initialize()
        {
            _service = new Mock<IGoogleCalendarService>( MockBehavior.Strict );
            _calendarPurge = new CalendarPurge( _service.Object, "" );
        }

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
            var calPurge = new CalendarPurge( _service.Object, "url" );

            calPurge.PurgeAll();
        }

        [TestMethod]
        [ExpectedException( typeof( NotImplementedException ) )]
        public void Purge_NotImplemented()
        {
            var calPurge = new CalendarPurge( _service.Object, "url" );
            var start = new DateTime( 2014, 1, 1 );
            var end = new DateTime( 2014, 1, 31 );

            calPurge.Purge( start, end );
        }

        [TestMethod]
        public void Class_IsInternal()
        {
            var type = typeof( CalendarPurge );

            bool isInternal = type.IsNotPublic;

            Assert.IsTrue( isInternal );
        }

        [TestMethod]
        public void HasEvents_NoEvents_ReturnsFalse()
        {
            _service.Setup( x => x.Query( It.IsAny<EventQuery>() ) ).Returns( new EventFeed( new Uri( "http://www.google.com" ), null ) );

            bool hasEvents = _calendarPurge.HasEvents();

            Assert.IsFalse( hasEvents );
        }
    }
}
