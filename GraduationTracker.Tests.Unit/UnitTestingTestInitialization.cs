using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraduationTracker.Tests.Unit
{
    [TestClass]
    public class UnitTestingTestInitialization
    {
        [AssemblyInitialize()]
        public static void AssemblyInitialize(TestContext tc)
        {
            //TODO: Initialise for all tests within assembly
        }

        [AssemblyCleanup()]
        public static void AssemblyCleanup()
        {
            //TODO: Clean up after all tests within assembly
        }
    }
}
