using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nCubed.GooCal.Common;
using nCubed.GooCal.IntegrationTests._Credentials;

namespace nCubed.GooCal.IntegrationTests
{
    [TestClass]
    public class CalendarPurgeFactoryIntegrationTests
    {
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
        public void Create_ValidCredentials_SuccessfulCreation()
        {
            ICalendarCredentials creds = new GooCalCreds();

            // ReSharper disable once UnusedVariable
            ICalendarPurge purge = CalendarPurgeFactory.Create( creds );
        }
    }
}
