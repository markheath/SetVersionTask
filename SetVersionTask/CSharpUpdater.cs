using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace SetVersionTask
{
    public class CSharpUpdater
    {
        private VersionUpdateRule assemblyVersionUpdateRule;

        public CSharpUpdater(VersionUpdateRule assemblyVersionUpdateRule)
        {
            this.assemblyVersionUpdateRule = assemblyVersionUpdateRule;
        }

        public void UpdateFile(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);
            var outlines = new List<string>();
            foreach (var line in lines)
            {
                outlines.Add(UpdateLine(line));
            }
            File.WriteAllLines(fileName, outlines.ToArray());
        }

        private string UpdateLine(string line)
        {
            VersionString v = null;
            var g = GetVersionString(line, "AssemblyVersion");
            if (g != null)
            {
                VersionString.TryParse(g.Value, out v);
            }
            if (v == null)
            {
                return line;
            }
            else
            {
                string newVersion = assemblyVersionUpdateRule.Update(v);
                return line.Substring(0, g.Index) + newVersion + line.Substring(g.Index + g.Length);
            }
        }

        // currently just works on AssemblyVersion
        // AssemblyFileVersion
        // AssemblyInformationalVersion
        public static Group GetVersionString(string input, string attributeName)
        {
            var commentIndex = input.IndexOf("//");
            if (commentIndex != -1)
            {
                input = input.Substring(0, commentIndex);
            }
            string attributeMatch = String.Format("(?:(?:{0})|(?:{0}Attribute))", attributeName);

            string pattern = @"^\s*\[assembly: " + attributeMatch + @"\(""(?<Version>[0-9\.\*]+)""\)\]";
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
