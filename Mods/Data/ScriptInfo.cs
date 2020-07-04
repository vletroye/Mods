using System;

namespace BeatificaBytes.Synology.Mods
{
    public class ScriptInfo
    {
        private string code;
        private string title;
        private HelpInfo help;

        public ScriptInfo(string code, string title, Uri helpUrl, string helpLabel)
        {
            this.code = code;
            this.title = title;
            if (helpUrl != null)
            {
                this.help = new HelpInfo(helpUrl, helpLabel);
            }
        }
        public ScriptInfo(string code, string title) : this(code, title, null, null)
        { }

        public string Code
        {
            get
            {
                //if (!string.IsNullOrEmpty(code)) code = code.Replace("\r\n", "\n");
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
