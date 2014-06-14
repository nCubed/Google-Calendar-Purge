using System;
using System.Collections.Generic;
using System.Linq;
using Google.GData.Calendar;
using Google.GData.Client;
using Google.GData.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nCubed.GooCal.Common;
using nCubed.GooCal.IntegrationTests._Credentials;

namespace nCubed.GooCal.IntegrationTests
{
    /// <summary>
    /// NOTE: Be sure to disable this class from automated test runners such as NCrunch
    /// as there is a lot of set up and tear down that is time consuming. There are related
    /// Unit Tests covering the <see cref="CalendarPurge"/> class that can be used for
    /// developing with an automated test runner.
    /// </summary>
    [TestClass]
    public class CalendarPurgeIntegrationTests
    {
        private static ICalendarCredentials _creds;
        private static CalendarPurge _calendarPurge;
        private static CalendarService _service;

        [ClassInitialize]
        public static void ClassInit( TestContext context )
        {
            _creds = GooCalCreds.CreateAndValidate();
            _calendarPurge = (CalendarPurge)CalendarPurgeFactory.Create( _creds );
            _service = _calendarPurge.GoogleCalendarService;

            DeleteAllEvents();
        }

        /// <summary>
        /// Test has been deprecated with the introduction of the DeleteAllEvents
        /// method that is used both pre and post test initialization.
        /// </summary>
        [Ignore]
        [TestMethod]
        public void HasEvents_WithOutEvents_ReturnsFalse()
        {
            bool hasEvents = _calendarPurge.HasEvents();

            Assert.IsFalse( hasEvents );
        }

        /// <summary>
        /// Test has been deprecated with the introduction of the PurgeAll_WithEvents_ReturnsDeletedEventTitles
        /// method that uses the CalendarPurge.HasEvents method. Additionally, when running tests in
        /// parallel, there were random conflicts between the PurgeAll test conflicting with the HasEvents
        /// test where PurgeAll purged as HasEvents was running.
        /// </summary>
        [Ignore]
        [TestMethod]
        public void HasEvents_WithEvents_ReturnsTrue()
        {
            EventEntry entry = CreateEventEntry( 1 );
            _service.Insert( _creds.CalendarUrl, entry );

            bool hasevents = _calendarPurge.HasEvents();

            Assert.IsTrue( hasevents );
        }

        [TestMethod]
        public void PurgeAll_WithEvents_ReturnsDeletedEventTitles()
        {
            // by default, the Google Calendar Service returns 25 entries at a time. This should
            // ensure the PurgeAll method has to run more than one time internally.
            const int entryCount = 30;
            var entries = new List<EventEntry>();

            for( int i = 1; i <= entryCount; i++ )
            {
                EventEntry entry = CreateEventEntry( i );
                _service.Insert( _creds.CalendarUrl, entry );
                entries.Add( entry );
            }

            bool hasEvents = _calendarPurge.HasEvents();
            Assert.IsTrue( hasEvents );

            var deletedEvents = (List<string>)_calendarPurge.PurgeAll();

            foreach( EventEntry e in entries )
            {
                Assert.IsTrue( deletedEvents.Contains( e.Title.Text ), string.Format( "'{0}' was expected to be in the deletedEvents, but was not found.", e.Title.Text ) );
            }

            Assert.AreEqual( entryCount, deletedEvents.Count );
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            DeleteAllEvents();
        }

        private static void DeleteAllEvents()
        {
            // we could write a delete all method here, but it'd be identical to the PurgeAll method.
            _calendarPurge.PurgeAll();

            // now see if everything really was purged
            Assert.IsFalse( _calendarPurge.HasEvents() );
            Assert.IsFalse( _calendarPurge.PurgeAll().Any() );
        }

        private EventEntry CreateEventEntry( int entryNumber )
        {
            var entry = new EventEntry
            {
                Content = { Content = "This is the default text entry." },
                Published = new DateTime( 2001, 1, 1 ),
                Title = { Text = "This is a entry number: " + entryNumber },
                Updated = DateTime.Now,
            };

            var author = new AtomPerson( AtomPersonType.Author )
            {
                Name = "nCubed",
                Email = "nCubed@example.com",
            };
            entry.Authors.Add( author );

            var cat = new AtomCategory
            {
                Label = "Default",
                Term = "Default Term",
            };
            entry.Categories.Add( cat );

            var newTime = new When
            {
                StartTime = DateTime.Today,
                EndTime = DateTime.Today.AddDays( 1 )
            };
            entry.Times.Add( newTime );

            return entry;
        }
    }
}
