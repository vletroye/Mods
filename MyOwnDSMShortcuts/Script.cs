using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatificaBytes.Synology.Mods
{
    public class ScriptDetails
    {
        private string code;
        private string title;
        private HelpInfo help;

        public ScriptDetails(string code, string title, Uri helpUrl, string helpLabel)
        {
            this.code = code;
            this.title = title;
            if (helpUrl != null)
            {
                this.help = new HelpInfo(helpUrl, helpLabel);
            }
        }
        public ScriptDetails(string code, string title) : this(code, title, null, null)
        { }

        public string Code
        {
            get
            {
                if (code != null) code = code.Replace("\r\n", "\n");
                return code;
            }

            set
            {
                code = value;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }

            set
            {
                title = value;
            }
        }

        public HelpInfo Help
        {
            get
            {
                return help;
            }

            set
            {
                help = value;
            }
        }
    }
}
