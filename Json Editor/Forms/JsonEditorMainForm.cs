using System.Drawing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Windows.Forms;
using ZTn.Json.Editor.Linq;
using System.Diagnostics;
using System.Reflection;
using ScintillaNET;

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

        private bool validJSon = true;

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
            var info = new FileInfo(path);
            using (var stream = new FileStream(path, FileMode.Open))
            {
                SetJsonSourceStream(stream, path);
            }

            ExpandWizardNodes(jsonTreeView.Nodes[0]);
        }

        private void ExpandWizardNodes(TreeNode node)
        {
            if (node != null)
            {
                if ((node.Level == 0 || node.Level == 3 || node.Level == 6) && node.Text.StartsWith("[Array]"))
                {
                    node.NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold | FontStyle.Underline);
                    node.Expand();
                    node.EnsureVisible();
                }

                foreach (TreeNode subnode in node.Nodes)
                {
                    ExpandWizardNodes(subnode);
                }
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

            InitJsonEditor(jsonValueEditor);

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
                    OpenJson(OpenedFileName);
                }
                catch
                {
                    OpenedFileName = null;
                }
            }

            webBrowserPreview.DocumentText = @" 
<!DOCTYPE html PUBLIC ' -//W3C//DTD HTML 4.01//EN' 'http://www.w3.org/TR/html4/strict.dtd'>
<style type='text/css'>
    #container {     
        display:table;
        border-collapse:collapse;
        height:200px;
        width:100%;
        border:1px solid #000; 
    }
    #layout {
        display:table-row;    
    }
    #content {     
    display:table-cell;   
        text-align:center;  
        vertical-align:middle;     
    }            
</style>      
<div id='container'>
    <div id='layout'>
        <div id ='content'>
            Click on 'Preview'
        </div>      
    </div>    
</div>";
        }

        private void InitJsonEditor(Scintilla textArea)
        {
            //textArea.WrapMode = WrapMode.None;
            //textArea.IndentationGuides = IndentView.LookBoth;
            // Configure the default style
            textArea.StyleResetDefault();
            textArea.Styles[Style.Default].Font = "Consolas";
            textArea.Styles[Style.Default].Size = 10;
            textArea.StyleClearAll();

            textArea.Lexer = ScintillaNET.Lexer.Json;

            textArea.Styles[ScintillaNET.Style.Json.Keyword].ForeColor = Color.DarkViolet;
            textArea.Styles[ScintillaNET.Style.Json.Number].ForeColor = Color.Plum;
            textArea.Styles[ScintillaNET.Style.Json.PropertyName].ForeColor = Color.DarkBlue;
            textArea.Styles[ScintillaNET.Style.Json.String].ForeColor = Color.Red;
            textArea.Styles[ScintillaNET.Style.Json.StringEol].ForeColor = Color.DarkOrange;
            textArea.Styles[ScintillaNET.Style.Json.Uri].ForeColor = Color.MediumBlue;
            textArea.Styles[ScintillaNET.Style.Json.Operator].ForeColor = Color.Gray;
            textArea.Styles[ScintillaNET.Style.Json.Default].ForeColor = Color.DarkViolet;
            textArea.Styles[ScintillaNET.Style.Json.LdKeyword].ForeColor = Color.DarkViolet;

            textArea.SetKeywords(0, "step_title items type desc subitems activeate deactivate key defaultVaule emptyText validator disabled height hidden invalidText preventMark width allowBlank minLength maxLength vtype regex fn blankText grow growMax growMin htmlEncode maxLengthText minLengthText api_store autoSelect displayField editable forceSelection handleHeight listAlign listEmptyText listWidth maxHeight minChars minHeight minListWidth mode pageSize queryDelay resizable selectOnFocus store title typeAhead typeAheadDelay valueField baseParams data fields idProperty root xtype");
            textArea.SetKeywords(1, "false null true");
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
                OpenJson(openFileDialog.FileName);
                OpenedFileName = path;
            }
        }

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

        private void jsonValueEditor_TextChanged(object sender, EventArgs e)
        {
            var node = jsonTreeView.SelectedNode as IJsonTreeNode;
            if (node == null)
            {
                return;
            }

            StartValidationTimer(node);
        }

        private void jsonValueEditor_Leave(object sender, EventArgs e)
        {
            jsonValueEditor.TextChanged -= jsonValueEditor_TextChanged;
        }

        private void jsonValueEditor_Enter(object sender, EventArgs e)
        {
            jsonValueEditor.TextChanged += jsonValueEditor_TextChanged;
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

            jsonValueEditor.ReadOnly = true;
        }

        // ReSharper disable once UnusedParameter.Local
        private void JsonTreeView_AfterSelectImplementation(JTokenTreeNode node, TreeViewEventArgs e)
        {
            newtonsoftJsonTypeTextBox.Text = node.Tag.GetType().Name;

            jsonTypeComboBox.Text = node.JTokenTag.Type.ToString();

            // If jsonValueEditor is focused then it triggers this event in the update process, so don't update it again ! (risk: infinite loop between events).
            if (!jsonValueEditor.Focused)
            {
                jsonValueEditor.Text = node.JTokenTag.ToString();
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
                    jsonValueEditor.Text = $@"""{node.JValueTag}""";
                    break;
                default:
                    jsonValueEditor.Text = $"{node.JValueTag}";
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
            validJSon = !isError;

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
                var newNode = node.AfterJsonTextChange(jsonValueEditor.Text);
                if (newNode != null)
                {
                    jsonTreeView.SelectedNode = newNode;

                    SetJsonStatus("Json format validated.", false);
                }
                else
                {
                    SetJsonStatus("INVALID Json format", true);
                }
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

            DialogResult response = DialogResult.Yes;
            if (!validJSon)
                response = MessageBox.Show("This wizard is incorrect. Do you really want to save it ?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);

            if (response == DialogResult.Yes)
            {
                try
                {
                    using (var stream = new FileStream(OpenedFileName, FileMode.Truncate))
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
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void wizardDevGuideToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = new ProcessStartInfo("https://developer.synology.com/developer-guide/synology_package/WIZARD_UIFILES.html");
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private string GenerateHtmlPreview()
        {
            var preview = "";
            using (var stringWriter = new StringWriter())
            {
                using (var writer = new HtmlTextWriter(stringWriter))
                {
                    writer.WriteLine("<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\">");
                    foreach (JToken step in JsonEditorItem.JTokenValue.Children())
                    {
                        var imgSrc = Path.Combine(Helper.AssemblyDirectory, "backwizard.png");
                        var uri = new System.Uri(imgSrc).AbsoluteUri;
                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        writer.AddAttribute(HtmlTextWriterAttribute.Style, "background-image: url('" + uri + "'); background-position: right; color:#FFFFFF; height:50px; display: flex; vertical-align: middle;");

                        writer.RenderBeginTag(HtmlTextWriterTag.Div);
                        writer.RenderBeginTag(HtmlTextWriterTag.H3);
                        var property = GetNodeByKey(step.Children(), "step_title") as JProperty;
                        if (property != null)
                        {
                            writer.Write(property.Value.ToString());
                        }
                        else
                        {
                            writer.Write("Step title not found ?!");
                        }
                        writer.RenderEndTag();
                        writer.RenderEndTag();
                        var items = GetNodeByKey(step.Children(), "items");
                        GenerateHtmlStep(writer, items);
                        writer.RenderEndTag();
                        writer.RenderBeginTag(HtmlTextWriterTag.Hr);
                        writer.RenderEndTag();
                    }

                    preview = stringWriter.ToString();
                }
            }

            return preview;
        }

        private void GenerateHtmlStep(HtmlTextWriter writer, JToken items)
        {
            items = items.First;
            if (items != null)
            {
                foreach (JToken item in items.Children())
                {
                    var node = GetNodeByKey(item.Children(), "type") as JProperty;
                    if (node != null)
                    {
                        var desc = GetNodeByKey(item.Children(), "desc") as JProperty;
                        string description = "";
                        if (desc != null)
                            description = desc.Value.ToString();
                        var type = node.Value.ToString();
                        switch (type)
                        {
                            case "singleselect":
                                GenerateHtmlRadioButtons(writer, item, description);
                                break;
                            case "multiselect":
                                GenerateHtmlCheckBoxes(writer, item, description);
                                break;
                            case "textfield":
                                GenerateHtmlTextField(writer, item, description);
                                break;
                            case "password":
                                GenerateHtmlPassword(writer, item, description);
                                break;
                            case "combobox":
                                GenerateHtmlCombo(writer, item, description);
                                break;
                        }
                    }
                    else
                    {
                        writer.Write("Missing a 'type' under 'items/[Array]/{Object}'");
                    }
                }
            }
            else
            {
                writer.Write("Missing an [Array] under 'items'");
            }
        }

        private void GenerateHtmlCombo(HtmlTextWriter writer, JToken item, string description)
        {
            var uid = Guid.NewGuid().ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Name, uid);
            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
            if (!string.IsNullOrEmpty(description))
            {
                //writer.RenderBeginTag(HtmlTextWriterTag.Legend);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(description);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
                writer.RenderEndTag();
            }

            var subitems = GetNodeByKey(item.Children(), "subitems");
            var array = subitems.First;
            if (array != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "margin-left: 20px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (JToken combo in array.Children())
                {
                    var desc = GetNodeByKey(combo.Children(), "desc") as JProperty;
                    if (desc != null)
                        if (desc.Value.ToString() == "")
                            desc = GetNodeByKey(combo.Children(), "key") as JProperty;
                    if (desc != null)
                    {
                        writer.Write(desc.Value.ToString());
                        writer.Write(": ");
                    }
                    var value = GetNodeByKey(combo.Children(), "defaultValue") as JProperty;
                    if (value != null)
                        if (value.Value.ToString() != "")
                            writer.AddAttribute(HtmlTextWriterAttribute.Value, value.Value.ToString());
                    //if (value == null || value.Value.ToString() != "")
                    //{
                    //    var empty = GetNodeByKey(combo.Children(), "emptyText") as JProperty;
                    //    if (empty != null)
                    //        if (empty.Value.ToString() != "")
                    //            writer.AddAttribute(HtmlTextWriterAttribute.Value, empty.Value.ToString());
                    //}
                    var disabled = GetNodeByKey(combo.Children(), "disabled") as JProperty;
                    if (disabled != null)
                        if (disabled.Value.ToString() == "true")
                            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");

                    writer.RenderBeginTag(HtmlTextWriterTag.Select);
                    var mode = GetNodeByKey(combo.Children(), "mode") as JProperty;
                    if (mode.Value.ToString() == "local")
                    {
                        var store = GetNodeByKey(combo.Children(), "store");
                        var data = GetNodeByKey(store.First.Children(), "data");

                        foreach (var element in data.First.Children())
                        {
                            var displayName = element.Last.ToString();
                            writer.RenderBeginTag(HtmlTextWriterTag.Option);
                            writer.Write(displayName);
                            writer.RenderEndTag();
                        }
                    }
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Br);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("Missing an [Array] under 'subitems'");
            }
            writer.RenderEndTag();
        }

        private void GenerateHtmlPassword(HtmlTextWriter writer, JToken item, string description)
        {
            var uid = Guid.NewGuid().ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Name, uid);
            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
            if (!string.IsNullOrEmpty(description))
            {
                //writer.RenderBeginTag(HtmlTextWriterTag.Legend);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(description);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
                writer.RenderEndTag();
            }

            var subitems = GetNodeByKey(item.Children(), "subitems");
            var array = subitems.First;
            if (array != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "margin-left: 20px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (JToken radio in array.Children())
                {
                    var desc = GetNodeByKey(radio.Children(), "desc") as JProperty;
                    if (desc != null)
                        if (desc.Value.ToString() == "")
                            desc = GetNodeByKey(radio.Children(), "key") as JProperty;
                    if (desc != null)
                    {
                        writer.Write(desc.Value.ToString());
                        writer.Write(": ");
                    }
                    var value = GetNodeByKey(radio.Children(), "defaultValue") as JProperty;
                    if (value != null)
                        if (value.Value.ToString() != "")
                            writer.AddAttribute(HtmlTextWriterAttribute.Value, value.Value.ToString());
                    if (value == null || value.Value.ToString() != "")
                    {
                        var empty = GetNodeByKey(radio.Children(), "emptyText") as JProperty;
                        if (empty != null)
                            if (empty.Value.ToString() != "")
                                writer.AddAttribute(HtmlTextWriterAttribute.Value, empty.Value.ToString());
                    }
                    var disabled = GetNodeByKey(radio.Children(), "disabled") as JProperty;
                    if (disabled != null)
                        if (disabled.Value.ToString() == "true")
                            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "password");
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Br);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("Missing an [Array] under 'subitems'");
            }
            writer.RenderEndTag();
        }

        private void GenerateHtmlTextField(HtmlTextWriter writer, JToken item, string description)
        {
            var uid = Guid.NewGuid().ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Name, uid);
            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
            if (!string.IsNullOrEmpty(description))
            {
                //writer.RenderBeginTag(HtmlTextWriterTag.Legend);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(description);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
                writer.RenderEndTag();
            }

            var subitems = GetNodeByKey(item.Children(), "subitems");
            var array = subitems.First;
            if (array != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "margin-left: 20px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (JToken radio in array.Children())
                {
                    var desc = GetNodeByKey(radio.Children(), "desc") as JProperty;
                    if (desc != null)
                        if (desc.Value.ToString() == "")
                            desc = GetNodeByKey(radio.Children(), "key") as JProperty;
                    if (desc != null)
                    {
                        writer.Write(desc.Value.ToString());
                        writer.Write(": ");
                    }
                    var value = GetNodeByKey(radio.Children(), "defaultValue") as JProperty;
                    if (value != null)
                        if (value.Value.ToString() != "")
                            writer.AddAttribute(HtmlTextWriterAttribute.Value, value.Value.ToString());
                    if (value == null || value.Value.ToString() != "")
                    {
                        var empty = GetNodeByKey(radio.Children(), "emptyText") as JProperty;
                        if (empty != null)
                            if (empty.Value.ToString() != "")
                                writer.AddAttribute(HtmlTextWriterAttribute.Value, empty.Value.ToString());
                    }
                    var disabled = GetNodeByKey(radio.Children(), "disabled") as JProperty;
                    if (disabled != null)
                        if (disabled.Value.ToString() == "true")
                            writer.AddAttribute(HtmlTextWriterAttribute.Disabled, "disabled");
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "text");
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Br);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("Missing an [Array] under 'subitems'");
            }
            writer.RenderEndTag();
        }

        private void GenerateHtmlCheckBoxes(HtmlTextWriter writer, JToken item, string description)
        {
            var uid = Guid.NewGuid().ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Name, uid);
            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
            if (!string.IsNullOrEmpty(description))
            {
                //writer.RenderBeginTag(HtmlTextWriterTag.Legend);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(description);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
                writer.RenderEndTag();
            }

            var subitems = GetNodeByKey(item.Children(), "subitems");
            var array = subitems.First;
            if (array != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "margin-left: 20px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (JToken radio in array.Children())
                {
                    var desc = GetNodeByKey(radio.Children(), "desc") as JProperty;
                    if (desc != null)
                        if (desc.Value.ToString() == "")
                            desc = GetNodeByKey(radio.Children(), "key") as JProperty;
                    if (desc != null)
                    {
                        writer.Write(desc.Value.ToString());
                        writer.Write(": ");
                    }
                    var value = GetNodeByKey(radio.Children(), "defaultValue") as JProperty;
                    if (value != null)
                        if (value.Value.ToString() == "true")
                            writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "checkbox");
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, uid);
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Br);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("Missing an [Array] under 'subitems'");
            }
            writer.RenderEndTag();
        }

        private void GenerateHtmlRadioButtons(HtmlTextWriter writer, JToken item, string description)
        {
            var uid = Guid.NewGuid().ToString();
            writer.AddAttribute(HtmlTextWriterAttribute.Name, uid);
            writer.RenderBeginTag(HtmlTextWriterTag.Br);
            writer.RenderEndTag();
            writer.RenderBeginTag(HtmlTextWriterTag.Fieldset);
            if (!string.IsNullOrEmpty(description))
            {
                //writer.RenderBeginTag(HtmlTextWriterTag.Legend);
                writer.RenderBeginTag(HtmlTextWriterTag.Span);
                writer.Write(description);
                writer.RenderEndTag();
                writer.RenderBeginTag(HtmlTextWriterTag.Br);
                writer.RenderEndTag();
            }

            var subitems = GetNodeByKey(item.Children(), "subitems");
            var array = subitems.First;
            if (array != null)
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Style, "margin-left: 20px");
                writer.RenderBeginTag(HtmlTextWriterTag.Div);
                foreach (JToken radio in array.Children())
                {
                    var desc = GetNodeByKey(radio.Children(), "desc") as JProperty;
                    if (desc != null)
                        if (desc.Value.ToString() == "")
                            desc = GetNodeByKey(radio.Children(), "key") as JProperty;
                    if (desc != null)
                    {
                        writer.Write(desc.Value.ToString());
                        writer.Write(": ");
                    }
                    var value = GetNodeByKey(radio.Children(), "defaultValue") as JProperty;
                    if (value != null)
                        if (value.Value.ToString() == "true")
                            writer.AddAttribute(HtmlTextWriterAttribute.Checked, "checked");
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, "radio");
                    writer.AddAttribute(HtmlTextWriterAttribute.Name, uid);
                    writer.RenderBeginTag(HtmlTextWriterTag.Input);
                    writer.RenderEndTag();
                    writer.RenderBeginTag(HtmlTextWriterTag.Br);
                    writer.RenderEndTag();
                }
                writer.RenderEndTag();
            }
            else
            {
                writer.Write("Missing an [Array] under 'subitems'");
            }
            writer.RenderEndTag();
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            var preview = GenerateHtmlPreview();

            webBrowserPreview.DocumentText = preview;
        }

        private JToken GetNodeByKey(JEnumerable<JToken> nodes, string key)
        {
            JToken found = null;
            foreach (JToken node in nodes)
            {
                var property = node as JProperty;
                if (property != null)
                {
                    if (property.Name == key)
                    {
                        found = node;
                        break;
                    }
                }
            }
            return found;
        }
    }
}
