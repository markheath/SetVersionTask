using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SetVersionTask;

namespace SetVersionTaskTests
{
    [TestFixture]
    public class VersionUtilsTests
    {
        [TestCase("[assembly: AssemblyVersion(\"1.2.3.4\")]", "1.2.3.4")]
        [TestCase("[assembly: AssemblyFileVersion(\"1.2.3.*\")]", "1.2.3.*")]
        [TestCase("[assembly: AssemblyInformationalVersion(\"1.2.3\")]", "1.2.3")]
        [TestCase("   [assembly: AssemblyVersion(\"1.2.*\")]", "1.2.*")]
        [TestCase("[assembly: AssemblyVersion(\"11.22.33.44\")]", "11.22.33.44")]
        public void CanFindAssembleVersionAttribute(string input, string version)
        {
            var versionString = VersionUtils.GetVersionStringFromCSharp(input);
            Assert.NotNull(versionString, "Expected a match");
            Assert.AreEqual(version, versionString.Value, "Version");
        }

        [TestCase("Version(\"11.22.33.44\")")]
        [TestCase("//[assembly: AssemblyVersion(\"1.2.3.4\")]")]
        [TestCase("[assembly: AssemblyCheeseVersion(\"11.22.33.44\")]")]
        [TestCase("[assembly: Version(\"11.22.33.44\")]")]
        [TestCase("[blah: AssemblyVersion(\"11.22.33.44\")]")]
        public void ShouldNotMatch(string input)
        {
            var versionString = VersionUtils.GetVersionStringFromCSharp(input);
            Assert.Null(versionString, "Expected no match");
        }
    }
}
