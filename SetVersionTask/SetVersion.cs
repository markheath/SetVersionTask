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
                    var updater = new CSharpUpdater(new VersionUpdateRule(Version));
                    updater.UpdateFile(FileName);
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

        private void ValidateArguments()
        {

        }

    }
}
