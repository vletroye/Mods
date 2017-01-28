using Newtonsoft.Json.Linq;
using System.Windows.Forms;
using ZTn.Json.Editor.Generic;

namespace ZTn.Json.Editor.Forms
{
    /// <summary>
    /// Specialized <see cref="TreeNode"/> for handling <see cref="JValue"/> representation in a <see cref="TreeView"/>.
    /// </summary>
    sealed class JValueTreeNode : JTokenTreeNode
    {
        #region >> Properties

        public JValue JValueTag
        {
            get { return Tag as JValue; }
        }

        #endregion

        #region >> Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="JValueTreeNode"/> class.
        /// </summary>
        /// <param name="jValue"></param>
        public JValueTreeNode(JToken jValue)
            : base(jValue)
        {
            ContextMenuStrip = SingleInstanceProvider<JValueContextMenuStrip>.Value;
        }

        #endregion
    }
}
