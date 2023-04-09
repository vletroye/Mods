using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YamlDotNet.Serialization;

namespace BeatificaBytes.Synology.Mods
{
    public partial class Dependencies : Form
    {
        private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/synology_package/INFO_optional_fields.html"), "Details about Dependencies");
        private PackageINFO info;
        private SortedDictionary<string, List<string>> services = new SortedDictionary<string, List<string>>();
        private StringBuilder init = new StringBuilder();
        private List<string> service1;
        private List<string> service2;
        private bool ongoingchange = false;
        private Keys keypressed = Keys.None;

        public Dependencies(PackageINFO info)
        {
            InitializeComponent();
            this.info = info;


            int major = 0;
            int minor = 0;
            int build = 0;
            if (info != null)
            {
                Helper.GetFirmwareMajorMinor(info, out major, out minor, out build);
                var srvc = string.Format("DSM_{0}.x", major);
                var dep = string.Format("DSM_{0}.x", major);

                if (major == 4 && minor == 3)
                {
                    srvc = "DSM_4.3";
                    dep = "DSM_4.3";
                }
                else if (major < 4 || (major == 4 && minor <= 2))
                {
                    srvc = "DSM_4.3";
                    dep = "DSM_4.2";
                }

                service1 = new List<string>() { };
                service2 = new List<string>() { };

                string yaml = File.ReadAllText(@"Resources\synology_services.yaml");
                var deserializer = new Deserializer();
                var result = deserializer.Deserialize<Dictionary<string, List<String>>>(new StringReader(yaml));
                foreach (var item in result)
                {
                    if (item.Key == srvc)
                        foreach (var service in item.Value)
                        {
                            service1.Add(service);
                        }
                }

                yaml = File.ReadAllText(@"Resources\synology_dependencies.yaml");
                result = deserializer.Deserialize<Dictionary<string, List<String>>>(new StringReader(yaml));
                foreach (var item in result)
                {
                    if (item.Key == dep)
                        foreach (var service in item.Value)
                        {
                            service2.Add(service);
                        }
                }              
            }

            AddItem("arch", "arch", "List the CPU architectures which can be used to install the package. Note: 'noarch' means the package can be installed and work in any platform.For example, the package is written in PHP or shell script.Arch values are separated with a space", null);
            AddItem("exclude_arch", "arch", "List the CPU architectures where the package can't be used to install the package. Note: Be careful to use this exclude_arch field.If the package has different exclude_arch value in the different versions, the end user can install the package in the specific version without some arch values of exclude_arch. Arch values are separated with a space.", null);
            AddItem("model", "model", "List of models on which packages can be installed in spesific models. It is organized by Synology string, architecture and model name. Models are separated with a space.", null);
            AddItem("install_dep_packages", "pkg", "Before a package is installed or upgraded, these packages must be installed first.In addition, the order of starting or stopping packages is also dependent on it.The format consists of a package name.If more than one dependent packages are required, the package name of the package(s) will be separated with a colon, e.g.install_dep_packages = packageA: packageB.If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g.install_dep_packages = packageA>2.2.2.\r\nNote: >= and <= operator only supported in DSM 4.2 or newer. Don’t use <= and >= if a package can be installed in DSM 4.1 or older because it cannot be compared correctly. Instead, the package version should be set lower or higher. Note: Each package name is separated with a colon.", null);
            AddItem("install_conflict_packages", "pkg", "Before your package is installed or upgraded, these conflict packages cannot be installed. The format consists of a package name, e.g. install_conflict_packages=packageA. If more than one conflict packages are required with the format, the name of the package(s) will be separated with a colon, e.g. install_conflict_packages=packageA: packageB. If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g. install_conflict_packages=packageA > 2.2.2.\r\nNote: >= and <= operator only supported in DSM 4.2 or newer. Do not use <= and >= if a package can be installed in DSM 4.1 because it can’t be compared correctly. Instead, the package version should be set lower or higher. Note: Each package name is separated with a colon.", null);
            AddItem("install_break_packages", "pkg", "After your package is installed or upgraded, these to-be-broken packages will be stopped and remain broken during the existence of your package. The format consists of a package name, e.g. install_break_packages=packageA. If more than one to-be-broken packages are required with the format, the name of the package(s) will be separated with a colon, e.g. install_break_packages=packageA: packageB. If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g. install_break_packages=packageA > 2.2.2. Note: Each package name is separated with a colon.", null);
            AddItem("install_replace_packages", "pkg", "After your package is installed or upgraded, these to-be-replaced packages will be removed. The format consists of a package name, e.g. install_replace_packages=packageA. If more than one to-be-replaced packages are required with the format, the name of the package(s) will be separated with a colon, e.g. install_replace_packages=packageA: packageB. If a specific version range is required, package name will be followed by one of the special characters =, <, >, >=, <= and package version which is composed by number and periods, e.g. install_replace_packages=packageA > 2.2.2. Note: Each package name is separated with a colon.", null);
            AddItem("instuninst_restart_services", "srvc", "Reload services after installing, upgrading and uninstalling the package. Note: If the service is not enabled or started by the end-user, services won't be reloaded. If the install_reboot is set to “yes”, this value is ignored in the installation process.\r\nNote: Each service is separated with a space.", service1);
            AddItem("startstop_restart_services", "srvc", "Reload services after starting and stopping the package. Note: If the service is not enabled or started by the end-user, services won't be reloaded. If startable is set to “no”, the value is ignored.\r\nNote: Each service is separated with a space.", service1);
            AddItem("install_dep_services", "dep", "Before the package is installed or upgraded, these services must be started or enabled by the end-user.\r\nNote: Each service is separated with a space.", service2);
            AddItem("start_dep_services", "dep", "Before the package is started, these services must be started or enabled by the end-user. If startable is set to “no”, this value is ignored.\r\nNote: Each service is separated with a space.", service2);
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
                        init.Append(data + " ");
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

        private void AddItem(string type, string sep, string tooltip, List<string> values)
        {
            var item = listViewDependencies.Items.Add(type);
            item.ToolTipText = tooltip;
            item.Tag = sep;
            if (values != null) services.Add(type, values);
        }

        private void listViewDependencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewDependencies.SelectedItems.Count > 0)
            {
                var type = listViewDependencies.SelectedItems[0];
                textBoxDependencies.Text = type.SubItems[1].Text;
                labelToolTip.Text = type.ToolTipText;

                if (services.ContainsKey(type.Text))
                {
                    labelToolTip.Text = type.ToolTipText + "\r\nValues for " + info["os_min_ver"] + ": " + services[type.Text].Aggregate((i, j) => i + ", " + j);
                }
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
                    var modelSrvc = new SynoServices(textBoxDependencies.Text, @"Resources\synology_services.yaml");
                    if (modelSrvc.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxDependencies.Text = modelSrvc.services;
                    }
                    break;
                case "dep":
                    var modelDep = new SynoServices(textBoxDependencies.Text, @"Resources\synology_dependencies.yaml");
                    if (modelDep.ShowDialog(this) == DialogResult.OK)
                    {
                        textBoxDependencies.Text = modelDep.services;
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
            if (!ongoingchange)
            {
                ongoingchange = true;
                if (listViewDependencies.SelectedItems.Count > 0)
                {
                    var item = listViewDependencies.SelectedItems[0];
                    if (item.Text == "arch" && string.IsNullOrEmpty(textBoxDependencies.Text))
                        textBoxDependencies.Text = "noarch";

                    if (keypressed != Keys.Delete && keypressed != Keys.Back)
                    {
                        var texts = textBoxDependencies.Text.Split(' ');
                        if (services.ContainsKey(item.Text) && texts.Length > 0)
                        {
                            var text = texts[texts.Length - 1];
                            if (!string.IsNullOrEmpty(text))
                                foreach (var service in services[item.Text])
                                {
                                    if (service.StartsWith(text))
                                    {
                                        var newText = textBoxDependencies.Text.Substring(0, textBoxDependencies.Text.Length - text.Length);
                                        textBoxDependencies.Text = newText + service;
                                        textBoxDependencies.SelectionStart = newText.Length + text.Length;
                                        textBoxDependencies.SelectionLength = service.Length - text.Length;
                                        break;
                                    }
                                }
                        }
                    }

                    item.SubItems[1].Text = textBoxDependencies.Text.Trim();
                }
                ongoingchange = false;
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            bool dirty = false;

            if (info != null)
            {
                var edit = new StringBuilder();
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
                    if (!string.IsNullOrEmpty(data))
                        edit.Append(data + " ");
                }

                if (edit.ToString() != init.ToString()) dirty = true;
            }
            else
            {
                this.Close();
            }
            if (dirty)
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void Dependencies_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            var info = new ProcessStartInfo(help.Url.AbsoluteUri);
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void textBoxDependencies_KeyDown(object sender, KeyEventArgs e)
        {
            keypressed = e.KeyCode;
        }
    }
}
