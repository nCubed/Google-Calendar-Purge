﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <summary>
        /// Mock for the Google Calendar IService interface. The IService
        /// interface is used by Google to implement the CalendarService.
        /// </summary>
        private Mock<IService> _service;
        private Mock<IGoogleCalendarService> _calendarService;
        private CalendarPurge _calendarPurge;

        [TestInitialize]
        public void TestInit()
        {
            _service = new Mock<IService>( MockBehavior.Strict );
            _calendarService = new Mock<IGoogleCalendarService>( MockBehavior.Strict );
            _calendarPurge = new CalendarPurge( _calendarService.Object, string.Empty );
        }

        [TestMethod]
        public void Class_Implements_ICalendarPurge()
        {
            AssertClass.ImplementsInterface<CalendarPurge, ICalendarPurge>();
        }

        [TestMethod]
        public void Class_Implements_IExposeGoogleCalendarService()
        {
            AssertClass.ImplementsInterface<CalendarPurge, IExposeGoogleCalendarService>();
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
            var calPurge = new CalendarPurge( _calendarService.Object, "url" );
            var start = new DateTime( 2014, 1, 1 );
            var end = new DateTime( 2014, 1, 31 );

            calPurge.Purge( start, end );
        }

        [TestMethod]
        public void HasEvents_NoEvents_ReturnsFalse()
        {
            var feed = new EventFeed( null, null );

            _calendarService.Setup( x => x.Query( It.IsAny<EventQuery>() ) ).Returns( feed );

            bool hasEvents = _calendarPurge.HasEvents();

            Assert.IsFalse( hasEvents );
        }

        [TestMethod]
        public void HasEvents_WithEvents_ReturnsTrue()
        {
            var feed = new EventFeed( null, null );
            feed.Entries.Add( new EventEntry() );

            _calendarService.Setup( x => x.Query( It.IsAny<EventQuery>() ) ).Returns( feed );

            bool hasEvents = _calendarPurge.HasEvents();

            Assert.IsTrue( hasEvents );
        }

        [TestMethod]
        public void PurgeAll_WithoutEvents_ReturnsNoEvents()
        {
            var feed = new AtomFeed( null, null );

            _calendarService.Setup( x => x.Query( It.IsAny<FeedQuery>() ) ).Returns( feed );

            IEnumerable<string> results = _calendarPurge.PurgeAll();

            Assert.IsFalse( results.Any() );
        }

        [TestMethod]
        public void PurgeAll_WithEvents_ReturnsDeletedEventTitles()
        {
            const string entryOneTitle = "1st Entry";
            const string entryTwoTitle = "2nd Entry";
            Assert.AreNotEqual( entryOneTitle, entryTwoTitle );

            var entryOne = new AtomEntry { Title = new AtomTextConstruct( AtomTextConstructElementType.Title, entryOneTitle ) };
            var entryTwo = new AtomEntry { Title = new AtomTextConstruct( AtomTextConstructElementType.Title, entryTwoTitle ) };
            Assert.AreNotEqual( entryOne.Title.Text, entryTwo.Title.Text );

            var feed = new AtomFeed( null, _service.Object );
            feed.Entries.Add( entryOne );
            feed.Entries.Add( entryTwo );

            // setup the only events that should be deleted. 
            // anything else will throw an exception with MockBehavior.Strict.
            _service.Setup( x => x.Delete( entryOne ) );
            _service.Setup( x => x.Delete( entryTwo ) );

            bool isFirstCallback = true;
            int callbackCount = 0;
            _calendarService.Setup( x => x.Query( It.IsAny<FeedQuery>() ) )
                .Callback( () =>
                {
                    if( isFirstCallback )
                    {
                        isFirstCallback = false;
                    }
                    else
                    {
                        feed.Entries.Clear();
                    }
                    callbackCount++;
                } ).Returns( feed );

            List<string> results = _calendarPurge.PurgeAll().ToList();

            Assert.AreEqual( 2, callbackCount );
            Assert.AreEqual( 2, results.Count );
            Assert.IsTrue( results.Contains( entryOneTitle ) );
            Assert.IsTrue( results.Contains( entryTwoTitle ) );
        }
    }
}
