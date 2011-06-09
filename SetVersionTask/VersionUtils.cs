using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SetVersionTask
{
    public static class VersionUtils
    {

        // AssemblyVersion
        // AssemblyFileVersion
        // AssemblyInformationalVersion

        public static Group GetVersionStringFromCSharp(string input)
        {
            var commentIndex = input.IndexOf("//");
            if (commentIndex != -1)
            {
                input = input.Substring(0, commentIndex);
            }
            // Version\("(?<Version>(?<Major>\d+)\.(?<Minor>\d+)\.(?:(?:(?<Build>\d+)\.(?<Revision>\*|\d+))|(?<Build>\*|\d+)))"\)
            string pattern = @"^\s*\[assembly: (?:(?:AssemblyFile)|(?:AssemblyInformational)|(?:Assembly))Version\(""(?<Version>[0-9\.\*]+)""\)\]";
            Regex regex = new Regex(pattern);
            var m = regex.Match(input);
            if (m.Success)
            {
                return m.Groups["Version"];
            }
            return null;
        }

        public static VersionString ParseVersionString(string input)
        {
            string pattern = @"^(?<Major>\d+)\.(?<Minor>\d+)\.(?:(?:(?<Build>\d+)\.(?<Revision>\*|\d+))|(?<Build>\*|\d+))$";
            Regex regex = new Regex(pattern);
            var matches = regex.Matches(input);
            VersionString version = null;
            if (matches.Count == 1)
            {
                var match = matches[0];
                version = new VersionString()
                {
                    Major = match.Groups["Major"].Value,
                    Minor = match.Groups["Minor"].Value,
                    Build = match.Groups["Build"].Value,
                    Revision = match.Groups["Revision"].Value,
                };
            }
            return version;           
        }
    }
}
