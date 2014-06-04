using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nCubed.GooCal.Common;

namespace nCubed.GooCal.UnitTests
{
    [TestClass]
    public class CalendarConnectionTests
    {
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
            using( new CalendarConnection() )
            { }
        }

        [TestMethod]
        public void Class_IsInternal()
        {
            var type = typeof( CalendarConnection );

            bool isInternal = type.IsNotPublic;

            Assert.IsTrue( isInternal );
        }
    }
}
