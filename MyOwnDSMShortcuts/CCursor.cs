using System;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    internal class CCursor : IDisposable
    {
        private Cursor saved = null;

        public CCursor(Cursor newCursor)
        {
            saved = Cursor.Current;

            Cursor.Current = newCursor;
        }

        public void Dispose()
        {
            Cursor.Current = saved;
        }
    }

    internal class CWaitCursor : CCursor
    {
        public CWaitCursor() : base(Cursors.WaitCursor) { }
    }
}