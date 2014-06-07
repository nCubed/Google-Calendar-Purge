using System;
using Google.GData.Calendar;
using Google.GData.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nCubed.GooCal.Common;
using nCubed.GooCal.UnitTests.TestUtils;

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
            AssertClass.ImplementsInterface<CalendarPurge, ICalendarPurge>();
        }

        [TestMethod]
        public void Class_IsInternal()
        {
            AssertClass.IsInternal<CalendarPurge>();
        }

        [TestMethod]
        public void Class_IsSealed()
        {
            AssertClass.IsSealed<CalendarPurge>();
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
        [ExpectedException( typeof( NotImplementedException ) )]
        public void PurgeAll_NotImplemented()
        {
            var calPurge = new CalendarPurge( _service.Object, "url" );

            calPurge.PurgeAll();
        }

        [TestMethod]
        public void HasEvents_NoEvents_ReturnsFalse()
        {
            var feed = new EventFeed( null, null );
            feed.Entries.Clear();

            _service.Setup( x => x.Query( It.IsAny<EventQuery>() ) ).Returns( feed );

            bool hasEvents = _calendarPurge.HasEvents();

            Assert.IsFalse( hasEvents );
        }

        [TestMethod]
        public void HasEvents_WithEvents_ReturnsTrue()
        {
            var feed = new EventFeed( null, null );
            feed.Entries.Add( new AtomEntry() );

            _service.Setup( x => x.Query( It.IsAny<EventQuery>() ) ).Returns( feed );

            bool hasEvents = _calendarPurge.HasEvents();

            Assert.IsTrue( hasEvents );
        }
    }
}
