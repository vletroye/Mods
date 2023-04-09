using System;
using System.Drawing;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    internal class CCursor : IDisposable
    {
        private Cursor savedCursor = null;
        private Color savedColor;
        private Label display;

        public CCursor(Cursor newCursor, Label display, string message)
        {
            savedCursor = Cursor.Current;

            this.display = display;
            if (display != null && ! string.IsNullOrEmpty(message))
            {
                savedColor = display.ForeColor;
                display.Text = message;
                display.ForeColor = Color.Red;
            }
            Cursor.Current = newCursor;
            Application.DoEvents();
        }

        public void Dispose()
        {
            if (display != null)
            {
                display.Text = "";
                display.ForeColor = savedColor;
            }
            Cursor.Current = savedCursor;
        }
    }

    internal class CWaitCursor : CCursor
    {
        public CWaitCursor() : base(Cursors.WaitCursor, null, null) { }

        public CWaitCursor(string message) : base(Cursors.WaitCursor, null, message) { }

        public CWaitCursor(Label display, string message) : base(Cursors.WaitCursor, display, message) { }

    }
}