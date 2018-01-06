using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatificaBytes.Synology.Mods
{
    public class HelpInfo
    {
        private Uri url;
        private string label;

        public HelpInfo(Uri url, string label)
        {
            this.url = url;
            this.label = label;
        }

        public string Label
        {
            get { return label; }
            set { label = value; }
        }

        public Uri Url
        {
            get { return url; }
            set { url = value; }
        }
    }
}
