using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ZTn.Json.Editor.Linq;

namespace ZTn.Json.Editor.Forms
{
    public sealed partial class JsonEditorMainForm : Form
    {

        #region >> Delegates

        private delegate void SetActionStatusDelegate(string text, bool isError);

        private delegate void SetJsonStatusDelegate(string text, bool isError);

        #endregion

        #region >> Fields

        private JTokenRoot JsonEditorItem =>
            jsonTreeView.Nodes.Count != 0 ? new JTokenRoot(((JTokenTreeNode)jsonTreeView.Nodes[0]).JTokenTag) : null;

        private string internalOpenedFileName;

        private System.Timers.Timer jsonValidationTimer;

        #endregion

        #region >> Properties


        public void OpenFile(string path)
        {
            try
            {
                string json = null;
                if (!string.IsNullOrEmpty(path))
                    json = File.ReadAllText(path);
                if (string.IsNullOrEmpty(json))
                {
                    CreateEmptyJson(path);
                }
                OpenJson(path);
            }
            catch
            {
                CreateEmptyJson(path);
                OpenJson(path);
            }
        }

        private void OpenJson(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open))
            {
                SetJsonSourceStream(stream, path);
            }
        }

        private void CreateEmptyJson(string path)
        {
            var jsonEditorItem = new JTokenRoot("[]");
            File.WriteAllText(path, jsonEditorItem.JTokenValue.ToString());
        }

        /// <summary>
        /// Accessor to file name of opened file.
        /// </summary>
        string OpenedFileName
        {
            get { return internalOpenedFileName; }
            set
            {
                internalOpenedFileName = value;
                //Text = (internalOpenedFileName ?? "") + @" - Json Editor by ZTn";
            }
        }

        #endregion

        #region >> Constructor

        public JsonEditorMainForm()
        {
            InitializeComponent();

            jsonTypeComboBox.DataSource = Enum.GetValues(typeof(JTokenType));

            jsonTreeView.AfterCollapse += jsonTreeView_AfterCollapse;
            jsonTreeView.AfterExpand += jsonTreeView_AfterExpand;

            OpenedFileName = null;
            SetActionStatus(@"Empty document.", true);
            SetJsonStatus(@"", false);


            var commandLineArgs = Environment.GetCommandLineArgs();
            if (commandLineArgs.Skip(1).Any())
            {
                OpenedFileName = commandLineArgs[1];
                try
                {
                    using (var stream = new FileStream(commandLineArgs[1], FileMode.Open))
                    {
                        SetJsonSourceStream(stream, commandLineArgs[1]);
                    }
                }
                catch
                {
                    OpenedFileName = null;
                }
            }
        }

        #endregion

        #region >> Form

        /// <inheritdoc />
        /// <remarks>
        /// Optimization aiming to reduce flickering on large documents (successfully).
        /// Source: http://stackoverflow.com/a/89125/1774251
        /// </remarks>
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;    // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        #endregion

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = @"json files (*.json)|*.json|All files (*.*)|*.*",
                FilterIndex = 1,
                RestoreDirectory = true
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                var path = OpenedFileName;
                using (var stream = openFileDialog.OpenFile())
                {
                    SetJsonSourceStream(stream, openFileDialog.FileName);
                }
                OpenedFileName = path;
            }
        }

        //private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    if (OpenedFileName == null)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        using (var stream = new FileStream(OpenedFileName, FileMode.Open))
        //        {
        //            JsonEditorItem.Save(stream);
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show(this, $"An error occured when saving file as \"{OpenedFileName}\".", @"Save As...");

        //        OpenedFileName = null;
        //        SetActionStatus(@"Document NOT saved.", true);

        //        return;
        //    }

        //    SetActionStatus(@"Document successfully saved.", false);
        //}

        //private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var saveFileDialog = new SaveFileDialog
        //    {
        //        Filter = DefaultFileFilters,
        //        FilterIndex = 1,
        //        RestoreDirectory = true
        //    };

        //    if (saveFileDialog.ShowDialog() != DialogResult.OK)
        //    {
        //        return;
        //    }

        //    try
        //    {
        //        OpenedFileName = saveFileDialog.FileName;
        //        using (var stream = saveFileDialog.OpenFile())
        //        {
        //            if (stream.CanWrite)
        //            {
        //                JsonEditorItem.Save(stream);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        MessageBox.Show(this, $"An error occured when saving file as \"{OpenedFileName}\".", @"Save As...");

        //        OpenedFileName = null;
        //        SetActionStatus(@"Document NOT saved.", true);

        //        return;
        //    }

        //    SetActionStatus(@"Document successfully saved.", false);
        //}

        //private void newJsonObjectToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var jsonEditorItem = new JTokenRoot("{}");

        //    jsonTreeView.Nodes.Clear();
        //    jsonTreeView.Nodes.Add(JsonTreeNodeFactory.Create(jsonEditorItem.JTokenValue));
        //    jsonTreeView.Nodes
        //        .Cast<TreeNode>()
        //        .ForEach(n => n.Expand());
        //}

        //private void newJsonArrayToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    var jsonEditorItem = new JTokenRoot("[]");

        //    jsonTreeView.Nodes.Clear();
        //    jsonTreeView.Nodes.Add(JsonTreeNodeFactory.Create(jsonEditorItem.JTokenValue));
        //    jsonTreeView.Nodes
        //        .Cast<TreeNode>()
        //        .ForEach(n => n.Expand());
        //}

        private void aboutJsonEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox().ShowDialog();
        }

        /// <summary>
        /// For the clicked node to be selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jsonTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            jsonTreeView.SelectedNode = e.Node;
        }

        private void jsonTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            var node = e.Node as IJsonTreeNode;
            node?.AfterCollapse();
        }

        private void jsonTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            var node = e.Node as IJsonTreeNode;
            node?.AfterExpand();
        }

        private void jsonValueTextBox_TextChanged(object sender, EventArgs e)
        {
            var node = jsonTreeView.SelectedNode as IJsonTreeNode;
            if (node == null)
            {
                return;
            }

            StartValidationTimer(node);
        }

        private void jsonValueTextBox_Leave(object sender, EventArgs e)
        {
            jsonValueTextBox.TextChanged -= jsonValueTextBox_TextChanged;
        }

        private void jsonValueTextBox_Enter(object sender, EventArgs e)
        {
            jsonValueTextBox.TextChanged += jsonValueTextBox_TextChanged;
        }

        #region >> Methods jsonTreeView_AfterSelect

        /// <summary>
        /// Main event handler dynamically dispatching the handling to specialized methods.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void jsonTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            JsonTreeView_AfterSelectImplementation((dynamic)e.Node, e);
        }

        /// <summary>
        /// Default catcher in case of a node of unattended type.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="e"></param>
        // ReSharper disable once UnusedParameter.Local
        private void JsonTreeView_AfterSelectImplementation(TreeNode node, TreeViewEventArgs e)
        {
            newtonsoftJsonTypeTextBox.Text = "";

            jsonTypeComboBox.Text = $"{JTokenType.Undefined}: {node.GetType().FullName}";

            jsonValueTextBox.ReadOnly = true;
        }

        // ReSharper disable once UnusedParameter.Local
        private void JsonTreeView_AfterSelectImplementation(JTokenTreeNode node, TreeViewEventArgs e)
        {
            newtonsoftJsonTypeTextBox.Text = node.Tag.GetType().Name;

            jsonTypeComboBox.Text = node.JTokenTag.Type.ToString();

            // If jsonValueTextBox is focused then it triggers this event in the update process, so don't update it again ! (risk: infinite loop between events).
            if (!jsonValueTextBox.Focused)
            {
                jsonValueTextBox.Text = node.JTokenTag.ToString();
            }
        }

        // ReSharper disable once UnusedParameter.Local
        private void JsonTreeView_AfterSelectImplementation(JValueTreeNode node, TreeViewEventArgs e)
        {
            newtonsoftJsonTypeTextBox.Text = node.Tag.GetType().Name;

            jsonTypeComboBox.Text = $"{node.JValueTag.Type}";

            switch (node.JValueTag.Type)
            {
                case JTokenType.String:
                    jsonValueTextBox.Text = $@"""{node.JValueTag}""";
                    break;
                default:
                    jsonValueTextBox.Text = $"{node.JValueTag}";
                    break;
            }
        }

        #endregion

        private void SetJsonSourceStream(Stream stream, string fileName)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }

            OpenedFileName = fileName;

            JTokenRoot jsonEditorItem;
            try
            {
                jsonEditorItem = new JTokenRoot(stream);
            }
            catch
            {
                MessageBox.Show(this, $"An error occured when reading \"{OpenedFileName}\"", @"Open...");

                OpenedFileName = null;
                SetActionStatus(@"Document NOT loaded.", true);

                return;
            }

            SetActionStatus(@"Document successfully loaded.", false);

            jsonTreeView.Nodes.Clear();
            jsonTreeView.Nodes.Add(JsonTreeNodeFactory.Create(jsonEditorItem.JTokenValue));
            jsonTreeView.Nodes
                .Cast<TreeNode>()
                .ForEach(n => n.Expand());
        }

        private void SetActionStatus(string text, bool isError)
        {
            if (InvokeRequired)
            {
                Invoke(new SetActionStatusDelegate(SetActionStatus), text, isError);
                return;
            }

            actionStatusLabel.Text = text;
            actionStatusLabel.ForeColor = isError ? Color.OrangeRed : Color.Black;
        }

        private void SetJsonStatus(string text, bool isError)
        {
            if (InvokeRequired)
            {
                Invoke(new SetJsonStatusDelegate(SetActionStatus), text, isError);
                return;
            }

            jsonStatusLabel.Text = text;
            jsonStatusLabel.ForeColor = isError ? Color.OrangeRed : Color.Black;
        }

        private void StartValidationTimer(IJsonTreeNode node)
        {
            jsonValidationTimer?.Stop();

            jsonValidationTimer = new System.Timers.Timer(250);

            jsonValidationTimer.Elapsed += (o, args) =>
            {
                jsonValidationTimer.Stop();

                jsonTreeView.Invoke(new Action<IJsonTreeNode>(JsonValidationTimerHandler), node);
            };

            jsonValidationTimer.Start();
        }

        private void JsonValidationTimerHandler(IJsonTreeNode node)
        {
            jsonTreeView.BeginUpdate();

            try
            {
                jsonTreeView.SelectedNode = node.AfterJsonTextChange(jsonValueTextBox.Text);

                SetJsonStatus("Json format validated.", false);
            }
            catch (JsonReaderException exception)
            {
                SetJsonStatus(
                    $"INVALID Json format at (line {exception.LineNumber}, position {exception.LinePosition})",
                    true);
            }
            catch
            {
                SetJsonStatus("INVALID Json format", true);
            }

            jsonTreeView.EndUpdate();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (OpenedFileName == null)
            {
                return;
            }

            try
            {
                using (var stream = new FileStream(OpenedFileName, FileMode.Open))
                {
                    JsonEditorItem.Save(stream);
                }
            }
            catch
            {
                MessageBox.Show(this, $"An error occured when saving file as \"{OpenedFileName}\".", @"Save As...");

                OpenedFileName = null;
                SetActionStatus(@"Wizard NOT saved.", true);

                return;
            }

            SetActionStatus(@"Wizard successfully saved.", false);

            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
