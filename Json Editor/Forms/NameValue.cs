using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZTn.Json.Editor.Forms
{
    public class NameValue
    {
        public string value;
        public string name;

        public NameValue(string value, string name)
        {
            this.value = value;
            this.name = name;
        }
        public override string ToString()
        {
            return name + ": " + value;
        }
    }
}
