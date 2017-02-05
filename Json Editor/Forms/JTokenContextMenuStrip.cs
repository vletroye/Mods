using Newtonsoft.Json.Linq;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZTn.Json.Editor.Extensions;
using ZTn.Json.Editor.Generic;
using ZTn.Json.Editor.Properties;

namespace ZTn.Json.Editor.Forms
{
    class JTokenContextMenuStrip : ContextMenuStrip
    {
        /// <summary>
        /// Source <see cref="TreeNode"/> at the origin of this <see cref="ContextMenuStrip"/>
        /// </summary>
        protected JTokenTreeNode JTokenNode;

        protected ToolStripItem AddWizardStepToolStripItem;
        protected ToolStripItem AddWizardItemToolStripItem;
        protected ToolStripItem AddWizardSubitemToolStripItem;
        protected ToolStripItem RemoveWizardStepToolStripItem;
        protected ToolStripItem RemoveWizardItemToolStripItem;
        protected ToolStripItem RemoveWizardSubitemToolStripItem;

        protected ToolStripItem CollapseAllToolStripItem;
        protected ToolStripItem ExpandAllToolStripItem;

        protected ToolStripMenuItem EditToolStripItem;

        protected ToolStripItem CopyNodeToolStripItem;
        protected ToolStripItem CutNodeToolStripItem;
        protected ToolStripItem DeleteNodeToolStripItem;
        protected ToolStripItem PasteNodeAfterToolStripItem;
        protected ToolStripItem PasteNodeBeforeToolStripItem;
        protected ToolStripItem PasteNodeReplaceToolStripItem;

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JTokenContextMenuStrip"/> class.
        /// </summary>
        public JTokenContextMenuStrip()
        {
            CollapseAllToolStripItem = new ToolStripMenuItem(Resources.CollapseAll, null, CollapseAll_Click);
            ExpandAllToolStripItem = new ToolStripMenuItem(Resources.ExpandAll, null, ExpandAll_Click);

            EditToolStripItem = new ToolStripMenuItem(Resources.Edit);

            CopyNodeToolStripItem = new ToolStripMenuItem(Resources.Copy, null, CopyNode_Click);
            CutNodeToolStripItem = new ToolStripMenuItem(Resources.Cut, null, CutNode_Click);
            DeleteNodeToolStripItem = new ToolStripMenuItem(Resources.DeleteNode, null, DeleteNode_Click);
            PasteNodeAfterToolStripItem = new ToolStripMenuItem(Resources.PasteNodeAfter, null, PasteNodeAfter_Click);
            PasteNodeBeforeToolStripItem = new ToolStripMenuItem(Resources.PasteNodeBefore, null, PasteNodeBefore_Click);
            PasteNodeReplaceToolStripItem = new ToolStripMenuItem(Resources.Replace, null, PasteNodeReplace_Click);

            EditToolStripItem.DropDownItems.Add(CopyNodeToolStripItem);
            EditToolStripItem.DropDownItems.Add(CutNodeToolStripItem);
            EditToolStripItem.DropDownItems.Add(PasteNodeBeforeToolStripItem);
            EditToolStripItem.DropDownItems.Add(PasteNodeAfterToolStripItem);
            EditToolStripItem.DropDownItems.Add(new ToolStripSeparator());
            EditToolStripItem.DropDownItems.Add(PasteNodeReplaceToolStripItem);
            EditToolStripItem.DropDownItems.Add(new ToolStripSeparator());
            EditToolStripItem.DropDownItems.Add(DeleteNodeToolStripItem);

            AddWizardStepToolStripItem = new ToolStripMenuItem("Add a Step", null, AddWizardStep_Click);
            AddWizardItemToolStripItem = new ToolStripMenuItem("Add an Item", null, AddWizardItem_Click);
            AddWizardSubitemToolStripItem = new ToolStripMenuItem("Add a Subitem", null, AddWizardSubitem_Click);
            RemoveWizardStepToolStripItem = new ToolStripMenuItem("Remove Step", null, RemoveWizardStep_Click);
            RemoveWizardItemToolStripItem = new ToolStripMenuItem("Remove Item", null, RemoveWizardItem_Click);
            RemoveWizardSubitemToolStripItem = new ToolStripMenuItem("Remove Subitem", null, RemoveWizardSubitem_Click);

            var entry = Items.Add("Wizard");
            entry.Font = new Font(entry.Font, FontStyle.Bold | FontStyle.Underline);
            Items.Add(AddWizardStepToolStripItem);
            Items.Add(AddWizardItemToolStripItem);
            Items.Add(AddWizardSubitemToolStripItem);
            Items.Add(RemoveWizardStepToolStripItem);
            Items.Add(RemoveWizardItemToolStripItem);
            Items.Add(RemoveWizardSubitemToolStripItem);
            Items.Add("-");
            Items.Add("Manual").Font = entry.Font;
            Items.Add(CollapseAllToolStripItem);
            Items.Add(ExpandAllToolStripItem);
            Items.Add(EditToolStripItem);
        }

        #endregion

        #region >> ContextMenuStrip

        /// <inheritdoc />
        protected override void OnVisibleChanged(EventArgs e)
        {
            var type = "";

            if (Visible)
            {
                JTokenNode = FindSourceTreeNode<JTokenTreeNode>();

                // Collapse item shown if node is expanded and has children
                CollapseAllToolStripItem.Visible = JTokenNode.IsExpanded
                    && JTokenNode.Nodes.Cast<TreeNode>().Any();

                // Expand item shown if node if not expanded or has a children not expanded
                ExpandAllToolStripItem.Visible = !JTokenNode.IsExpanded
                    || JTokenNode.Nodes.Cast<TreeNode>().Any(t => !t.IsExpanded);

                // Remove item enabled if it is not the root or the value of a property
                DeleteNodeToolStripItem.Enabled = (JTokenNode.Parent != null)
                    && !(JTokenNode.Parent is JPropertyTreeNode);

                // Cut item enabled if delete is
                CutNodeToolStripItem.Enabled = DeleteNodeToolStripItem.Enabled;

                // Paste items enabled only when a copy or cut operation is pending
                PasteNodeAfterToolStripItem.Enabled = !EditorClipboard<JTokenTreeNode>.IsEmpty()
                    && (JTokenNode.Parent != null)
                    && !(JTokenNode.Parent is JPropertyTreeNode);

                PasteNodeBeforeToolStripItem.Enabled = !EditorClipboard<JTokenTreeNode>.IsEmpty()
                    && (JTokenNode.Parent != null)
                    && !(JTokenNode.Parent is JPropertyTreeNode);

                PasteNodeReplaceToolStripItem.Enabled = !EditorClipboard<JTokenTreeNode>.IsEmpty()
                    && (JTokenNode.Parent != null);

                AddWizardStepToolStripItem.Visible = (JTokenNode.Level == 0 && JTokenNode.Text.StartsWith("[Array]"));
                AddWizardItemToolStripItem.Visible = (JTokenNode.Level == 3 && JTokenNode.Text.StartsWith("[Array]"));
                AddWizardSubitemToolStripItem.Visible = (JTokenNode.Level == 6 && JTokenNode.Text.StartsWith("[Array]"));
                if (AddWizardSubitemToolStripItem.Visible)
                {
                    try
                    {
                        type = JTokenNode.Parent.Parent.FirstNode.FirstNode.Text;
                    }
                    catch
                    {
                        MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    type = Helper.GetSubItemType(type);
                    if (type != "")
                    {
                        AddWizardSubitemToolStripItem.Text = "Add a " + type;
                    }
                }
                RemoveWizardStepToolStripItem.Visible = (JTokenNode.Level == 1 && JTokenNode.Text.StartsWith("{Object}"));
                if (RemoveWizardStepToolStripItem.Visible)
                {
                    try
                    {
                        RemoveWizardStepToolStripItem.Text = string.Format("Remove '{0}'", JTokenNode.FirstNode.FirstNode.Text);
                    }
                    catch
                    {
                        MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                RemoveWizardItemToolStripItem.Visible = (JTokenNode.Level == 4 && JTokenNode.Text.StartsWith("{Object}"));
                if (RemoveWizardItemToolStripItem.Visible)
                {
                    try
                    {

                        RemoveWizardItemToolStripItem.Text = string.Format("Remove '{0}'", JTokenNode.Nodes[1].FirstNode.Text);
                    }
                    catch
                    {
                        MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                RemoveWizardSubitemToolStripItem.Visible = (JTokenNode.Level == 7 && JTokenNode.Text.StartsWith("{Object}"));
                if (RemoveWizardSubitemToolStripItem.Visible)
                {
                    try
                    {
                        RemoveWizardSubitemToolStripItem.Text = string.Format("Remove '{0}'", JTokenNode.Nodes[1].FirstNode.Text);
                    }
                    catch
                    {
                        MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            base.OnVisibleChanged(e);
        }

        #endregion

        /// <summary>
        /// Click event handler for <see cref="CollapseAllToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CollapseAll_Click(Object sender, EventArgs e)
        {
            if (JTokenNode != null)
            {
                JTokenNode.TreeView.BeginUpdate();

                JTokenNode.Collapse(false);

                JTokenNode.TreeView.EndUpdate();
            }
        }

        private void AddWizardStep_Click(Object sender, EventArgs e)
        {
            var newStep = new StepDefinition();
            newStep.ShowDialog(this);
            var stepName = newStep.Title;

            if (string.IsNullOrEmpty(stepName))
                return;

            var stepValidation = newStep.Validation;
            var stepActivation = newStep.Activation;
            var stepDeactivation = newStep.Deactivation;

            JTokenNode = (JTokenTreeNode)InsertJToken(JObject.Parse("{}"), true);

            var items = InsertProperty("items", new JArray());
            if (!string.IsNullOrEmpty(stepActivation))
            {
                InsertProperty("activate", stepActivation); ////Should I do the same Typo 'activeate' as in official Synology documentation??
            }
            if (!string.IsNullOrEmpty(stepDeactivation))
            {
                InsertProperty("deactivate", stepDeactivation); //"{console.log('deactivate', arguments);}"
            }
            InsertProperty("invalid_next_disabled", stepValidation.ToString());
            InsertProperty("step_title", stepName);

            items.Expand();
            JTokenNode.TreeView.SelectedNode = items.FirstNode;
            JTokenNode = (JTokenTreeNode)items.FirstNode;
            JTokenNode.NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold | FontStyle.Underline);
        }

        private void AddWizardItem_Click(Object sender, EventArgs e)
        {
            var newItem = new ItemDefinition();
            newItem.ShowDialog(this);
            var itemType = newItem.Type;

            if (string.IsNullOrEmpty(itemType))
                return;

            var itemDescription = newItem.Description;

            JTokenNode = (JTokenTreeNode)InsertJToken(JObject.Parse("{}"), true);

            var subitems = InsertProperty("subitems", new JArray());
            if (!string.IsNullOrEmpty(itemDescription))
            {
                InsertProperty("desc", itemDescription);
            }
            InsertProperty("type", itemType);

            subitems.Expand();
            JTokenNode.TreeView.SelectedNode = subitems.FirstNode;
            JTokenNode = (JTokenTreeNode)subitems.FirstNode;
            JTokenNode.NodeFont = new Font(SystemFonts.DefaultFont, FontStyle.Bold | FontStyle.Underline);
        }

        private void AddWizardSubitem_Click(Object sender, EventArgs e)
        {
            var node = JTokenNode;
            var type = "";
            try
            {
                type = node.Parent.Parent.FirstNode.FirstNode.Text;
            }
            catch
            {
                MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!string.IsNullOrEmpty(type))
            {
                var newSubitem = new SubitemDefinition(type);
                newSubitem.ShowDialog(this);
                var subitemKey = newSubitem.Key;

                if (string.IsNullOrEmpty(subitemKey))
                    return;

                var subitemDescription = newSubitem.Description;
                var subitemDefaultValue = newSubitem.DefaultValue;
                var subitemEmptyValue = newSubitem.EmptyValue;
                var subitemInvalidValue = newSubitem.InvalidValue;
                var subitemWidth = newSubitem.Width;
                var subitemHeight = newSubitem.Height;
                var subitemDisabled = newSubitem.Disable;
                var subitemHidden = newSubitem.Hidden;
                var subitemPreventMark = newSubitem.PreventMark;
                var subitemBaseParams = newSubitem.BaseParams;
                var subitemRoot = newSubitem.Root;
                var subitemApi = newSubitem.Api;
                var subitemValueField = newSubitem.ValueField;
                var subitemDisplayField = newSubitem.DisplayField;
                var subitemStaticCombo  = newSubitem.StaticCombo;
                var subitemValueFieldUnique = newSubitem.ValueFieldIsUnique;
                var subitemDisplayFieldUnique = newSubitem.DisplayFieldIsUnique;


                JTokenNode = (JTokenTreeNode)InsertJToken(JObject.Parse("{}"), true);

                if (subitemBaseParams.Count > 0)
                {
                    if (subitemStaticCombo)
                    {
                        // Static Combo is filled
                        var comboNode = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("store", new JObject()).FirstNode;
                        InsertProperty("xtype", "arraystore");
                        var storeNode = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("fields", new JArray()).FirstNode;
                        if (!string.IsNullOrEmpty(subitemValueField))
                            InsertJToken(new JValue(subitemValueField));
                        if (!string.IsNullOrEmpty(subitemDisplayField))
                            InsertJToken(new JValue(subitemDisplayField));
                        JTokenNode = storeNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("data", new JArray()).FirstNode;
                        foreach (var item in subitemBaseParams)
                        {
                            InsertJToken(new JArray(new JValue(item.value), new JValue(item.name)));
                        }

                        JTokenNode = comboNode;
                        InsertProperty("mode", "local");
                        if (string.IsNullOrEmpty(subitemDefaultValue))
                            subitemDefaultValue = "false";
                        InsertProperty("editable", Convert.ToBoolean(subitemDefaultValue));
                        if (!string.IsNullOrEmpty(subitemValueField))
                            InsertProperty("valuefield", subitemValueField);
                        if (!string.IsNullOrEmpty(subitemDisplayField))
                            InsertProperty("displayfield", subitemDisplayField);
                    }
                    else
                    {
                        // Dynamic Combo is filled
                        var comboNode = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("api_store", new JObject()).FirstNode;
                        InsertProperty("api", subitemApi);
                        InsertProperty("method", "list");
                        InsertProperty("version", 1);
                        InsertProperty("root", subitemRoot);
                        if (subitemValueFieldUnique)
                        {
                            InsertProperty("idProperty", subitemValueField);
                        }
                        else if (subitemDisplayFieldUnique)
                        {
                            InsertProperty("idProperty", subitemDisplayField);
                        }
                        var storeNode = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("fields", new JArray()).FirstNode;
                        if (!string.IsNullOrEmpty(subitemValueField))
                            InsertJToken(new JValue(subitemValueField));
                        if (!string.IsNullOrEmpty(subitemDisplayField))
                            InsertJToken(new JValue(subitemDisplayField));
                        JTokenNode = storeNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("baseParams", new JObject()).FirstNode;
                        foreach (var item in subitemBaseParams)
                        {
                            InsertProperty(item.name, item.value);
                        }

                        JTokenNode = comboNode;
                        InsertProperty("mode", "remote");
                        if (string.IsNullOrEmpty(subitemDefaultValue))
                            subitemDefaultValue = "false";
                        InsertProperty("editable", Convert.ToBoolean(subitemDefaultValue));
                        if (!string.IsNullOrEmpty(subitemValueField))
                            InsertProperty("valuefield", subitemValueField);
                        if (!string.IsNullOrEmpty(subitemDisplayField))
                            InsertProperty("displayfield", subitemDisplayField);
                    }
                }

                if (subitemDisabled) InsertProperty("disabled", subitemDisabled);
                if (subitemHidden) InsertProperty("hidden", subitemHidden);
                if (subitemPreventMark) InsertProperty("preventMark", subitemPreventMark);
                if (!string.IsNullOrEmpty(subitemWidth))
                {
                    InsertProperty("width", subitemWidth);
                }
                if (!string.IsNullOrEmpty(subitemHeight))
                {
                    InsertProperty("height", subitemHeight);
                }
                if (!string.IsNullOrEmpty(subitemDescription))
                {
                    InsertProperty("desc", subitemDescription);
                }
                InsertProperty("key", subitemKey);

                JTokenNode.TreeView.SelectedNode = node;
                JTokenNode = node;
            }
        }

        private void RemoveWizardStep_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.EditDelete();
            }
            catch (JTokenTreeNodeDeleteException exception)
            {
                MessageBox.Show(exception.InnerException.Message, Resources.DeletionActionFailed);
            }
        }

        private void RemoveWizardItem_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.EditDelete();
            }
            catch (JTokenTreeNodeDeleteException exception)
            {
                MessageBox.Show(exception.InnerException.Message, Resources.DeletionActionFailed);
            }
        }

        private void RemoveWizardSubitem_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.EditDelete();
            }
            catch (JTokenTreeNodeDeleteException exception)
            {
                MessageBox.Show(exception.InnerException.Message, Resources.DeletionActionFailed);
            }
        }

        /// <summary>
        /// Add a new <see cref="JToken"/> instance in current <see cref="JArrayTreeNode"/>
        /// </summary>
        /// <param name="newJToken"></param>
        private TreeNode InsertJToken(JToken newJToken, bool last = false)
        {
            var jArrayTreeNode = JTokenNode as JArrayTreeNode;

            if (jArrayTreeNode == null)
            {
                return null;
            }

            if (!last)
                jArrayTreeNode.JArrayTag.AddFirst(newJToken);
            else
                jArrayTreeNode.JArrayTag.Add(newJToken);

            TreeNode newTreeNode = JsonTreeNodeFactory.Create(newJToken);
            if (!last)
                jArrayTreeNode.Nodes.Insert(0, newTreeNode);
            else
                jArrayTreeNode.Nodes.Insert(jArrayTreeNode.Nodes.Count, newTreeNode);

            jArrayTreeNode.TreeView.SelectedNode = newTreeNode;

            return newTreeNode;
        }

        private JPropertyTreeNode InsertProperty(string name, object propertyValue, bool last = false)
        {
            var jObjectTreeNode = JTokenNode as JObjectTreeNode;

            if (jObjectTreeNode == null)
            {
                return null;
            }

            var newJProperty = new JProperty(name, propertyValue);
            if (!last)
                jObjectTreeNode.JObjectTag.AddFirst(newJProperty);
            else
                jObjectTreeNode.JObjectTag.Add(newJProperty);

            var jPropertyTreeNode = (JPropertyTreeNode)JsonTreeNodeFactory.Create(newJProperty);
            jObjectTreeNode.Nodes.Insert(0, jPropertyTreeNode);

            jObjectTreeNode.TreeView.SelectedNode = jPropertyTreeNode;

            return jPropertyTreeNode;
        }

        /// <summary>
        /// Click event handler for <see cref="CopyNodeToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CopyNode_Click(Object sender, EventArgs e)
        {
            JTokenNode.ClipboardCopy();
        }

        /// <summary>
        /// Click event handler for <see cref="CutNodeToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CutNode_Click(Object sender, EventArgs e)
        {
            JTokenNode.ClipboardCut();
        }

        /// <summary>
        /// Click event handler for <see cref="DeleteNodeToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void DeleteNode_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.EditDelete();
            }
            catch (JTokenTreeNodeDeleteException exception)
            {
                MessageBox.Show(exception.InnerException.Message, Resources.DeletionActionFailed);
            }
        }

        /// <summary>
        /// Click event handler for <see cref="ExpandAllToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ExpandAll_Click(Object sender, EventArgs e)
        {
            if (JTokenNode != null)
            {
                JTokenNode.TreeView.BeginUpdate();

                JTokenNode.ExpandAll();

                JTokenNode.TreeView.EndUpdate();
            }
        }

        /// <summary>
        /// Click event handler for <see cref="PasteNodeAfterToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PasteNodeAfter_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.ClipboardPasteAfter();
            }
            catch (JTokenTreeNodePasteException exception)
            {
                MessageBox.Show(exception.InnerException.Message, Resources.PasteActionFailed);
            }
        }

        /// <summary>
        /// Click event handler for <see cref="PasteNodeBeforeToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PasteNodeBefore_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.ClipboardPasteBefore();
            }
            catch (JTokenTreeNodePasteException exception)
            {
                MessageBox.Show(exception.InnerException.Message, Resources.PasteActionFailed);
            }
        }

        /// <summary>
        /// Click event handler for <see cref="PasteNodeReplaceToolStripItem"/>.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void PasteNodeReplace_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.ClipboardPasteReplace();
            }
            catch (JTokenTreeNodePasteException exception)
            {
                MessageBox.Show(exception.InnerException.Message, Resources.PasteActionFailed);
            }
        }

        /// <summary>
        /// Identify the Source <see cref="TreeNode"/> at the origin of this <see cref="ContextMenuStrip"/>.
        /// </summary>
        /// <typeparam name="T">Subtype of <see cref="TreeNode"/> to return.</typeparam>
        /// <returns></returns>
        public T FindSourceTreeNode<T>() where T : TreeNode
        {
            if (SourceControl == null)
            {
                return null;
            }

            var treeView = SourceControl as TreeView;
            if (treeView == null)
            {
                return null;
            }

            return treeView.SelectedNode as T;
        }
    }
}
