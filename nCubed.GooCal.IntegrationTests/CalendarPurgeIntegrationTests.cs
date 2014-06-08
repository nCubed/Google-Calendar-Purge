using Microsoft.VisualStudio.TestTools.UnitTesting;
using nCubed.GooCal.Common;
using nCubed.GooCal.IntegrationTests._Credentials;

namespace nCubed.GooCal.IntegrationTests
{
    [TestClass]
    public class CalendarPurgeIntegrationTests
    {
        // TODO's:
        // 1. Create helper method to add a new event; will use for HasEvents to ensure an event exists on the calendar.

        private static ICalendarCredentials _creds;

        [ClassInitialize]
        public static void ClassInit( TestContext context )
        {
            _creds = new GooCalCreds();
        }

        [TestMethod]
        public void HasEvents_WithEvents_ReturnsTrue()
        {
            ICalendarPurge purge = CalendarPurgeFactory.Create( _creds );

            bool hasEvents = purge.HasEvents();

            Assert.IsTrue( hasEvents );
        }
    }
}
