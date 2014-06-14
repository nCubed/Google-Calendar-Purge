using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nCubed.GooCal.Common;
using nCubed.GooCal.IntegrationTests._Credentials;

namespace nCubed.GooCal.IntegrationTests
{
    // TODO: Create test for validating the user supplied calendar URL when creating the ICalendarCredentials.
    //       The Google API will throw an System.ArgumentException: The query argument MUST contain a valid Uri Parameter name: feedQuery

    [TestClass]
    public class CalendarPurgeFactoryIntegrationTests
    {
        private static ICalendarCredentials _creds;

        [ClassInitialize]
        public static void ClassInit( TestContext context )
        {
            _creds = GooCalCreds.CreateAndValidate();
        }

        [TestMethod]
        [ExpectedException( typeof( Google.GData.Client.InvalidCredentialsException ) )]
        public void Create_InvalidCredentials_ThrowsException()
        {
            var creds = new Mock<ICalendarCredentials>( MockBehavior.Strict );
            creds.Setup( x => x.UserName ).Returns( "InvalidName" );
            creds.Setup( x => x.Password ).Returns( "InvalidPassword" );

            // ReSharper disable once UnusedVariable
            ICalendarPurge purge = CalendarPurgeFactory.Create( creds.Object );
        }

        [TestMethod]
        [ExpectedException( typeof( Google.GData.Client.AuthenticationException ) )]
        public void Create_InvalidCalendarUrl_ThrowsException()
        {
            var creds = new GooCalCreds();
            creds.OverrideCalendarUrl( _creds.CalendarUrl.Replace( "/private/", "/public/" ) );

            // ReSharper disable once UnusedVariable
            ICalendarPurge purge = CalendarPurgeFactory.Create( creds );
        }

        [TestMethod]
        public void Create_ValidCredentials_SuccessfulCreation()
        {
            // ReSharper disable once UnusedVariable
            ICalendarPurge purge = CalendarPurgeFactory.Create( _creds );
        }
    }
}
