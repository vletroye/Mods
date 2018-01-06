using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace BeatificaBytes.Synology.Mods
{
    public partial class ArchAndModels : Form
    {
        // constants used to hide a checkbox
        public const int TVIF_STATE = 0x8;
        public const int TVIS_STATEIMAGEMASK = 0xF000;
        public const int TV_FIRST = 0x1100;
        public const int TVM_SETITEM = TV_FIRST + 63;

        [DllImport("user32.dll")]
        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam,
        IntPtr lParam);

        // struct used to set node properties
        public struct TVITEM
        {
            public int mask;
            public IntPtr hItem;
            public int state;
            public int stateMask;
            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpszText;
            public int cchTextMax;
            public int iImage;
            public int iSelectedImage;
            public int cChildren;
            public IntPtr lParam;
        }

        private string[] selectedArchs;
        private string[] selectedModels;

        public ArchAndModels(string archs, string models)
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(archs)) archs = "";
            this.selectedArchs = archs.Split(' ');
            if (models != null)
            {
                var list = new List<String>();
                foreach (string item in models.Split(' '))
                {
                    list.Add(item.Split('_').Last());
                }
                this.selectedModels = list.ToArray();
            }
            treeViewArch.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeViewArch.DrawNode += new DrawTreeNodeEventHandler(tree_DrawNode);
        }
        void tree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.Level == 0)
            {
                HideCheckBox(e.Node);
                e.DrawDefault = true;
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(Color.White),
                    new Rectangle(e.Bounds.Left + 1, e.Bounds.Top, this.ClientSize.Width - e.Bounds.Left, e.Bounds.Height));
                e.Graphics.DrawString(e.Node.Text, e.Node.TreeView.Font,
                   Brushes.Black, e.Node.Bounds.X, e.Node.Bounds.Y);
            }
        }

        private void HideCheckBox(TreeNode node)
        {
            TVITEM tvi = new TVITEM();
            tvi.hItem = node.Handle;
            tvi.mask = TVIF_STATE;
            tvi.stateMask = TVIS_STATEIMAGEMASK;
            tvi.state = 0;
            IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvi));
            Marshal.StructureToPtr(tvi, lparam, false);
            SendMessage(node.TreeView.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
        }

        private void ArchAndModels_Load(object sender, EventArgs e)
        {
            string yaml = File.ReadAllText(@"Resources\synology_models.yaml");
            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<Dictionary<string, Dictionary<string, List<String>>>>(new StringReader(yaml));
            foreach (var item in result)
            {
                var plateform = treeViewArch.Nodes.Add(item.Key);
                foreach (var archItem in item.Value)
                {
                    var arch = plateform.Nodes.Add(archItem.Key);
                    if (selectedArchs.Contains(archItem.Key, StringComparer.InvariantCultureIgnoreCase))
                        arch.Checked = true;
                    if (archItem.Value != null && this.selectedModels != null)
                    {
                        foreach (var modelItem in archItem.Value)
                        {
                            var model = arch.Nodes.Add(modelItem);
                            if (selectedModels.Contains(modelItem, StringComparer.InvariantCultureIgnoreCase))
                                model.Checked = true;
                        }
                    }
                }
                plateform.ExpandAll();
            }
        }
        public string archs
        {
            get
            {
                var list = new List<string>();
                foreach (TreeNode platform in treeViewArch.Nodes)
                {
                    foreach (TreeNode arch in platform.Nodes)
                    {
                        if (arch.Checked)
                            list.Add(arch.Text);
                    }
                }
                return string.Join(" ", list);
            }
        }
        public string models
        {
            get
            {
                var list = new List<string>();
                foreach (TreeNode platform in treeViewArch.Nodes)
                {
                    foreach (TreeNode arch in platform.Nodes)
                    {
                        foreach (TreeNode model in arch.Nodes)
                        {
                            if (model.Checked || arch.Checked)
                                list.Add(string.Format("synology_{0}_{1}", arch.Text, model.Text));
                        }
                    }
                }
                return string.Join(" ", list);
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void treeViewArch_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Location.X >= e.Node.Bounds.Left) //clicking on the checkbox with tick it and trigger next the nodeclick :(
                e.Node.Checked = !e.Node.Checked;
        }
    }
}
