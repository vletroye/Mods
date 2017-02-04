using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTn.Json.Editor.Forms
{
    public class ComboItem
    {
        public string value;
        public string display;

        public  ComboItem(string value, string display)
        {
            this.value = value;
            this.display = display;
        }
                public override string ToString()
        {
            return "(" + value + "): " + display;
        }
    }
}
