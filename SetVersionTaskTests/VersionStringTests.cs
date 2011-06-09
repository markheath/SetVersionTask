using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SetVersionTask;

namespace SetVersionTaskTests
{
    [TestFixture]
    public class VersionStringTests
    {
        [TestCase("1.2.3.4", "1", "2", "3", "4")]
        [TestCase("1.2.3.*", "1", "2", "3", "*")]
        [TestCase("1.2.3", "1", "2", "3", "")]
        [TestCase("1.2.*", "1", "2", "*", "")]
        [TestCase("11.22.33.44", "11", "22", "33", "44")]
        public void CanParseVersionString(string input, string major, string minor, string build, string revision)
        {
            var versionString = new VersionString(input);
            Assert.AreEqual(major, versionString.Major, "Major");
            Assert.AreEqual(minor, versionString.Minor, "Minor");
            Assert.AreEqual(build, versionString.Build, "Build");
            Assert.AreEqual(revision, versionString.Revision, "Revision");
        }
    }
}
