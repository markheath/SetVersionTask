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
            string pattern = @"^\s*\[assembly: (?:(?:AssemblyFile)|(?:AssemblyInformational)|(?:Assembly))Version\(""(?<Version>[0-9\.\*]+)""\)\]";
            Regex regex = new Regex(pattern);
            var m = regex.Match(input);
            if (m.Success)
            {
                return m.Groups["Version"];
            }
            return null;
        }
    }
}
