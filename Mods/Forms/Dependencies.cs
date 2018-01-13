using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods
{
    public partial class Dependencies : Form
    {
        SortedDictionary<string, string> info;

        public Dependencies(SortedDictionary<string, string> info)
        {
            InitializeComponent();
            this.info = info;

            AddItem("arch", "arch", "List the CPU architectures which can be used to install the package. Note: 'noarch' means the package can be installed and work in any platform.For example, the package is written in PHP or shell script.Arch values are separated with a space");
            AddItem("exclude_arch", "arch", "List the CPU architectures where the package can't be used to install the package. Note: Be careful to use this exclude_arch field.If the package has different exclude_arch value in the different versions, the end user can install the package in the specific version without some arch values of exclude_arch. Arch values are separated with a space.");
            AddItem("model", "model", "List of models on which packages can be installed in spesific models. It is organized by Synology string, architecture and model name. Models are separated with a space.");
            AddItem("install_dep_packages", "pkg", "Before a package is installed or upgraded, these packages must be installed first.In addition, the order of starting or stopping packages is also dependent on it.The format consists of a package name.If more than one dependent packages are required, the package name of the package(s) will be separated with a colon, e.g.install_dep_packages = packageA.If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g.install_dep_packages = packageA>2.2.2:packageB.\r\nNote: >= and <= operator only supported in DSM 4.2 or newer. Don’t use <= and >= if a package can be installed in DSM 4.1 or older because it cannot be compared correctly. Instead, the package version should be set lower or higher. Note: Each package name is separated with a colon.");
            AddItem("install_conflict_packages", "pkg", "Before your package is installed or upgraded, these conflict packages cannot be installed. The format consists of a package name, e.g. install_conflict_packages=packageA. If more than one conflict packages are required with the format, the name of the package(s) will be separated with a colon, e.g. install_conflict_packages=packageA: packageB. If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g. install_conflict_packages=packageA > 2.2.2:packageB.\r\nNote: >= and <= operator only supported in DSM 4.2 or newer. Do not use <= and >= if a package can be installed in DSM 4.1 because it can’t be compared correctly. Instead, the package version should be set lower or higher. Note: Each package name is separated with a colon.");
            AddItem("install_break_packages", "pkg", "After your package is installed or upgraded, these to-be-broken packages will be stopped and remain broken during the existence of your package. The format consists of a package name, e.g. install_break_packages=packageA. If more than one to-be-broken packages are required with the format, the name of the package(s) will be separated with a colon, e.g. install_break_packages=packageA: packageB. If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g. install_break_packages=packageA > 2.2.2:packageB. Note: Each package name is separated with a colon.");
            AddItem("install_replace_packages", "pkg", "After your package is installed or upgraded, these to-be-replaced packages will be removed. The format consists of a package name, e.g. install_replace_packages=packageA. If more than one to-be-replaced packages are required with the format, the name of the package(s) will be separated with a colon, e.g. install_replace_packages=packageA: packageB. If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g. install_replace_packages=packageA > 2.2.2:packageB. Note: Each package name is separated with a colon.");
            AddItem("instuninst_restart_services", "srvc", "Reload services after installing, upgrading and uninstalling the package. Note: If the service is not enabled or started by the end-user, services won't be reloaded. If the install_reboot is set to “yes”, this value is ignored in the installation process.\r\nValue: DSM 4.3 or older: apache - sys, apache - web, mdns, samba, db, applenetwork, cron, nfs, firewall\r\nDSM 5.0 ~DSM 5.2: apache - sys, apache - web, mdns, samba, applenetwork, cron, nfs, firewall\r\nDSM 6.0: nginx, mdns, samba, applenetwork, cron, nfs, firewall\r\nNote: Each service is separated with a space.");
            AddItem("startstop_restart_services", "srvc", "Reload services after starting and stopping the package. Note: If the service is not enabled or started by the end-user, services won't be reloaded. If startable is set to “no”, the value is ignored.\r\nValue: DSM 4.3 or older: apache - sys, apache - web, mdns, samba, db, applenetwork, cron, nfs, firewall\r\nDSM 5.0 ~DSM 5.2: apache - sys, apache - web, mdns, samba, applenetwork, cron, nfs, firewall\r\nDSM 6.0: nginx, mdns, samba, applenetwork, cron, nfs, firewall\r\nNote: Each service is separated with a space.");
            AddItem("install_dep_services", "srvc", "Before the package is installed or upgraded, these services must be started or enabled by the end-user.\r\nValue: DSM 4.3 or older: apache - sys, apache - web, mdns, samba, db, applenetwork, cron, nfs, firewall\r\nDSM 5.0 ~DSM 5.2: apache - sys, apache - web, mdns, samba, applenetwork, cron, nfs, firewall\r\nDSM 6.0: nginx, mdns, samba, applenetwork, cron, nfs, firewall\r\nNote: Each service is separated with a space.");
            AddItem("start_dep_services", "srvc", "Before the package is started, these services must be started or enabled by the end-user. If startable is set to “no”, this value is ignored.\r\nValue: DSM 4.3 or older: apache - sys, apache - web, mdns, samba, db, applenetwork, cron, nfs, firewall\r\nDSM 5.0 ~DSM 5.2: apache - sys, apache - web, mdns, samba, applenetwork, cron, nfs, firewall\r\nDSM 6.0: nginx, mdns, samba, applenetwork, cron, nfs, firewall\r\nNote: Each service is separated with a space.");
        }

        public void RemoveSupported(List<string> info)
        {
            foreach (ListViewItem item in listViewDependencies.Items)
            {
                if (info.Contains(item.Text))
                {
                    info.Remove(item.Text);
                }
            }
        }

        private void Dependencies_Load(object sender, EventArgs e)
        {
            if (info != null)
            {
                foreach (ListViewItem item in listViewDependencies.Items)
                {
                    if (info.Keys.Contains(item.Text))
                    {
                        var data = info[item.Text];
                        item.SubItems.Add(data);
                    }
                    else
                    {
                        item.SubItems.Add("");
                    }
                }
            }
            else
            {
                this.Close();
            }
        }

        private void AddItem(string type, string sep, string tooltip)
        {
            var item = listViewDependencies.Items.Add(type);
            item.ToolTipText = tooltip;
            item.Tag = sep;
        }

        private void listViewDependencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDependencies.SelectedItems.Count > 0)
            {
                var item = listViewDependencies.SelectedItems[0];
                textBoxDependencies.Text = item.SubItems[1].Text;
                labelToolTip.Text = item.ToolTipText;
            }
            else
            {
                textBoxDependencies.Text = "";
            }
        }

        private void listViewDependencies_Resize(object sender, EventArgs e)
        {
            listViewDependencies.Columns[1].Width = listViewDependencies.Width - 160;
        }
        private void textBoxDependencies_DoubleClick(object sender, EventArgs e)
        {
            if (listViewDependencies.SelectedItems.Count > 0)
            {
                var item = listViewDependencies.SelectedItems[0];
                OpenEditor(item);
            }
        }

        private void OpenEditor(ListViewItem item)
        {
            switch (item.Tag.ToString())
            {
                case "arch":
                    var archForm = new ArchAndModels(textBoxDependencies.Text, null);
                    if (archForm.ShowDialog(this) == DialogResult.OK)
                    {
                        if (archForm.archs.Contains("noarch"))
                            textBoxDependencies.Text = "noarch";
                        else
                            textBoxDependencies.Text = archForm.archs;
                    }
                    break;
                case "model":
                    var modelForm = new ArchAndModels(null, textBoxDependencies.Text);
                    if (modelForm.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxDependencies.Text = modelForm.models;
                    }
                    break;
                case "pkg":
                    break;
                case "srvc":
                    var modelSrvc = new SynoServices(textBoxDependencies.Text);
                    if (modelSrvc.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxDependencies.Text = modelSrvc.services;
                    }
                    break;
            }
        }

        private void listViewDependencies_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listViewDependencies.SelectedItems.Count > 0)
            {
                var item = listViewDependencies.SelectedItems[0];
                OpenEditor(item);
            }
        }

        private void textBoxDependencies_TextChanged(object sender, EventArgs e)
        {
            if (listViewDependencies.SelectedItems.Count > 0)
            {
                var item = listViewDependencies.SelectedItems[0];
                if (item.Text == "arch" && string.IsNullOrEmpty(textBoxDependencies.Text))
                    textBoxDependencies.Text = "noarch";

                item.SubItems[1].Text = textBoxDependencies.Text;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (info != null)
            {
                foreach (ListViewItem item in listViewDependencies.Items)
                {
                    var key = item.SubItems[0].Text;
                    var data = item.SubItems[1].Text;
                    if (info.Keys.Contains(key))
                    {
                        if (string.IsNullOrEmpty(data))
                            info.Remove(key);
                        else
                            info[key] = data;
                    }
                    else if (!string.IsNullOrEmpty(data))
                    {
                        info.Add(key, data);
                    }
                }
            }
            else
            {
                this.Close();
            }
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
