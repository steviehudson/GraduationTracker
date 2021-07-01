using System;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace GraduationTracker.Tests.Unit
{
    public class TestBase
    {
        public TestContext TestContext { get; set; }

        protected void WriteDescription(Type typ)
        {
            string testName = TestContext.TestName;

            MemberInfo method = typ.GetMethod(testName);
            if (method != null) TestContext.WriteLine("Test Description: " + method);
            }
        }
}
