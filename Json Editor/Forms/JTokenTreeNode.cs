using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Windows.Forms;
using ZTn.Json.Editor.Generic;

namespace ZTn.Json.Editor.Forms
{
    /// <summary>
    /// Specialized <see cref="TreeNode"/> for handling <see cref="JToken"/> representation in a <see cref="TreeView"/>.
    /// </summary>
    public abstract class JTokenTreeNode : TreeNode, IJsonTreeNode
    {
        #region >> Properties

        public JToken JTokenTag
        {
            get { return Tag as JToken; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JTokenTreeNode"/> class.
        /// </summary>
        /// <param name="jToken"></param>
        protected JTokenTreeNode(JToken jToken)
        {
            Tag = jToken;
            ContextMenuStrip = SingleInstanceProvider<JTokenContextMenuStrip>.Value;

            AfterCollapse();
        }

        #endregion

        #region >> IJsonTreeNode

        /// <inheritdoc />
        /// <remarks>Default simple implementation to be overriden if needed.</remarks>
        public virtual void AfterCollapse()
        {
            Text = Tag.ToString();
        }

        /// <inheritdoc />
        /// <remarks>Default simple implementation to be overriden if needed.</remarks>
        public virtual void AfterExpand()
        {
            Text = Tag.ToString();
        }

        /// <inheritdoc />
        /// <remarks>Default simple implementation to be overriden if needed.</remarks>
        public virtual TreeNode AfterJsonTextChange(string jsonString)
        {
            var jTokenRoot = new JTokenRoot(jsonString);
            TreeNode node = null;
            if (jTokenRoot.JTokenValue != null)
            {
                if (JTokenTag.Parent != null)
                {
                    JTokenTag.Replace(jTokenRoot.JTokenValue);
                }

                node = InsertInParent(JsonTreeNodeFactory.Create(jTokenRoot.JTokenValue), true);
            }
            return node;
        }

        #endregion

        /// <summary>
        /// Remove JTokenTag from its parent if <paramref name="jsonString"/> is empty or null.
        /// </summary>
        /// <param name="jsonString"></param>
        /// <returns><value>true</value> if <paramref name="jsonString"/> is empty or null.</returns>
        protected bool CheckEmptyJsonString(string jsonString)
        {
            if (String.IsNullOrWhiteSpace(jsonString))
            {
                JTokenTag.Remove();
                CleanParentTreeNode();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove <see cref="JTokenTreeNode"/>s from the parent of current <see cref="TreeNode"/> having a detached JTokenTag property.
        /// </summary>
        /// <returns>First available <see cref="TreeNode"/> or null if the parent has no children.</returns>
        public TreeNode CleanParentTreeNode()
        {
            return ((JTokenTreeNode)Parent).CleanTreeNode();
        }

        /// <summary>
        /// Remove <see cref="JTokenTreeNode"/>s from current <see cref="TreeNode"/> having a detached JTokenTag property.
        /// </summary>
        /// <returns>First available <see cref="TreeNode"/> or null if the parent has no children.</returns>
        public TreeNode CleanTreeNode()
        {
            // ToList() is mandatory before ForEach because working list will be modified
            Nodes
                .OfType<JTokenTreeNode>()
                .Where(n => n != null && n.JTokenTag.Parent == null)
                .ToList()
                .ForEach(n => Nodes.Remove(n));

            return Nodes
                .Cast<TreeNode>()
                .FirstOrDefault();
        }

        /// <summary>
        /// Insert a <paramref name="newNode"/> in current parent nodes.
        /// </summary>
        /// <param name="newNode"></param>
        /// <param name="insertBefore">
        /// Set to <c>true</c> to insert <paramref name="newNode"/> before current node.
        /// Set to <c>false</c> to insert <paramref name="newNode"/> after current node.
        /// </param>
        /// <returns></returns>
        public TreeNode InsertInParent(TreeNode newNode, bool insertBefore)
        {
            if (newNode == this)
            {
                return newNode;
            }

            var treeNodeCollection = Parent != null ? Parent.Nodes : TreeView.Nodes;

            if (Parent != null)
            {
                var nodeIndex = treeNodeCollection.IndexOf(this);

                if (insertBefore)
                {
                    treeNodeCollection.Insert(nodeIndex, newNode);
                }
                else
                {
                    treeNodeCollection.Insert(nodeIndex + 1, newNode);
                }

                CleanParentTreeNode();
            }
            else
            {
                treeNodeCollection.Clear();
                treeNodeCollection.Insert(0, newNode);
            }

            if (IsExpanded)
            {
                newNode.Expand();
            }
            else
            {
                newNode.Collapse();
            }

            return newNode;
        }

        /// <summary>
        /// Insert a <paramref name="newNode"/> in current node.
        /// </summary>
        /// <param name="newNode"></param>
        /// <returns></returns>
        public TreeNode InsertInCurrent(TreeNode newNode)
        {
            if (newNode == this)
            {
                return newNode;
            }

            Nodes.Insert(0, newNode);

            CleanTreeNode();

            Expand();

            return newNode;
        }
    }
}
