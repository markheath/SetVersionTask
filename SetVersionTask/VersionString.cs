using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SetVersionTask
{

    public class VersionString
    {
        public string Major { get; set; }
        public string Minor { get; set; }
        public string Build { get; set; }
        public string Revision { get; set; }
        public Group VersionMatch { get; set; }
    }
}
