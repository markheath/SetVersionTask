using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SetVersionTask
{
    public static class VersionUtils
    {
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

    public class VersionString
    {
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Build { get; set; }
        public string Revision { get; set; }
        public Group VersionMatch { get; set; }
    }

    public class VersionUpdateRule
    {
        private string[] partRules;
        public VersionUpdateRule(string rule)
        {
            this.partRules = rule.Split('.');
            if (partRules.Length < 2 || partRules.Length > 4)
                throw new ArgumentException("Expecting 2-4 version parts");
            foreach (var partRule in partRules)
            {
                if (partRule == "+" || partRule == "=")
                {
                    // OK, valid rule
                }
                else
                {
                    // will throw an exception if not an int
                    int.Parse(partRule);
                }
            }
        }

        public string Update(VersionString version)
        {
            List<string> inParts = new List<string>() { version.Major, version.Minor, version.Build, version.Revision };
            List<string> outParts = new List<string>();
            for (int index = 0; index < partRules.Length; index++)
            {
                var rule = partRules[index];
                var inPart = inParts[index];
                if (rule == "=")
                {
                    if (inPart.Length > 0)
                    {
                        outParts.Add(inParts[index]);
                    }
                }
                else if (rule == "+")
                {
                    if (inPart.Length == 0)
                    {
                        throw new ArgumentException("Can't increment missing value");
                    }
                    int inNumber = int.Parse(inPart);
                    inNumber++;
                    outParts.Add(inNumber.ToString());
                }
                else
                {
                    // must be a numeric literal
                    outParts.Add(partRules[index]);
                }
            }
            return String.Join(",", outParts.ToArray());
        }
    }
}
