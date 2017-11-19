using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace BeatificaBytes.Synology.Mods
{
    public partial class ArchAndModels : Form
    {
        private string[] selectedArchs;
        private string[] selectedModels;

        public ArchAndModels(string archs, string models)
        {
            InitializeComponent();
            if (archs == null) archs = "";
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
                            if (model.Checked)
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
    }
}
