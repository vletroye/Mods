using Newtonsoft.Json.Linq;
using System;
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

            Items.Add(AddWizardStepToolStripItem);
            Items.Add(AddWizardItemToolStripItem);
            Items.Add(AddWizardSubitemToolStripItem);
            Items.Add("-");
            Items.Add(CollapseAllToolStripItem);
            Items.Add(ExpandAllToolStripItem);
            Items.Add(EditToolStripItem);
        }

        #endregion

        #region >> ContextMenuStrip

        /// <inheritdoc />
        protected override void OnVisibleChanged(EventArgs e)
        {
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

                AddWizardStepToolStripItem.Visible = (JTokenNode.Level == 0);
                AddWizardItemToolStripItem.Visible = (JTokenNode.Level == 3);
                AddWizardSubitemToolStripItem.Visible = (JTokenNode.Level == 6);
                if (JTokenNode.Level == 6)
                {
                    var type = "";
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

            JTokenNode = (JTokenTreeNode)InsertJToken(JObject.Parse("{}"));
            var items = InsertProperty("items", new JArray());
            if (!string.IsNullOrEmpty(stepActivation))
            {
                InsertProperty("activeate", stepActivation); //Typo 'activeate' is from official Synology documentation!!
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
        }

        private void AddWizardItem_Click(Object sender, EventArgs e)
        {
            var newItem = new ItemDefinition();
            newItem.ShowDialog(this);
            var itemType = newItem.Type;

            if (string.IsNullOrEmpty(itemType))
                return;

            var itemDescription = newItem.Description;

            JTokenNode = (JTokenTreeNode)InsertJToken(JObject.Parse("{}"));

            var subitems = InsertProperty("subitems", new JArray());
            if (!string.IsNullOrEmpty(itemDescription))
            {
                InsertProperty("desc", itemDescription);
            }
            InsertProperty("type", itemType);

            subitems.Expand();
            JTokenNode.TreeView.SelectedNode = subitems.FirstNode;
            JTokenNode = (JTokenTreeNode)subitems.FirstNode;
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

                JTokenNode = (JTokenTreeNode)InsertJToken(JObject.Parse("{}"));


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
                if (!string.IsNullOrEmpty(subitemEmptyValue))
                {
                    InsertProperty("emptyText", subitemEmptyValue);
                }
                if (!string.IsNullOrEmpty(subitemDefaultValue))
                {
                    InsertProperty("defaultVaule", subitemDefaultValue); //Typo 'defaultVaule' is from official Synology documentation!!
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

        /// <summary>
        /// Add a new <see cref="JToken"/> instance in current <see cref="JArrayTreeNode"/>
        /// </summary>
        /// <param name="newJToken"></param>
        private TreeNode InsertJToken(JToken newJToken)
        {
            var jArrayTreeNode = JTokenNode as JArrayTreeNode;

            if (jArrayTreeNode == null)
            {
                return null;
            }

            jArrayTreeNode.JArrayTag.AddFirst(newJToken);

            TreeNode newTreeNode = JsonTreeNodeFactory.Create(newJToken);
            jArrayTreeNode.Nodes.Insert(0, newTreeNode);

            jArrayTreeNode.TreeView.SelectedNode = newTreeNode;

            return newTreeNode;
        }

        private JPropertyTreeNode InsertProperty(string name, object propertyValue)
        {
            var jObjectTreeNode = JTokenNode as JObjectTreeNode;

            if (jObjectTreeNode == null)
            {
                return null;
            }

            var newJProperty = new JProperty(name, propertyValue);
            jObjectTreeNode.JObjectTag.AddFirst(newJProperty);

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
