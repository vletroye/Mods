using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
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
        protected ToolStripItem EditWizardStepToolStripItem;
        protected ToolStripItem RemoveWizardItemToolStripItem;
        protected ToolStripItem RemoveWizardSubitemToolStripItem;
        protected ToolStripItem EditWizardSubitemToolStripItem;

        protected ToolStripItem CollapseAllToolStripItem;
        protected ToolStripItem ExpandAllToolStripItem;

        protected ToolStripMenuItem EditToolStripItem;

        protected ToolStripItem CopyNodeToolStripItem;
        protected ToolStripItem CutNodeToolStripItem;
        protected ToolStripItem DeleteNodeToolStripItem;
        protected ToolStripItem PasteNodeAfterToolStripItem;
        protected ToolStripItem PasteNodeBeforeToolStripItem;
        protected ToolStripItem PasteNodeReplaceToolStripItem;

        protected ToolStripLabel WizardToolStripLabel;
        protected ToolStripLabel ManualToolStripLabel;
        protected ToolStripItem SeparatorToolStripItem;

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
            AddWizardItemToolStripItem = new ToolStripMenuItem("Add an Item Group", null, AddWizardItem_Click);
            AddWizardSubitemToolStripItem = new ToolStripMenuItem("Add a Subitem", null, AddWizardSubitem_Click);
            RemoveWizardStepToolStripItem = new ToolStripMenuItem("Remove Step", null, RemoveWizardStep_Click);
            EditWizardStepToolStripItem = new ToolStripMenuItem("Edit Step", null, EditWizardStep_Click);
            RemoveWizardItemToolStripItem = new ToolStripMenuItem("Remove Item Group", null, RemoveWizardItem_Click);
            RemoveWizardSubitemToolStripItem = new ToolStripMenuItem("Remove Subitem", null, RemoveWizardSubitem_Click);
            EditWizardSubitemToolStripItem = new ToolStripMenuItem("Edit Subitem", null, EditWizardSubitem_Click);

            WizardToolStripLabel = new ToolStripLabel("Wizard");
            WizardToolStripLabel.Font = new Font(WizardToolStripLabel.Font, FontStyle.Bold | FontStyle.Underline);
            Items.Add(WizardToolStripLabel);
            Items.Add(AddWizardStepToolStripItem);
            Items.Add(AddWizardItemToolStripItem);
            Items.Add(AddWizardSubitemToolStripItem);
            Items.Add(RemoveWizardStepToolStripItem);
            Items.Add(EditWizardStepToolStripItem);
            Items.Add(RemoveWizardItemToolStripItem);
            Items.Add(RemoveWizardSubitemToolStripItem);
            Items.Add(EditWizardSubitemToolStripItem);
            SeparatorToolStripItem = Items.Add("-");
            ManualToolStripLabel = new ToolStripLabel("Manual");
            ManualToolStripLabel.Font = WizardToolStripLabel.Font;
            Items.Add(ManualToolStripLabel);
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

                var wizard = false;

                AddWizardStepToolStripItem.Visible = (JTokenNode.Level == 0 && JTokenNode.Text.StartsWith("[Array]"));
                wizard |= AddWizardStepToolStripItem.Visible;
                AddWizardItemToolStripItem.Visible = (JTokenNode.Level == 3 && JTokenNode.Text.StartsWith("[Array]"));
                wizard |= AddWizardItemToolStripItem.Visible;
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
                        wizard = true;
                    }
                }
                RemoveWizardStepToolStripItem.Visible = (JTokenNode.Level == 1 && JTokenNode.Text.StartsWith("{Object}"));
                if (RemoveWizardStepToolStripItem.Visible)
                {
                    try
                    {
                        var node = GetNodeByTag(JTokenNode, "step_title");
                        if (node != null)
                        {
                            string desc = ((JValue)(((JValueTreeNode)node).JTokenTag)).Value.ToString();
                            if (desc.Length > 10)
                                desc = desc.Substring(0, 10) + "...";
                            RemoveWizardStepToolStripItem.Text = string.Format("Remove Step '{0}'", desc);
                            wizard = true;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                EditWizardStepToolStripItem.Visible = (JTokenNode.Level == 1 && JTokenNode.Text.StartsWith("{Object}"));
                if (EditWizardStepToolStripItem.Visible)
                {
                    try
                    {
                        var node = GetNodeByTag(JTokenNode, "step_title");
                        if (node != null)
                        {
                            string desc = ((JValue)(((JValueTreeNode)node).JTokenTag)).Value.ToString();
                            if (desc.Length > 10)
                                desc = desc.Substring(0, 10) + "...";
                            EditWizardStepToolStripItem.Text = string.Format("Edit Step '{0}'", desc );
                            wizard = true;
                        }
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
                        var node = GetNodeByTag(JTokenNode, "desc");
                        if (node != null)
                        {
                            string desc = node.Text;
                            if (desc.Length > 10)
                                desc = desc.Substring(0, 10) + "...";
                            RemoveWizardItemToolStripItem.Text = string.Format("Remove items of '{0}'", desc);
                            wizard = true;
                        }
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
                        var node = GetNodeByTag(JTokenNode, "desc");
                        if (node != null)
                        {
                            string desc = ((JValue)(((JValueTreeNode)node).JTokenTag)).Value.ToString();
                            if (desc.Length > 10)
                                desc = desc.Substring(0, 10) + "...";
                            RemoveWizardSubitemToolStripItem.Text = string.Format("Remove '{0}'", desc);
                            wizard = true;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                EditWizardSubitemToolStripItem.Visible = (JTokenNode.Level == 7 && JTokenNode.Text.StartsWith("{Object}"));
                if (EditWizardSubitemToolStripItem.Visible)
                {
                    try
                    {
                        var node = GetNodeByTag(JTokenNode, "desc");
                        if (node != null)
                        {
                            var desc = ((JValue)(((JValueTreeNode)node).JTokenTag)).Value.ToString();
                            if (desc.Length > 10)
                                desc = desc.Substring(0, 10) + "...";
                            EditWizardSubitemToolStripItem.Text = string.Format("Edit '{0}'", desc);
                            wizard = true;
                        }
                    }
                    catch
                    {
                        MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                WizardToolStripLabel.Visible = wizard;
                SeparatorToolStripItem.Visible = wizard;
            }

            base.OnVisibleChanged(e);
        }

        private TreeNode GetNodeByTag(JTokenTreeNode parent, string tag)
        {
            TreeNode child = null;
            if (parent != null)
            {
                foreach (JPropertyTreeNode node in parent.Nodes)
                {
                    if (node.JPropertyTag.Name == tag)
                    {
                        child = node.FirstNode;
                        break;
                    }
                }
            }
            return child;
        }

        private object GetNodeTextByTag(JTokenTreeNode parent, string tag, object defaultValue = null)
        {
            object text = defaultValue;
            if (parent != null)
            {
                JValueTreeNode child = GetNodeByTag(parent, tag) as JValueTreeNode;
                text = child == null ? "" : ((JValue)child.JTokenTag).Value;
            }
            return text;
        }

        private void SetNodeTextByTag(JTokenTreeNode parent, string tag, object value, object defaultValue)
        {
            JValueTreeNode child = GetNodeByTag(parent, tag) as JValueTreeNode;
            if (child != null)
            {
                if (value.Equals(defaultValue))
                {
                    ((JPropertyTreeNode)child.Parent).AfterJsonTextChange("");
                }
                else
                {
                    var newJProperty = new JProperty(tag, value);
                    ((JPropertyTreeNode)child.Parent).AfterJsonTextChange(newJProperty.ToString());
                }
            }
            else if (!value.Equals(defaultValue))
                InsertProperty(tag, value);
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
            InsertProperty("invalid_next_disabled", stepValidation);
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

            //Add automatically one subitem
            AddWizardSubitem_Click(sender, e);
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
                var subitemEmptyText = newSubitem.EmptyText;
                var subitemInvalidText = newSubitem.InvalidText;
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
                var subitemStaticCombo = newSubitem.StaticCombo;
                var subitemValueFieldUnique = newSubitem.ValueFieldIsUnique;
                var subitemDisplayFieldUnique = newSubitem.DisplayFieldIsUnique;
                var subitemEditable = newSubitem.Editable;
                var subitemAutoSelect = newSubitem.AutoSelect;
                var subitemForceSelection = newSubitem.ForceSelection;

                var subitemAllowBlank = newSubitem.AllowBlank;
                var subitemMinLength = newSubitem.MinLength;
                var subitemMaxLength = newSubitem.MaxLength;
                var subitemvType = newSubitem.vType;
                var subitemRegex = newSubitem.Regex;
                var subitemFn = newSubitem.Fn;
                var subitemBlankText = newSubitem.BlankText;
                var subitemGrow = newSubitem.Grow;
                var subitemGrowMax = newSubitem.GrowMax;
                var subitemGrowMin = newSubitem.GrowMin;
                var subitemHtmlEncode = newSubitem.HtmlEncode;
                var subitemMaxLengthText = newSubitem.MaxLengthText;
                var subitemMinLengthText = newSubitem.MinLengthText;

                JTokenNode = (JTokenTreeNode)InsertJToken(JObject.Parse("{}"), true);

                if (type == "combobox")
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
                        InsertProperty("editable", subitemEditable);
                        InsertProperty("autoSelect", subitemAutoSelect);
                        InsertProperty("forceSelection", subitemForceSelection);
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
                        InsertProperty("editable", subitemEditable);
                        InsertProperty("autoSelect", subitemAutoSelect);
                        InsertProperty("forceSelection", subitemForceSelection);
                        if (!string.IsNullOrEmpty(subitemValueField))
                            InsertProperty("valuefield", subitemValueField);
                        if (!string.IsNullOrEmpty(subitemDisplayField))
                            InsertProperty("displayfield", subitemDisplayField);
                    }
                }

                if (type == "textfield" || type == "password")
                {
                    if (!string.IsNullOrEmpty(subitemRegex) ||
                        subitemAllowBlank ||
                        !string.IsNullOrEmpty(subitemMinLength) ||
                        !string.IsNullOrEmpty(subitemMaxLength) ||
                        !string.IsNullOrEmpty(subitemvType) ||
                        !string.IsNullOrEmpty(subitemFn))
                    {
                        var selectedNode = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("validator", new JObject()).FirstNode;
                        if (!string.IsNullOrEmpty(subitemRegex))
                        {
                            var validatorNode = JTokenNode;
                            JTokenNode = (JTokenTreeNode)InsertProperty("regex", new JObject()).FirstNode;
                            InsertProperty("expr", subitemRegex);
                            InsertProperty("errorText", subitemInvalidText);
                            JTokenNode = validatorNode;
                        }

                        InsertProperty("allowBlank", subitemAllowBlank);
                        if (!string.IsNullOrEmpty(subitemMinLength))
                            InsertProperty("minLength", subitemMinLength);
                        if (!string.IsNullOrEmpty(subitemMaxLength))
                            InsertProperty("maxLength", subitemMaxLength);
                        if (!string.IsNullOrEmpty(subitemvType))
                            InsertProperty("vtype", subitemvType);
                        if (!string.IsNullOrEmpty(subitemFn))
                            InsertProperty("fn", subitemFn);

                        JTokenNode = selectedNode;
                    }
                }


                if (string.IsNullOrEmpty(subitemRegex) && !string.IsNullOrEmpty(subitemInvalidText))
                    InsertProperty("invalidText", subitemInvalidText);

                if (type == "textfield" || type == "password" || type == "combobox")
                {
                    if (!string.IsNullOrEmpty(subitemBlankText))
                        InsertProperty("blankText", subitemBlankText);
                    if (subitemGrow)
                    {
                        InsertProperty("grow", subitemGrow);
                        if (!string.IsNullOrEmpty(subitemGrowMax))
                            InsertProperty("growMax", subitemGrowMax);
                        if (!string.IsNullOrEmpty(subitemGrowMin))
                            InsertProperty("growMin", subitemGrowMin);
                    }
                    if (subitemHtmlEncode)
                        InsertProperty("htmlEncode", subitemHtmlEncode);
                    if (!string.IsNullOrEmpty(subitemMaxLengthText))
                        InsertProperty("maxLengthText", subitemMaxLengthText);
                    if (!string.IsNullOrEmpty(subitemMinLengthText))
                        InsertProperty("minLengthText", subitemMinLengthText);
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
                if (subitemDefaultValue != null)
                {
                    InsertProperty("defaultValue", subitemDefaultValue);
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
                MessageBox.Show(this, exception.InnerException.Message, Resources.DeletionActionFailed);
            }
        }
        private void EditWizardStep_Click(Object sender, EventArgs e)
        {
            var node = JTokenNode;
            var newStep = new StepDefinition();

            var stepValidation = GetNodeTextByTag(JTokenNode, "invalid_next_disabled").Equals(true);
            var stepActivation = GetNodeTextByTag(JTokenNode, "activate") as string;
            var stepDeactivation = GetNodeTextByTag(JTokenNode, "deactivate") as string;
            var stepName = GetNodeTextByTag(JTokenNode, "step_title") as string;

            newStep.Title = stepName;
            newStep.Validation = stepValidation;
            newStep.Activation = stepActivation;
            newStep.Deactivation = stepDeactivation;

            newStep.ShowDialog(this);
            stepName = newStep.Title;

            if (string.IsNullOrEmpty(stepName))
                return;

            stepValidation = newStep.Validation;
            stepActivation = newStep.Activation;
            stepDeactivation = newStep.Deactivation;

            SetNodeTextByTag(JTokenNode, "invalid_next_disabled", stepValidation, false);
            SetNodeTextByTag(JTokenNode, "activate", stepActivation, "");
            SetNodeTextByTag(JTokenNode, "deactivate", stepDeactivation, "");
            SetNodeTextByTag(JTokenNode, "step_title", stepName, "");


            JTokenNode.TreeView.BeginUpdate();
            var parent = node.Parent as JTokenTreeNode;
            parent.Nodes.Remove(node);
            parent.Nodes.Add(node);
            JTokenNode = node;
            JTokenNode.TreeView.SelectedNode = node;
            JTokenNode.TreeView.EndUpdate();
        }

        private void RemoveWizardItem_Click(Object sender, EventArgs e)
        {
            try
            {
                JTokenNode.EditDelete();
            }
            catch (JTokenTreeNodeDeleteException exception)
            {
                MessageBox.Show(this, exception.InnerException.Message, Resources.DeletionActionFailed);
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
                MessageBox.Show(this, exception.InnerException.Message, Resources.DeletionActionFailed);
            }
        }

        private void EditValidatorSubitem_Click(Object sender, EventArgs e)
        {

        }

        private void EditWizardSubitem_Click(Object sender, EventArgs e)
        {
            var node = JTokenNode;
            var type = "";
            try
            {
                type = node.Parent.Parent.Parent.FirstNode.FirstNode.Text;
            }
            catch
            {
                MessageBox.Show(this, "Type of Item cannot be found. Did you modify the json? Possibly recreate this item!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (!string.IsNullOrEmpty(type))
            {
                var subitemKey = GetNodeTextByTag(JTokenNode, "key") as string;
                var subitemDescription = GetNodeTextByTag(JTokenNode, "desc") as string;
                var subitemDefaultValue = GetNodeTextByTag(JTokenNode, "defaultValue"); // !!! value of checkbox and options are possibly strings : "true"/"false" (TBC!)
                var subitemEmptyValue = GetNodeTextByTag(JTokenNode, "emptyValue") as string;
                var subitemWidth = GetNodeTextByTag(JTokenNode, "width") as string;
                var subitemHeight = GetNodeTextByTag(JTokenNode, "height") as string;
                var subitemDisabled = GetNodeTextByTag(JTokenNode, "disabled").Equals(true); //empty = false by default
                var subitemHidden = GetNodeTextByTag(JTokenNode, "hidden").Equals(true);
                var subitemPreventMark = GetNodeTextByTag(JTokenNode, "preventMark").Equals(true); //empty = false by default
                var subitemEditable = GetNodeTextByTag(JTokenNode, "editable").Equals(true);
                var subitemAutoSelect = GetNodeTextByTag(JTokenNode, "autoSelect").Equals(true);
                var subitemForceSelection = GetNodeTextByTag(JTokenNode, "forceSelection").Equals(true);
                var subitemRoot = GetNodeTextByTag(JTokenNode, "root") as string;
                var subitemApi = GetNodeTextByTag(JTokenNode, "api") as string;
                var subitemValueField = GetNodeTextByTag(JTokenNode, "valuefield") as string;
                var subitemDisplayField = GetNodeTextByTag(JTokenNode, "displayfield") as string;
                var subitemStaticCombo = GetNodeTextByTag(JTokenNode, "mode").Equals("local");
                var subitemIdProperty = GetNodeTextByTag(JTokenNode, "idProperty") as string;
                var subitemValueFieldUnique = (subitemIdProperty == subitemValueField);
                var subitemDisplayFieldUnique = (subitemIdProperty == subitemDisplayField);
                List<NameValue> subitemBaseParams = null;
                JTokenTreeNode store = null; ;
                if (type == "combobox")
                {
                    if (subitemStaticCombo)
                    {
                        store = GetNodeByTag(JTokenNode, "store") as JTokenTreeNode;
                        var data = GetNodeByTag(store, "data");
                        subitemBaseParams = new List<NameValue>();
                        foreach (JTokenTreeNode item in data.Nodes)
                        {
                            subitemBaseParams.Add(new NameValue(((JContainer)item.JTokenTag).First.ToString(), ((JContainer)item.JTokenTag).Last.ToString()));
                        }
                    }
                    else
                    {
                        store = GetNodeByTag(JTokenNode, "api_store") as JTokenTreeNode;
                        var data = GetNodeByTag((JTokenTreeNode)store, "baseParams");
                        subitemBaseParams = new List<NameValue>();
                        foreach (JTokenTreeNode item in data.Nodes)
                        {
                            subitemBaseParams.Add(new NameValue(((JProperty)item.JTokenTag).Value.ToString(), ((JProperty)item.JTokenTag).Name.ToString()));
                        }
                    }
                }

                string subitemRegex = null;
                var validator = GetNodeByTag(JTokenNode, "validator") as JTokenTreeNode;
                var regex = GetNodeByTag(validator, "regex") as JTokenTreeNode;
                subitemRegex = GetNodeTextByTag(regex, "expr") as string;
                var subitemInvalidText = GetNodeTextByTag(regex, "errorText") as string;
                var subitemAllowBlank = GetNodeTextByTag(validator, "allowBlank", false).Equals(true);
                var subitemMinLength = GetNodeTextByTag(validator, "minLength") as string;
                var subitemMaxLength = GetNodeTextByTag(validator, "maxLength") as string;
                var subitemvType = GetNodeTextByTag(validator, "vtype") as string;
                var subitemFn = GetNodeTextByTag(validator, "fn") as string;

                if (string.IsNullOrEmpty(subitemInvalidText))
                    subitemInvalidText = GetNodeTextByTag(JTokenNode, "invalidText") as string;

                var subitemBlankText = GetNodeTextByTag(JTokenNode, "blankText") as string;
                var subitemGrow = GetNodeTextByTag(JTokenNode, "grow").Equals(true);
                var subitemGrowMax = GetNodeTextByTag(JTokenNode, "growMax") as string;
                var subitemGrowMin = GetNodeTextByTag(JTokenNode, "growMin") as string;
                var subitemHtmlEncode = GetNodeTextByTag(JTokenNode, "htmlEncode").Equals(true);
                var subitemMaxLengthText = GetNodeTextByTag(JTokenNode, "maxLengthText") as string;
                var subitemMinLengthText = GetNodeTextByTag(JTokenNode, "minLengthText") as string;

                var newSubitem = new SubitemDefinition(type);
                newSubitem.Key = subitemKey;
                newSubitem.Description = subitemDescription;
                newSubitem.DefaultValue = subitemDefaultValue;
                newSubitem.EmptyText = subitemEmptyValue;
                newSubitem.InvalidText = subitemInvalidText;
                newSubitem.Width = subitemWidth;
                newSubitem.Height = subitemHeight;
                newSubitem.Disable = subitemDisabled;
                newSubitem.Hidden = subitemHidden;
                newSubitem.PreventMark = subitemPreventMark;
                newSubitem.BaseParams = subitemBaseParams;
                newSubitem.Root = subitemRoot;
                newSubitem.Api = subitemApi;
                newSubitem.ValueField = subitemValueField;
                newSubitem.DisplayField = subitemDisplayField;
                newSubitem.StaticCombo = subitemStaticCombo;
                newSubitem.ValueFieldIsUnique = subitemValueFieldUnique;
                newSubitem.DisplayFieldIsUnique = subitemDisplayFieldUnique;
                newSubitem.Editable = subitemEditable;
                newSubitem.AutoSelect = subitemAutoSelect;
                newSubitem.ForceSelection = subitemForceSelection;


                newSubitem.AllowBlank = subitemAllowBlank;
                newSubitem.MinLength = subitemMinLength;
                newSubitem.MaxLength = subitemMaxLength;
                newSubitem.vType = subitemvType;
                newSubitem.Regex = subitemRegex;
                newSubitem.Fn = subitemFn;
                newSubitem.BlankText = subitemBlankText;
                newSubitem.Grow = subitemGrow;
                newSubitem.GrowMax = subitemGrowMax;
                newSubitem.GrowMin = subitemGrowMin;
                newSubitem.HtmlEncode = subitemHtmlEncode;
                newSubitem.MaxLengthText = subitemMaxLengthText;
                newSubitem.MinLengthText = subitemMinLengthText;

                newSubitem.ShowDialog(this);
                subitemKey = newSubitem.Key;

                if (string.IsNullOrEmpty(subitemKey))
                    return;

                subitemDescription = newSubitem.Description;
                subitemDefaultValue = newSubitem.DefaultValue;
                subitemEmptyValue = newSubitem.EmptyText;
                subitemInvalidText = newSubitem.InvalidText;
                subitemWidth = newSubitem.Width;
                subitemHeight = newSubitem.Height;
                subitemDisabled = newSubitem.Disable;
                subitemHidden = newSubitem.Hidden;
                subitemPreventMark = newSubitem.PreventMark;
                subitemBaseParams = newSubitem.BaseParams;
                subitemRoot = newSubitem.Root;
                subitemApi = newSubitem.Api;
                subitemValueField = newSubitem.ValueField;
                subitemDisplayField = newSubitem.DisplayField;
                subitemStaticCombo = newSubitem.StaticCombo;
                subitemValueFieldUnique = newSubitem.ValueFieldIsUnique;
                subitemDisplayFieldUnique = newSubitem.DisplayFieldIsUnique;
                subitemEditable = newSubitem.Editable;
                subitemAutoSelect = newSubitem.AutoSelect;
                subitemForceSelection = newSubitem.ForceSelection;

                subitemAllowBlank = newSubitem.AllowBlank;
                subitemMinLength = newSubitem.MinLength;
                subitemMaxLength = newSubitem.MaxLength;
                subitemvType = newSubitem.vType;
                subitemRegex = newSubitem.Regex;
                subitemFn = newSubitem.Fn;
                subitemBlankText = newSubitem.BlankText;
                subitemGrow = newSubitem.Grow;
                subitemGrowMax = newSubitem.GrowMax;
                subitemGrowMin = newSubitem.GrowMin;
                subitemHtmlEncode = newSubitem.HtmlEncode;
                subitemMaxLengthText = newSubitem.MaxLengthText;
                subitemMinLengthText = newSubitem.MinLengthText;

                if (store != null) //subitemBaseParams.Count > 0)
                {
                    SetNodeTextByTag(JTokenNode, "valuefield", subitemValueField, "");
                    SetNodeTextByTag(JTokenNode, "displayfield", subitemDisplayField, "");
                    SetNodeTextByTag(JTokenNode, "editable", subitemEditable, true);
                    SetNodeTextByTag(JTokenNode, "autoSelect", subitemAutoSelect, true);
                    SetNodeTextByTag(JTokenNode, "forceSelection", subitemForceSelection, false);

                    var comboNode = JTokenNode;
                    if (store != null)
                        ((JPropertyTreeNode)store.Parent).AfterJsonTextChange("");

                    if (subitemStaticCombo)
                    {
                        // Static Combo is filled
                        JTokenNode = (JTokenTreeNode)InsertProperty("store", new JObject()).FirstNode;
                        InsertProperty("xtype", "arraystore");
                        store = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("fields", new JArray()).FirstNode;
                        if (!string.IsNullOrEmpty(subitemValueField))
                            InsertJToken(new JValue(subitemValueField));
                        if (!string.IsNullOrEmpty(subitemDisplayField))
                            InsertJToken(new JValue(subitemDisplayField));
                        JTokenNode = store;
                        JTokenNode = (JTokenTreeNode)InsertProperty("data", new JArray()).FirstNode;
                        foreach (var item in subitemBaseParams)
                        {
                            InsertJToken(new JArray(new JValue(item.value), new JValue(item.name)));
                        }
                    }
                    else
                    {
                        // Dynamic Combo is filled
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
                        store = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("fields", new JArray()).FirstNode;
                        if (!string.IsNullOrEmpty(subitemValueField))
                            InsertJToken(new JValue(subitemValueField));
                        if (!string.IsNullOrEmpty(subitemDisplayField))
                            InsertJToken(new JValue(subitemDisplayField));
                        JTokenNode = store;
                        JTokenNode = (JTokenTreeNode)InsertProperty("baseParams", new JObject()).FirstNode;
                        foreach (var item in subitemBaseParams)
                        {
                            InsertProperty(item.name, item.value);
                        }
                    }
                    JTokenNode = comboNode;
                }

                var selectedNode = JTokenNode;
                if (validator != null)
                    ((JPropertyTreeNode)validator.Parent).AfterJsonTextChange("");
                //if (type == "textfield" || type == "password")
                if (!string.IsNullOrEmpty(subitemRegex) ||
                    subitemAllowBlank ||
                    !string.IsNullOrEmpty(subitemMinLength) ||
                    !string.IsNullOrEmpty(subitemMaxLength) ||
                    !string.IsNullOrEmpty(subitemvType) ||
                    !string.IsNullOrEmpty(subitemFn))
                {
                    JTokenNode = (JTokenTreeNode)InsertProperty("validator", new JObject()).FirstNode;
                    if (!string.IsNullOrEmpty(subitemRegex))
                    {
                        validator = JTokenNode;
                        JTokenNode = (JTokenTreeNode)InsertProperty("regex", new JObject()).FirstNode;
                        InsertProperty("expr", subitemRegex);
                        InsertProperty("errorText", subitemInvalidText);
                        JTokenNode = validator;
                    }

                    InsertProperty("allowBlank", subitemAllowBlank);
                    if (!string.IsNullOrEmpty(subitemMinLength))
                        InsertProperty("minLength", subitemMinLength);
                    if (!string.IsNullOrEmpty(subitemMaxLength))
                        InsertProperty("maxLength", subitemMaxLength);
                    if (!string.IsNullOrEmpty(subitemvType))
                        InsertProperty("vtype", subitemvType);
                    if (!string.IsNullOrEmpty(subitemFn))
                        InsertProperty("fn", subitemFn);

                    JTokenNode = selectedNode;
                }

                if (string.IsNullOrEmpty(subitemRegex))
                    SetNodeTextByTag(JTokenNode, "invalidText", subitemInvalidText, "");

                SetNodeTextByTag(JTokenNode, "blankText", subitemBlankText, "");
                SetNodeTextByTag(JTokenNode, "grow", subitemGrow, false);
                SetNodeTextByTag(JTokenNode, "growMax", subitemGrowMax, "");
                SetNodeTextByTag(JTokenNode, "growMin", subitemGrowMin, "");
                SetNodeTextByTag(JTokenNode, "htmlEncode", subitemHtmlEncode, false);
                SetNodeTextByTag(JTokenNode, "maxLengthText", subitemMaxLengthText, "");
                SetNodeTextByTag(JTokenNode, "minLengthText", subitemMinLengthText, "");

                SetNodeTextByTag(JTokenNode, "disabled", subitemDisabled, false);
                SetNodeTextByTag(JTokenNode, "hidden", subitemHidden, false);
                SetNodeTextByTag(JTokenNode, "preventMark", subitemPreventMark, false);
                SetNodeTextByTag(JTokenNode, "width", subitemWidth, "");
                SetNodeTextByTag(JTokenNode, "height", subitemHeight, "");
                SetNodeTextByTag(JTokenNode, "desc", subitemDescription, "");
                SetNodeTextByTag(JTokenNode, "defaultValue", subitemDefaultValue, "");
                SetNodeTextByTag(JTokenNode, "key", subitemKey, null);
                //SetNodeTextByTag(JTokenNode, "mode", subitemStaticCombo == true ? "local" : "remote", ""); //No support to change this !

                JTokenNode.TreeView.BeginUpdate();
                var parent = node.Parent as JTokenTreeNode;
                parent.Nodes.Remove(node);
                parent.Nodes.Add(node);
                JTokenNode = node;
                JTokenNode.TreeView.SelectedNode = node;
                JTokenNode.TreeView.EndUpdate();
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
                MessageBox.Show(this, exception.InnerException.Message, Resources.DeletionActionFailed);
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
                MessageBox.Show(this, exception.InnerException.Message, Resources.PasteActionFailed);
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
                MessageBox.Show(this, exception.InnerException.Message, Resources.PasteActionFailed);
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
                MessageBox.Show(this, exception.InnerException.Message, Resources.PasteActionFailed);
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
