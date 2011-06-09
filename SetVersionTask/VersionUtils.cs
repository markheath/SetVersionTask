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

        public static VersionString GetVersionStringFromCSharp(string input)
        {
            var commentIndex = input.IndexOf("//");
            if (commentIndex != -1)
            {
                input = input.Substring(0, commentIndex);
            }
            // Version\("(?<Version>(?<Major>\d+)\.(?<Minor>\d+)\.(?:(?:(?<Build>\d+)\.(?<Revision>\*|\d+))|(?<Build>\*|\d+)))"\)
            string pattern = @"Version\(""(?<Version>(?<Major>\d+)\.(?<Minor>\d+)\.(?:(?:(?<Build>\d+)\.(?<Revision>\*|\d+))|(?<Build>\*|\d+)))""\)";
            Regex regex = new Regex(pattern);
            var matches = regex.Matches(input);
            if (matches.Count == 1)
            {
                var match = matches[0];
                return new VersionString()
                {
                    Major = match.Groups["Major"].Value,
                    Minor = match.Groups["Minor"].Value,
                    Build = match.Groups["Build"].Value,
                    Revision = match.Groups["Revision"].Value,
                    VersionMatch = match.Groups["Version"],
                };
            }
            return null;
        }
    }



}
