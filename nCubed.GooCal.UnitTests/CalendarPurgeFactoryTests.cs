using Microsoft.VisualStudio.TestTools.UnitTesting;
using nCubed.GooCal.Common;
using nCubed.GooCal.UnitTests.TestUtils;

namespace nCubed.GooCal.UnitTests
{
    [TestClass]
    public class CalendarPurgeFactoryTests
    {
        [TestMethod]
        public void Class_IsStatic()
        {
            AssertClass.IsStatic( typeof( CalendarPurgeFactory ), AssertClass.Visbility.Public );
        }
    }
}