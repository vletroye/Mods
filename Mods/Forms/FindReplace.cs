using System;
using System.Windows.Forms;
using ScintillaNET;

namespace ScintillaFindReplaceControl
{
    public partial class FindReplace : Form
    {
        #region Class Def

        // Pointer to the Scintilla control to apply actions to
        private Scintilla _scintilla;

        // Search Flags for Find and for Replace
        private SearchFlags _searchFlags;

        /// <summary>
        ///     Creates an instance of the FindReplace dialogs
        /// </summary>
        public FindReplace()
        {
            InitializeComponent();
            _searchFlags = SearchFlags.None;
        }

        /// <summary>
        ///     Inits the 'Find' dialog for the specified Scintilla control
        /// </summary>
        /// <param name="scintilla"></param>
        public void SetFind(Scintilla scintilla, string text)
        {
            tabControlFindReplace.SelectTab(0);
            _scintilla = scintilla;
            textBoxFind.Text = text;
            textBoxFindRep.Text = text;
        }

        /// <summary>
        ///     /// Inits the 'Replace' dialog for the specified Scintilla control
        /// </summary>
        /// <param name="scintilla"></param>
        public void SetReplace(Scintilla scintilla, string text)
        {
            tabControlFindReplace.SelectTab(1);
            _scintilla = scintilla;
            textBoxFind.Text = text;
            textBoxFindRep.Text = text;

        }

        internal void SearchNext(Scintilla searchArea)
        {
            if (_scintilla == searchArea && !string.IsNullOrEmpty(textBoxFind.Text))
            {
                DoSearch();
            }
        }
        #endregion

        #region Control Handlers

        /// <summary>
        ///     Prevents the form from being destroyed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindReplace_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide();
            e.Cancel = true;
        }

        /// <summary>
        ///     Handles the 'Find Next' button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFind_Click(object sender, EventArgs e)
        {
            DoSearch();
        }

        private void DoSearch()
        {
            SetSearchFlags();
            var text = textBoxFind.Text;

            if (checkBoxBackward.Checked)
                FindPrevious(text, true);
            else
                FindNext(text, true);
        }

        /// <summary>
        ///     Handles the Replace Next button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplace_Click(object sender, EventArgs e)
        {
            SetSearchFlags();
            Replace(textBoxFindRep.Text, textBoxReplace.Text, true);
        }

        /// <summary>
        ///     Handles with Replace All button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonReplaceAll_Click(object sender, EventArgs e)
        {
            // Set the replace Search Flags
            SetSearchFlags();
            // Record current position and anchor
            var currentPos = _scintilla.CurrentPosition;
            var currentAnchorPos = _scintilla.AnchorPosition;
            // Start search at the beginning of the control text
            _scintilla.CurrentPosition = 0;
            _scintilla.AnchorPosition = 0;
            // Call Replace All
            ReplaceAll(textBoxFindRep.Text, textBoxReplace.Text);
            // Restore the position and anchor
            _scintilla.CurrentPosition = currentPos;
            _scintilla.AnchorPosition = currentAnchorPos;
        }

        #endregion

        #region Text find and replace

        /// <summary>
        ///     Finds the next occurnce of the text in the active Scintilla control
        /// </summary>
        /// <param name="text"></param>
        /// <param name="searchFlags"></param>
        /// <returns></returns>
        public int FindNext(string text, bool loop)
        {
            _scintilla.SearchFlags = _searchFlags;
            _scintilla.TargetStart = Math.Max(_scintilla.CurrentPosition, _scintilla.AnchorPosition);
            _scintilla.TargetEnd = _scintilla.TextLength;

            var pos = _scintilla.SearchInTarget(text);
            if (pos >= 0)
                _scintilla.SetSel(_scintilla.TargetStart, _scintilla.TargetEnd);
            else if (_scintilla.TargetStart > 0 && loop)
            {
                _scintilla.TargetStart = 0;
                _scintilla.TargetEnd = _scintilla.TextLength;

                pos = _scintilla.SearchInTarget(text);
                if (pos >= 0)
                    _scintilla.SetSel(_scintilla.TargetStart, _scintilla.TargetEnd);
            }

            return pos;
        }

        /// <summary>
        ///     Finds the previous occurence of the text in the active Scintilla control
        /// </summary>
        /// <param name="text"></param>
        /// <param name="searchFlags"></param>
        /// <returns></returns>
        public int FindPrevious(string text, bool loop)
        {
            _scintilla.SearchFlags = _searchFlags;
            _scintilla.TargetStart = Math.Min(_scintilla.CurrentPosition, _scintilla.AnchorPosition);
            _scintilla.TargetEnd = 0;

            var pos = _scintilla.SearchInTarget(text);
            if (pos >= 0)
                _scintilla.SetSel(_scintilla.TargetStart, _scintilla.TargetEnd);
            else if (_scintilla.TargetStart < _scintilla.TextLength && loop)
            {
                _scintilla.TargetStart = _scintilla.TextLength;
                _scintilla.TargetEnd = 0;

                pos = _scintilla.SearchInTarget(text);
                if (pos >= 0)
                    _scintilla.SetSel(_scintilla.TargetStart, _scintilla.TargetEnd);
            }

            return pos;
        }

        /// <summary>
        ///     Replaces the text with text specified and Find the next match
        /// </summary>
        /// <param name="findText"></param>
        /// <param name="replaceText"></param>
        /// <returns></returns>
        private int Replace(string findText, string replaceText, bool loop)
        {
            if (_scintilla.SelectedText == findText)
                _scintilla.ReplaceSelection(replaceText);

            int pos = 0;
            if (checkBoxBackward.Checked)
                pos = FindPrevious(findText, loop);
            else
                pos = FindNext(findText, loop);

            return pos;
        }

        /// <summary>
        ///     Replaces all occurences of text with text specified
        /// </summary>
        /// <param name="findText"></param>
        /// <param name="replaceText"></param>
        private void ReplaceAll(string findText, string replaceText)
        {
            var pos = 0;
            // Iterate until text is no longer found
            while (pos >= 0)
            {
                pos = Replace(findText, replaceText, false);
            }
        }

        #endregion

        #region UI helpers

        /// <summary>
        ///     Set the Find search flags based on checked options
        /// </summary>
        private void SetSearchFlags()
        {
            if (checkBoxMatchCase.Checked)
                _searchFlags |= SearchFlags.MatchCase;
            if (checkBoxRegEx.Checked)
                _searchFlags |= SearchFlags.Regex;
            if (checkBoxWholeWord.Checked)
                _searchFlags |= SearchFlags.WholeWord;
            if (checkBoxWordStart.Checked)
                _searchFlags |= SearchFlags.WordStart;
        }
        #endregion

        private void tabControlFindReplace_TabIndexChanged(object sender, EventArgs e)
        {
            if (tabControlFindReplace.SelectedIndex == 0)
            { this.AcceptButton = buttonFindNext; textBoxFind.Focus(); }
            else
            { this.AcceptButton = buttonFindNextToReplace; textBoxFindRep.Focus(); }
        }

        private void textBoxFind_TextChanged(object sender, EventArgs e)
        {
            textBoxFindRep.Text = textBoxFind.Text;
            _scintilla.SelectionEnd = _scintilla.SelectionStart;
            DoSearch();
        }

        private void textBoxFindRep_TextChanged(object sender, EventArgs e)
        {
            textBoxFind.Text = textBoxFindRep.Text;
            _scintilla.SelectionEnd = _scintilla.SelectionStart;
            DoSearch();
        }

        private void FindReplace_Activated(object sender, EventArgs e)
        {
            if (tabControlFindReplace.SelectedIndex == 0)
            {
                textBoxFind.Focus();
            }
            else
            {
                textBoxFindRep.Focus();
            }

        }
    }
}