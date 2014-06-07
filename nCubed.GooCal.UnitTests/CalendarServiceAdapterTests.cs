using Google.GData.Calendar;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nCubed.GooCal.Common;
using nCubed.GooCal.UnitTests.TestUtils;

namespace nCubed.GooCal.UnitTests
{
    [TestClass]
    public class CalendarServiceAdapterTests
    {
        [TestMethod]
        public void Class_Implements_IGoogleCalendarService()
        {
            AssertClass.ImplementsInterface<CalendarServiceAdapter, IGoogleCalendarService>();
        }

        [TestMethod]
        public void Class_Inherits_CalendarService()
        {
            AssertClass.InheritsBaseClass<CalendarServiceAdapter, CalendarService>();
        }

        [TestMethod]
        public void Class_IsInternal()
        {
            AssertClass.IsInternal<CalendarServiceAdapter>();
        }

        [TestMethod]
        public void Class_IsSealed()
        {
            AssertClass.IsSealed<CalendarServiceAdapter>();
        }
    }
}