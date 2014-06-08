using System.IO;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using nCubed.GooCal.Common;

namespace nCubed.GooCal.IntegrationTests._Credentials
{
    /// <summary>
    /// Integration testing implementation of <see cref="ICalendarCredentials"/>
    /// to allow for storage of calendar log in credentials on the developer's
    /// local machine without having to expose the credentials in source control.
    /// </summary>
    internal class GooCalCreds : ICalendarCredentials
    {
        public const string FileName = "_Credentials\\GooCalCreds.xml";

        public string CalendarUrl { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }

        public GooCalCreds()
        {
            Hydrate();
        }

        private void Hydrate()
        {
            string location = Assembly.GetExecutingAssembly().Location;
            string dir = Path.GetDirectoryName( location );

            Assert.IsNotNull( dir );

            string filePath = Path.Combine( dir, FileName );

            var doc = XDocument.Load( filePath );

            // ReSharper disable PossibleNullReferenceException
            CalendarUrl = doc.Root.Element( "CalendarUrl" ).Value;
            UserName = doc.Root.Element( "UserName" ).Value;
            Password = doc.Root.Element( "Password" ).Value;
            // ReSharper restore PossibleNullReferenceException
        }

        public void OverrideCalendarUrl( string url )
        {
            CalendarUrl = url;
        }
    }
}