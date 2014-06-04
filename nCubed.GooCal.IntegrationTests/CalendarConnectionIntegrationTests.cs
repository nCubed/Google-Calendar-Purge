using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using nCubed.GooCal.Common;

namespace nCubed.GooCal.IntegrationTests
{
    [Ignore]
    [TestClass]
    public class CalendarConnectionIntegrationTests
    {
        private Mock<ICalendarCredentials> _creds;

        [TestInitialize]
        public void Initialize()
        {
            _creds = new Mock<ICalendarCredentials>( MockBehavior.Strict );
        }

        [TestMethod]
        [ExpectedException( typeof( Google.GData.Client.InvalidCredentialsException ) )]
        public void Connect_InvalidCredentials_ThrowsException()
        {
            _creds.Setup( x => x.UserName ).Returns( "InvalidName" );
            _creds.Setup( x => x.Password ).Returns( "InvalidPassword" );

            var c = new CalendarConnection();

            c.Connect( _creds.Object );
        }
    }
}
