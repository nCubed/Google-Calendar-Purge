using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace nCubed.GooCal.UnitTests
{
    [TestClass]
    public class CalendarConnectionTests
    {
        private Mock<ICalendarCredentials> _creds;

        [TestInitialize]
        public void Initialize()
        {
            _creds = new Mock<ICalendarCredentials>( MockBehavior.Strict );
        }

        [TestMethod]
        public void Class_Implements_ICalendarConnection()
        {
            var type = typeof( CalendarConnection );

            bool isAssignable = typeof( ICalendarConnection ).IsAssignableFrom( type );

            Assert.IsTrue( isAssignable );
        }

        [TestMethod]
        [ExpectedException( typeof( NotImplementedException ) )]
        public void Dispose_NotImplemented()
        {
            using( new CalendarConnection() ) { }
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
