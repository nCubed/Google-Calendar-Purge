using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace nCubed.GooCal.UnitTests.TestUtils
{
    internal static class AssertClass
    {
        public enum Visbility
        {
            Public,
            Internal,
        }

        public static void IsStatic( Type type, Visbility visbility )
        {
            bool isStatic = type.IsAbstract && type.IsSealed;

            Assert.IsTrue( isStatic );

            ConstructorInfo[] ctors = type.GetConstructors( BindingFlags.Public );

            Assert.AreEqual( 0, ctors.Length );

            AssertVisibility( type, visbility );
        }

        public static void IsPublic<TClass>() where TClass : class
        {
            var type = typeof( TClass );

            AssertIsPublic( type );
        }

        public static void IsInternal<TClass>() where TClass : class
        {
            var type = typeof( TClass );

            AssertIsInternal( type );
        }

        public static void IsAbstract<TClass>() where TClass : class
        {
            var type = typeof( TClass );

            AssertIsAbstract( type );
        }

        public static void IsSealed<TClass>() where TClass : class
        {
            var type = typeof( TClass );

            bool isSealed = type.IsSealed;

            Assert.IsTrue( isSealed );
        }

        public static void InheritsBaseClass<TClass, TBaseClass>()
            where TClass : class
            where TBaseClass : class
        {
            Type baseType = typeof( TBaseClass );

            AssertIsAssignableFrom<TClass>( baseType );
        }

        public static void InheritsAbstractClass<TClass, TAbstractClass>()
            where TClass : class
            where TAbstractClass : class
        {
            Type abstractType = typeof( TAbstractClass );

            AssertIsAbstract( abstractType );

            AssertIsAssignableFrom<TClass>( abstractType );
        }

        public static void ImplementsInterface<TClass, TInterface>()
            where TClass : class
            where TInterface : class
        {
            Type interfaceType = typeof( TInterface );

            Assert.IsTrue( interfaceType.IsInterface );

            AssertIsAssignableFrom<TClass>( interfaceType );
        }

        private static void AssertIsAssignableFrom<TClass>( Type assignableType ) where TClass : class
        {
            Type classType = typeof( TClass );

            bool isAssignableFrom = assignableType.IsAssignableFrom( classType );

            Assert.IsTrue( isAssignableFrom );
        }

        private static void AssertVisibility( Type type, Visbility visbility )
        {
            switch( visbility )
            {
                case Visbility.Public:
                    AssertIsPublic( type );
                    break;

                case Visbility.Internal:
                    AssertIsInternal( type );
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private static void AssertIsPublic( Type type )
        {
            bool isPublic = type.IsPublic;

            Assert.IsTrue( isPublic );
        }

        private static void AssertIsInternal( Type type )
        {
            bool isInternal = type.IsNotPublic;

            Assert.IsTrue( isInternal );
        }

        private static void AssertIsAbstract( Type type )
        {
            bool isAbstract = type.IsAbstract;

            Assert.IsTrue( isAbstract );
        }
    }
}