using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Utilities;
using Microsoft.Build.Framework;
using System.IO;
using System.Text.RegularExpressions;

namespace SetVersionTask
{
    public class SetVersion : Task
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        public string Version { get; set; }

        public override bool Execute()
        {
            try
            {
                if (this.FileName.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                {
                    UpdateCSharp();
                }
                else if (this.FileName.EndsWith(".nuspec", StringComparison.OrdinalIgnoreCase))
                {
                    UpdateNuSpec();
                }
            }
            catch (Exception e)
            {
                Log.LogError(e.Message);
                return false;
            }
            return true;
        }

        private void UpdateNuSpec()
        {


        }

        private void UpdateCSharp()
        {
            string[] lines = File.ReadAllLines(FileName);
            var outlines = new List<string>();
            foreach (var line in lines)
            {
                VersionString v = null;
                var g = VersionUtils.GetVersionStringFromCSharp(line);
                if (g != null)
                {
                    VersionString.TryParse(g.Value, out v);
                }
                if (v == null)
                {
                    outlines.Add(line);
                }
                else
                {
                    string newVersion = UpdateVersion(v);
                    outlines.Add(line.Substring(0, g.Index) + newVersion + line.Substring(g.Index + g.Length));
                }
            }
        }

        private string UpdateVersion(VersionString v)
        {
            throw new NotImplementedException();
        }

        private void ValidateArguments()
        {

        }

    }
}
