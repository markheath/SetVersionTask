using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MsBuildSetVersion;

namespace MsBuildSetVersionTests
{
    [TestFixture]
    public class VersionUtilsTests
    {
        [TestCase("[assembly: AssemblyVersion(\"1.2.3.4\")]", "1", "2", "3", "4", "1.2.3.4")]
        [TestCase("[assembly: AssemblyVersion(\"1.2.3.*\")]", "1", "2", "3", "*", "1.2.3.*")]
        [TestCase("[assembly: AssemblyVersion(\"1.2.3\")]", "1", "2", "3", "", "1.2.3")]
        [TestCase("[assembly: AssemblyVersion(\"1.2.*\")]", "1", "2", "*", "", "1.2.*")]
        [TestCase("[assembly: AssemblyVersion(\"11.22.33.44\")]", "11", "22", "33", "44", "11.22.33.44")]
        public void ShouldMatch(string input, string major, string minor, string build, string revision, string version)
        {
            var versionString = VersionUtils.GetVersionStringFromCSharp(input);
            Assert.NotNull(versionString, "Expected a match");
            Assert.AreEqual(major, versionString.Major, "Major");
            Assert.AreEqual(minor, versionString.Minor, "Minor");
            Assert.AreEqual(build, versionString.Build, "Build");
            Assert.AreEqual(revision, versionString.Revision, "Revision");
            Assert.AreEqual(version, versionString.VersionMatch.Value, "Version");
        }

    }
}
