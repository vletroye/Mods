using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BeatificaBytes.Synology.Mods
{
    public class MessageEventArgs : EventArgs
    {
        public string Message { get; set; }

        public MessageEventArgs(string message)
        {
            this.Message = message;
        }
    }

    internal static class HelperNew
    {
        internal static event EventHandler<MessageEventArgs> Notify;

        //[MethodImpl(MethodImplOptions.NoInlining)]
        //static public MethodBase GetCurrentMethod()
        //{
        //    var st = new StackTrace();
        //    var sf = st.GetFrame(1);

        //    return sf.GetMethod();
        //}
        //[MethodImpl(MethodImplOptions.NoInlining)]
        //static public MethodBase GetPreviousMethod()
        //{
        //    var st = new StackTrace();
        //    var sf = st.GetFrame(2);

        //    return sf.GetMethod();
        //}

        internal static void PublishWarning(string message)
        {
            Notify(null, new MessageEventArgs(message));
        }
    }
}
