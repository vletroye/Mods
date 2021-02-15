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
    public partial class SynoServices : Form
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

        private string[] selectedServices;

        public SynoServices(string services, string defaults)
        {
            InitializeComponent();

            if (services == null) services = "";
            this.selectedServices = services.Split(' ');

            string yaml = File.ReadAllText(defaults);
            var deserializer = new Deserializer();
            var result = deserializer.Deserialize<Dictionary<string, List<String>>>(new StringReader(yaml));
            foreach (var item in result)
            {
                var firmware = treeViewServices.Nodes.Add(item.Key);
                foreach (var service in item.Value)
                {
                    var srvc = firmware.Nodes.Add(service);
                    if (selectedServices.Contains(service, StringComparer.InvariantCultureIgnoreCase))
                        srvc.Checked = true;
                }
                firmware.ExpandAll();
            }

            treeViewServices.DrawMode = TreeViewDrawMode.OwnerDrawText;
            treeViewServices.DrawNode += new DrawTreeNodeEventHandler(tree_DrawNode);
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

        public string services
        {
            get
            {
                var list = new List<string>();
                foreach (TreeNode platform in treeViewServices.Nodes)
                {
                    foreach (TreeNode srvc in platform.Nodes)
                    {
                        if (srvc.Checked && !list.Contains(srvc.Text))
                            list.Add(srvc.Text);
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

        private void treeViewServices_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Location.X >= e.Node.Bounds.Left) //clicking on the checkbox with tick it and trigger next the nodeclick :(
                e.Node.Checked = !e.Node.Checked;
        }
    }
}
