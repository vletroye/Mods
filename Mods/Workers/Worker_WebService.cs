using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json.Linq;
using BeatificaBytes.Synology.Mods.Properties;

namespace BeatificaBytes.Synology.Mods.Forms
{
    public partial class Worker_WebService : Form
    {

        Dictionary<TabPage, HashSet<Control>> _tabControls = new Dictionary<TabPage, HashSet<Control>>();
        private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/resource_acquisition/web_service.html"), "Details about Web Service");
        private WebService _resource = null;

        private JToken _origwebservice;
        private JToken _webservice;

        public Worker_WebService(PackageResource resource)
        {
            if (resource == null)
                resource = new PackageResource();

            var webservice = resource.WebService;

            InitializeComponent();
            SetWebservice(webservice);

            editListViewPhpSettings.Columns.Add(columnHeaderPhpSettings);
            editListViewPhpSettings.Columns.Add(columnHeaderPhpValue);

            var comboSource = new Dictionary<int, String>();
            comboSource.Add(3, "PHP5.6");
            comboSource.Add(4, "PHP7.0");
            comboSource.Add(5, "PHP7.1");
            comboSource.Add(6, "PHP7.2");
            comboSource.Add(7, "PHP7.3");
            comboSource.Add(8, "PHP7.4");
            comboSource.Add(9, "PHP8.0");
            comboSource.Add(10, "PHP8.1");
            comboBoxPhpVersion.DataSource = new BindingSource(comboSource, null);
            comboBoxPhpVersion.DisplayMember = "Value";
            comboBoxPhpVersion.ValueMember = "Key";

            comboSource = new Dictionary<int, String>();
            comboSource.Add(1, "Apache 2.2");
            comboSource.Add(2, "Apache 2.4");
            comboBoxApacheVersion.DataSource = new BindingSource(comboSource, null);
            comboBoxApacheVersion.DisplayMember = "Value";
            comboBoxApacheVersion.ValueMember = "Key";

            Helper.LoadPhpExtensions(textBoxPhpSettings);
            foreach (var phpSetting in textBoxPhpSettings.AutoCompleteCustomSource)
            {
                checkedListBoxPhpExtensions.Items.Add(phpSetting);
            }

            AttachEventsToFields(this.tabPageServices.Controls);
            AttachEventsToFields(this.tabPagePortals.Controls);
            AttachEventsToFields(this.tabPageDirectory.Controls);

            RegisterToValidationEvents();

            SetServices(_resource);
            SetDirectory(_resource);
            SetPortals(_resource);
        }

        private void SetWebservice(WebService webservice)
        {
            _origwebservice = JToken.Parse(JsonConvert.SerializeObject(webservice));

            if (webservice != null)
            {
                _resource = webservice; ;

                // keep all default values for comparison
                //_webservice = JToken.Parse(JsonConvert.SerializeObject(_resource, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
                _webservice = JToken.Parse(JsonConvert.SerializeObject(_resource));

                var keys = Helper.GetLostNode(_origwebservice, _webservice);
                if (keys.Count > 0)
                {
                    var list = String.Join(",", keys);
                    if (MessageBox.Show(this, string.Format("Some settings from the original webservice are not supported: {0}." + System.Environment.NewLine + System.Environment.NewLine + "Do you want to continue ?", list), "Warning", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        if (CloseScript(DialogResult.Abort)) SafeClose();
                    }
                }
            }
            else
            {
                _resource = new WebService();
                _resource.services = new List<Service>();
                _resource.services.Add(new Service());
                _resource.services[0].php = new Php();
            }
        }

        public JToken Specification
        {
            get
            {
                return _webservice;
            }

            set
            {
                _webservice = value;
            }
        }

        private void RegisterToValidationEvents()
        {
            foreach (TabPage tab in this.tabControlWebService.TabPages)
            {
                var tabControlList = new HashSet<Control>();
                _tabControls[tab] = tabControlList;

                var controls = tab.Controls;
                RegisterControlValidation(tabControlList, controls);
            }
        }

        private void RegisterControlValidation(HashSet<Control> tabControlList, Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                var capturedControl = control; //this is necessary
                control.Validating += (sender, e) => tabControlList.Add(capturedControl);
                control.Validated += (sender, e) => tabControlList.Remove(capturedControl);

                if (control.Controls.Count > 0)
                    RegisterControlValidation(tabControlList, control.Controls);
            }
        }

        private void SetServices(WebService resource)
        {
            if (resource.services == null || resource.services.Count == 0)
            {
                resource.services = new List<Service>();
                resource.services.Add(new Service());
            }
            if (resource.pkg_dir_prepare != null && resource.pkg_dir_prepare.Count > 1)
            {
                MessageBox.Show(this, String.Format("There are {0} directory configured as resource for this package. Only one is supported by MODS.", resource.pkg_dir_prepare.Count), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            var service = resource.services[0];

            textBoxServiceName.Text = service.service;
            textBoxServiceDisplayName.Text = service.display_name;
            checkBoxSupportAlias.Checked = service.support_alias;
            checkBoxSupportServer.Checked = service.support_server;
            textBoxServiceIcon.Text = service.icon;
            comboBoxServiceType.SelectedItem = service.type;

            textBoxServiceRoot.Text = service.root;
            if (service.index != null) textBoxServiceIndex.Text = string.Join(",", service.index);


            switch (service.type)
            {
                case "static":
                    break;
                case "nginx_php":
                    if (service.connect_timeout != 0) textBoxConnectionTimeOut.Text = service.connect_timeout.ToString();
                    if (service.read_timeout != 0) textBoxReadTimeOut.Text = service.read_timeout.ToString();
                    if (service.send_timeout != 0) textBoxSendTimeOut.Text = service.send_timeout.ToString();
                    break;
                case "apache_php ":
                    if (service.connect_timeout != 0) textBoxConnectionTimeOut.Text = service.connect_timeout.ToString();
                    if (service.read_timeout != 0) textBoxReadTimeOut.Text = service.read_timeout.ToString();
                    if (service.send_timeout != 0) textBoxSendTimeOut.Text = service.send_timeout.ToString();
                    checkBoxApacheInterceptErrors.Checked = service.intercept_errors;
                    break;
            }

            if (service.php != null)
            {
                textBoxPhpProfileName.Text = service.php.profile_name;
                textBoxPhpProfileDescription.Text = service.php.profile_desc;
                comboBoxPhpVersion.SelectedValue = service.php.backend;
                textBoxPhpOpenBasedir.Text = service.php.open_basedir;
                textBoxPhpUser.Text = service.php.user;
                textBoxPhpGroup.Text = service.php.group;

                if (service.php.extensions != null)
                    foreach (var item in service.php.extensions)
                    {
                        var itemIndex = checkedListBoxPhpExtensions.Items.IndexOf(item);
                        if (itemIndex >= 0)
                        {
                            checkedListBoxPhpExtensions.SetItemChecked(itemIndex, true);
                        }
                    }

                if (service.php.php_settings != null)
                {
                    List<List<String>> lines = new List<List<string>>();
                    foreach (var item in service.php.php_settings)
                    {
                        lines.Add(new List<String>() { item.Key, item.Value });
                    }
                    editListViewPhpSettings.Lines = lines;
                }
            }
        }
        private void SetDirectory(WebService resource)
        {
            if (resource.pkg_dir_prepare == null || resource.pkg_dir_prepare.Count == 0)
            {
                resource.pkg_dir_prepare = new List<PkgDirPrepare>();
                resource.pkg_dir_prepare.Add(new PkgDirPrepare());
            }
            if (resource.pkg_dir_prepare.Count > 1)
            {
                MessageBox.Show(this, String.Format("There are {0} directory configured as resource for this package. Only one is supported by MODS.", resource.pkg_dir_prepare.Count), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            var pkg_dir_prepare = resource.pkg_dir_prepare[0];
            textBoxGroup.Text = pkg_dir_prepare.group;
            textBoxSource.Text = pkg_dir_prepare.source;
            textBoxTarget.Text = pkg_dir_prepare.target;
            textBoxUser.Text = pkg_dir_prepare.user;
            int mode = 0;
            int.TryParse(pkg_dir_prepare.mode, out mode);
            userControlPermissions.Permissions = mode;

            checkBoxForceDirectory.Checked = textBoxPhpUser.Text != textBoxUser.Text || textBoxPhpGroup.Text != textBoxGroup.Text || textBoxServiceRoot.Text != textBoxTarget.Text;
        }

        private void SetPortals(WebService resource)
        {
            Portal alias = null;
            Portal server = null;
            if (resource.portals != null && resource.portals.Count > 2)
            {
                MessageBox.Show(this, String.Format("There are {0} portals configured as resource for this package. Only two (one alias and one server) are supported by MODS.", resource.pkg_dir_prepare.Count), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (resource.portals != null)
            {
                if (resource.portals.Count > 0)
                {
                    if (resource.portals[0].type == "alias")
                    {
                        alias = resource.portals[0];
                    }
                    if (resource.portals[0].type == "server")
                    {
                        server = resource.portals[0];
                    }
                }
                if (resource.portals.Count > 1)
                {
                    if (resource.portals[1].type == "alias")
                    {
                        alias = resource.portals[1];
                    }
                    if (resource.portals[1].type == "server")
                    {
                        server = resource.portals[1];
                    }
                }
            }

            //checkBoxAlias.Checked = (alias != null);
            groupBoxAliasPortal.Visible = checkBoxAlias.Checked;
            if (alias != null)
            {
                textBoxAlias.Text = alias.alias;
                textBoxAliasApp.Text = alias.app;
                textBoxAliasName.Text = alias.name;
                textBoxAliasDisplayName.Text = alias.display_name;
                textBoxAliasService.Text = alias.service;
            }

            //checkBoxServer.Checked = (server != null);
            groupBoxServerPortal.Visible = checkBoxServer.Checked;
            if (server != null)
            {
                if (server.http_port != null && server.http_port.Length > 0)
                    textBoxServerHttp.Text = server.http_port[0].ToString();
                if (server.https_port != null && server.https_port.Length > 0)
                    textBoxServerHttps.Text = server.https_port[0].ToString();
                textBoxServerApp.Text = server.app;
                textBoxServerName.Text = server.name;
                textBoxServerDisplayName.Text = server.display_name;
                textBoxServerService.Text = server.service;
            }

            var aliasServiceText = textBoxServiceName.Text + "|" + getAliasName() + "|" + getAliasDisplayName();
            var aliasText = textBoxAliasService.Text + "|" + textBoxAliasName.Text + "|" + textBoxAliasDisplayName.Text;
            if (!checkBoxAlias.Checked || aliasText == "||") aliasText = aliasServiceText;

            var serverServiceText = textBoxServiceName.Text + "|" + getServerName() + "|" + getServerDisplayName();
            var serverText = textBoxServerService.Text + "|" + textBoxServerName.Text + "|" + textBoxServerDisplayName.Text;
            if (!checkBoxServer.Checked || serverText == "||") serverText = serverServiceText;

            checkBoxForcePortals.Checked = aliasServiceText != aliasText || serverServiceText != serverText;
        }

        private void GetServices(WebService resource)
        {
            var service = resource.services[0];

            service.service = textBoxServiceName.Text;
            service.display_name = textBoxServiceDisplayName.Text;
            service.support_alias = checkBoxSupportAlias.Checked;
            service.support_server = checkBoxSupportServer.Checked;
            service.icon = textBoxServiceIcon.Text;
            service.type = comboBoxServiceType.SelectedItem as string;

            service.php.profile_name = textBoxPhpProfileName.Text;
            service.php.profile_desc = textBoxPhpProfileDescription.Text;
            int backend = 3;
            int.TryParse(comboBoxPhpVersion.SelectedValue.ToString(), out backend);
            service.php.backend = backend;
            service.php.open_basedir = textBoxPhpOpenBasedir.Text;
            service.php.user = textBoxPhpUser.Text;
            service.php.group = textBoxPhpGroup.Text;

            service.root = textBoxServiceRoot.Text;
            if (!string.IsNullOrWhiteSpace(textBoxServiceIndex.Text))
                service.index = textBoxServiceIndex.Text.Split(',');
            else
                service.index = null;

            switch (service.type)
            {
                case "static":
                    break;
                case "nginx_php":
                    if (!string.IsNullOrEmpty(textBoxConnectionTimeOut.Text)) service.connect_timeout = int.Parse(textBoxConnectionTimeOut.Text); else service.connect_timeout = 0; ;
                    if (!string.IsNullOrEmpty(textBoxReadTimeOut.Text)) service.read_timeout = int.Parse(textBoxReadTimeOut.Text); else service.read_timeout = 0;
                    if (!string.IsNullOrEmpty(textBoxSendTimeOut.Text)) service.send_timeout = int.Parse(textBoxSendTimeOut.Text); else service.send_timeout = 0;
                    break;
                case "apache_php":
                    if (!string.IsNullOrEmpty(textBoxConnectionTimeOut.Text)) service.connect_timeout = int.Parse(textBoxConnectionTimeOut.Text); else service.connect_timeout = 0; ;
                    if (!string.IsNullOrEmpty(textBoxReadTimeOut.Text)) service.read_timeout = int.Parse(textBoxReadTimeOut.Text); else service.read_timeout = 0;
                    if (!string.IsNullOrEmpty(textBoxSendTimeOut.Text)) service.send_timeout = int.Parse(textBoxSendTimeOut.Text); else service.send_timeout = 0;
                    backend = 1;
                    int.TryParse(comboBoxApacheVersion.SelectedValue.ToString(), out backend);
                    service.backend = backend;
                    service.intercept_errors = checkBoxApacheInterceptErrors.Checked;
                    break;
            }


            service.php.extensions = new List<string>();
            foreach (var item in checkedListBoxPhpExtensions.Items)
            {
                var itemIndex = checkedListBoxPhpExtensions.Items.IndexOf(item);
                if (checkedListBoxPhpExtensions.GetItemChecked(itemIndex))
                {
                    service.php.extensions.Add(item.ToString());
                }
            }
            if (service.php.extensions.Count == 0) service.php.extensions = null;

            service.php.php_settings = new Dictionary<string, string>();
            foreach (var item in editListViewPhpSettings.Lines)
            {
                service.php.php_settings.Add(item[0], item[1]);
            }
            if (service.php.php_settings.Count == 0) service.php.php_settings = null;
        }
        private void GetDirectory(WebService resource)
        {
            var pkg_dir_prepare = resource.pkg_dir_prepare[0];

            pkg_dir_prepare.group = textBoxGroup.Text;
            pkg_dir_prepare.source = textBoxSource.Text;
            pkg_dir_prepare.target = textBoxTarget.Text;
            pkg_dir_prepare.user = textBoxUser.Text;
            pkg_dir_prepare.mode = "0" + userControlPermissions.Permissions.ToString();
        }

        private void GetPortals(WebService resource)
        {
            if (resource.portals.Count > 0)
                resource.portals.Clear();

            if (checkBoxAlias.Checked)
            {
                var alias = new Portal()
                {
                    service = textBoxAliasService.Text,
                    name = textBoxAliasName.Text,
                    display_name = textBoxAliasDisplayName.Text,
                    app = textBoxAliasApp.Text,
                    type = "alias",
                    alias = textBoxAlias.Text,
                    host = ""
                };
                resource.portals.Add(alias);
            }

            if (checkBoxServer.Checked)
            {
                var server = new Portal()
                {
                    service = textBoxServerService.Text,
                    name = textBoxServerName.Text,
                    display_name = textBoxServerDisplayName.Text,
                    app = textBoxServerApp.Text,
                    type = "server",
                    alias = "",
                    host = "",
                };
                int http = 0;
                int https = 0;
                int.TryParse(textBoxServerHttp.Text, out http);
                int.TryParse(textBoxServerHttps.Text, out https);
                if (http > 0)
                    server.http_port = new int[1] { http };

                if (https > 0)
                    server.https_port = new int[1] { https };

                resource.portals.Add(server);
            }
        }
        private void checkBoxAlias_CheckedChanged(object sender, EventArgs e)
        {
            var webService = _resource;
            groupBoxAliasPortal.Visible = checkBoxAlias.Checked;
            Portal alias = null;

            if (checkBoxAlias.Checked != checkBoxSupportAlias.Checked)
                checkBoxSupportAlias.Checked = checkBoxAlias.Checked;

            if (checkBoxAlias.Checked)
            {
                if (webService.portals == null)
                    webService.portals = new List<Portal>();

                if (webService.portals.Count > 0)
                {
                    if (webService.portals[0].type == "alias")
                    {
                        alias = webService.portals[0];
                    }
                    if (webService.portals.Count > 1)
                    {
                        if (webService.portals[1].type == "alias")
                        {
                            alias = webService.portals[0];
                        }
                    }
                }
                if (alias == null)
                {
                    alias = new Portal();
                    alias.type = "alias";
                    webService.portals.Add(alias);
                }

                SetPortals(webService);
            }

        }

        private void checkBoxServer_CheckedChanged(object sender, EventArgs e)
        {
            var webService = _resource;
            groupBoxServerPortal.Visible = checkBoxServer.Checked;
            Portal server = null;

            if (checkBoxSupportServer.Checked != checkBoxServer.Checked)
                checkBoxSupportServer.Checked = checkBoxServer.Checked;

            if (checkBoxServer.Checked)
            {
                if (webService.portals == null)
                    webService.portals = new List<Portal>();

                if (webService.portals.Count > 0)
                {
                    if (webService.portals[0].type == "server")
                    {
                        server = webService.portals[0];
                    }
                    if (webService.portals.Count > 1)
                    {
                        if (webService.portals[1].type == "server")
                        {
                            server = webService.portals[0];
                        }
                    }
                }
                if (server == null)
                {
                    server = new Portal();
                    server.type = "server";
                    webService.portals.Add(server);
                }

                SetPortals(webService);
            }

        }

        private void textBoxPhpSettings_TextChanged(object sender, EventArgs e)
        {
            var phpExtension = textBoxPhpSettings.Text;
            var itemIndex = checkedListBoxPhpExtensions.Items.IndexOf(phpExtension);
            if (itemIndex >= 0)
            {
                checkedListBoxPhpExtensions.SelectedIndex = itemIndex;
            }
        }

        private void textBoxPhpSettings_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                var phpExtension = textBoxPhpSettings.Text;
                var itemIndex = checkedListBoxPhpExtensions.Items.IndexOf(phpExtension);
                if (itemIndex >= 0)
                {
                    checkedListBoxPhpExtensions.SetItemChecked(itemIndex, !checkedListBoxPhpExtensions.GetItemChecked(itemIndex));
                }
            }
        }

        private void AttachEventsToFields(Control.ControlCollection list)
        {
            foreach (var control in list)
            {
                var item = control as Control;
                var text = this.toolTip4WebService.GetToolTip(item);
                if (!string.IsNullOrEmpty(text))
                {
                    item.MouseEnter += new System.EventHandler(this.OnMouseEnter);
                    item.Enter += new System.EventHandler(this.OnMouseEnter);
                    item.MouseLeave += new System.EventHandler(this.OnMouseLeave);

                    //if (item.Name.StartsWith("textBox") && Helper.IsSubscribed(item, "EventValidating"))
                    //    item.TextChanged += new System.EventHandler(this.OnTextChanged);
                }
                else
                {
                    if (item.Controls.Count > 0)
                        AttachEventsToFields(item.Controls);
                }
            }
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            var zone = sender as Control;
            if (zone != null)
            {
                var text = toolTip4WebService.GetToolTip(zone);
                labelToolTip.Text = text;
            }
            var menu = sender as ToolStripItem;
            if (menu != null)
            {
                var text = menu.ToolTipText;
                labelToolTip.Text = text;
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            labelToolTip.Text = "";
        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //0 - static
            //1 - nginx_php
            //2 - apache_php
            //3 - reverse_proxy
            switch (comboBoxServiceType.SelectedIndex)
            {
                case 0:
                    labelConnectionTimeOut.Visible = false;
                    textBoxConnectionTimeOut.Visible = false;
                    labelReadTimeOut.Visible = false;
                    textBoxReadTimeOut.Visible = false;
                    labelSendTimeOut.Visible = false;
                    textBoxSendTimeOut.Visible = false;
                    labelApacheVersion.Visible = false;
                    comboBoxApacheVersion.Visible = false;
                    checkBoxApacheInterceptErrors.Visible = false;
                    groupBoxPhpSettings.Visible = false;
                    break;
                case 1:
                    labelConnectionTimeOut.Visible = true;
                    textBoxConnectionTimeOut.Visible = true;
                    labelReadTimeOut.Visible = true;
                    textBoxReadTimeOut.Visible = true;
                    labelSendTimeOut.Visible = true;
                    textBoxSendTimeOut.Visible = true;
                    labelApacheVersion.Visible = false;
                    comboBoxApacheVersion.Visible = false;
                    checkBoxApacheInterceptErrors.Visible = false;
                    groupBoxPhpSettings.Visible = true;
                    break;
                case 2:
                    labelConnectionTimeOut.Visible = true;
                    textBoxConnectionTimeOut.Visible = true;
                    labelReadTimeOut.Visible = true;
                    textBoxReadTimeOut.Visible = true;
                    labelSendTimeOut.Visible = true;
                    textBoxSendTimeOut.Visible = true;
                    labelApacheVersion.Visible = true;
                    comboBoxApacheVersion.Visible = true;
                    checkBoxApacheInterceptErrors.Visible = true;
                    groupBoxPhpSettings.Visible = true;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
        }

        private void checkBoxSupportAlias_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxAlias.Checked != checkBoxSupportAlias.Checked)
                checkBoxAlias.Checked = checkBoxSupportAlias.Checked;
        }

        private void checkBoxSupportServer_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxServer.Checked != checkBoxSupportServer.Checked)
                checkBoxServer.Checked = checkBoxSupportServer.Checked;
        }

        private void textBoxService_Validated(object sender, EventArgs e)
        {
            errorProviderWebService.SetError(textBoxServiceName, "");
            if (string.IsNullOrWhiteSpace(textBoxServiceRoot.Text))
                textBoxServiceRoot.Text = textBoxServiceName.Text;
        }

        private void textBoxService_Validating(object sender, CancelEventArgs e)
        {
            if (errorProviderWebService.Tag == null)
            {
                if (!textBoxServiceName.CheckEmpty(errorProviderWebService, ref e, ""))
                {
                    textBoxServiceName.Text = textBoxServiceName.Text.Replace(' ', '_').Replace("-", "").Replace("__", "_");
                    var name = textBoxServiceName.Text;
                    var cleaned = Helper.CleanUpText(textBoxServiceName.Text);
                    if (name != cleaned)
                    {
                        e.Cancel = true;
                        textBoxServiceName.Select(0, textBoxServiceName.Text.Length);
                        errorProviderWebService.SetError(textBoxServiceName, "The name of the package may not contain blanks or special characters.");
                    }
                }
            }
        }

        private void textBoxService_TextChanged(object sender, EventArgs e)
        {
            if (!checkBoxForcePortals.Checked)
            {
                textBoxAliasService.Text = textBoxServiceName.Text;
                textBoxServerService.Text = textBoxServiceName.Text;
                textBoxAliasName.Text = getAliasName();
                textBoxServerName.Text = getServerName();
            }
        }

        private void textBoxDisplayName_TextChanged(object sender, EventArgs e)
        {
            if (!checkBoxForcePortals.Checked)
            {
                textBoxAliasDisplayName.Text = getAliasDisplayName();
                textBoxServerDisplayName.Text = getServerDisplayName();
            }
        }

        private void checkBoxForce_CheckedChanged(object sender, EventArgs e)
        {
            textBoxAliasService.ReadOnly = !checkBoxForcePortals.Checked;
            textBoxAliasName.ReadOnly = !checkBoxForcePortals.Checked;
            textBoxAliasDisplayName.ReadOnly = !checkBoxForcePortals.Checked;
            textBoxAliasApp.ReadOnly = !checkBoxForcePortals.Checked;
            textBoxServerService.ReadOnly = !checkBoxForcePortals.Checked;
            textBoxServerName.ReadOnly = !checkBoxForcePortals.Checked;
            textBoxServerDisplayName.ReadOnly = !checkBoxForcePortals.Checked;
            textBoxServerApp.ReadOnly = !checkBoxForcePortals.Checked;

            if (!checkBoxForcePortals.Checked)
            {
                textBoxAliasService.Text = textBoxServiceName.Text;
                textBoxServerService.Text = textBoxServiceName.Text;
                textBoxAliasName.Text = getAliasName();
                textBoxServerName.Text = getServerName();
                textBoxAliasDisplayName.Text = getAliasDisplayName();
                textBoxServerDisplayName.Text = getServerDisplayName();
            }
        }

        private string getAliasName()
        {
            return textBoxAliasService.Text + "_alias";
        }
        private string getAliasDisplayName()
        {
            return textBoxServiceDisplayName.Text + " Alias";
        }
        private string getServerName()
        {
            return textBoxServerService.Text + "_server";
        }
        private string getServerDisplayName()
        {
            return textBoxServiceDisplayName.Text + " Server";
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            editListViewPhpSettings.AddNewItem();
        }

        private void WebService_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            var info = new ProcessStartInfo(help.Url.AbsoluteUri);
            info.UseShellExecute = true;
            Process.Start(info);
        }

        private void textBoxRoot_Validating(object sender, CancelEventArgs e)
        {
            if (errorProviderWebService.Tag == null)
            {
                textBoxServiceRoot.CheckEmpty(errorProviderWebService, ref e, "");
            }
        }

        private void textBoxRoot_Validated(object sender, EventArgs e)
        {
            errorProviderWebService.SetError(textBoxServiceRoot, "");
            if (_resource.services[0].root != null)
            {
                textBoxPhpOpenBasedir.Text = textBoxPhpOpenBasedir.Text.Replace(_resource.services[0].root, textBoxServiceRoot.Text);
                _resource.services[0].root = textBoxServiceRoot.Text;
            }
        }

        private void WebService_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                errorProviderWebService.Tag = new object(); ;
                ResetValidateChildren(this);
                switch (tabControlWebService.SelectedIndex)
                {
                    case 0:
                        textBoxServiceName.Focus();
                        break;
                    case 1:
                        if (checkBoxAlias.Checked)
                            textBoxAliasService.Focus();
                        if (checkBoxServer.Checked)
                            textBoxServerService.Focus();
                        break;
                    case 2:
                        textBoxSource.Focus();
                        break;

                }
                errorProviderWebService.Tag = null;
            }
        }

        // Reset errors possibly displayed on any Control
        private void ResetValidateChildren(Control control)
        {
            errorProviderWebService.SetError(control, "");
            foreach (Control ctrl in control.Controls)
            {
                if (ctrl != null)
                {
                    ResetValidateChildren(ctrl);
                }
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (this.ValidateChildren())
            {
                UpdateWebservice();

                if (CloseScript(DialogResult.OK)) SafeClose();
            }
            else
            {
                var unvalidatedTabs = _tabControls.Where(kvp => kvp.Value.Count != 0).Select(kvp => kvp.Key);
                TabPage firstUnvalidated = unvalidatedTabs.FirstOrDefault();
                if (firstUnvalidated != null && !unvalidatedTabs.Contains(tabControlWebService.SelectedTab))
                    tabControlWebService.SelectedTab = firstUnvalidated;
            }
        }

        private void UpdateWebservice()
        {
            GetServices(_resource);
            GetPortals(_resource);
            GetDirectory(_resource);

            _webservice = JToken.Parse(JsonConvert.SerializeObject(_resource, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));
        }

        private void tabControlWebService_SelectedIndexChanged(object sender, EventArgs e)
        {
            //this.ValidateChildren();
            //var currentTab = tabControlWebService.TabPages[currentIndex];
            //var validatedTab = _tabControls[currentTab];
            //if (validatedTab.Count > 0)
            //    tabControlWebService.SelectedIndex = currentIndex;

            //currentIndex = tabControlWebService.SelectedIndex;
        }

        private void textBoxServerHttp_Validating(object sender, CancelEventArgs e)
        {
            if (errorProviderWebService.Tag == null)
            {
                if (checkBoxServer.Checked && string.IsNullOrEmpty(textBoxServerHttp.Text) && string.IsNullOrEmpty(textBoxServerHttps.Text) && !textBoxServerHttps.Focused)
                {
                    errorProviderWebService.SetError(textBoxServerHttp, "Http port and Https port may not both be empty");
                    e.Cancel = true;
                }
            }
        }

        private void textBoxServerHttp_Validated(object sender, EventArgs e)
        {
            errorProviderWebService.SetError(textBoxServerHttp, "");
        }

        private void textBoxServerHttps_Validating(object sender, CancelEventArgs e)
        {
            if (errorProviderWebService.Tag == null)
            {
                if (checkBoxServer.Checked && string.IsNullOrEmpty(textBoxServerHttp.Text) && string.IsNullOrEmpty(textBoxServerHttps.Text) && !textBoxServerHttp.Focused)
                {
                    errorProviderWebService.SetError(textBoxServerHttps, "Http port and Https port may not both be empty");
                    e.Cancel = true;
                }
            }
        }

        public bool PendingChanges()
        {
            UpdateWebservice();

            var pendingChanges = !(_origwebservice == null && _webservice == null);

            if (pendingChanges)
            {
                pendingChanges = (_origwebservice == null && _webservice != null) || (_origwebservice != null && (_webservice == null || _origwebservice.ToString() != _webservice.ToString()));
            }

            return pendingChanges;
        }

        private void textBoxServerHttps_Validated(object sender, EventArgs e)
        {
            errorProviderWebService.SetError(textBoxServerHttps, "");
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var answer = MessageBoxEx.Show(this, "Do you really want to delete the webservice?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (answer == DialogResult.Yes)
            {
                _webservice = null;
                this.DialogResult = DialogResult.OK;
                SafeClose();
            }
        }

        private bool CloseScript(DialogResult exitMode)
        {
            DialogResult = exitMode;
            if (DialogResult == DialogResult.None)
            {
                DialogResult = MessageBoxEx.Show(this, "Do you want to save your changes?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                switch (DialogResult)
                {
                    case DialogResult.Yes:
                        DialogResult = DialogResult.OK;
                        break;
                    case DialogResult.No:
                        DialogResult = DialogResult.Cancel;
                        break;
                    case DialogResult.Cancel:
                        DialogResult = DialogResult.None;
                        break;
                }
            }

            if (DialogResult != DialogResult.None)
            {
                switch (DialogResult)
                {
                    case DialogResult.OK:

                        break;
                    case DialogResult.Cancel:
                        if (PendingChanges())
                        {
                            DialogResult = MessageBoxEx.Show(this, "Do you want really want to quit without saving your changes?", "Question", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
                            switch (DialogResult)
                            {
                                case DialogResult.Yes:

                                    break;
                                case DialogResult.No:
                                    DialogResult = DialogResult.None;
                                    break;
                                case DialogResult.Cancel:
                                    DialogResult = DialogResult.None;
                                    break;
                            }
                        }
                        break;
                }
            }

            return (DialogResult != DialogResult.None);
        }

        private void Worker_WebService_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !CloseScript(DialogResult.Cancel);
        }
        private void SafeClose()
        {
            this.FormClosing -= new System.Windows.Forms.FormClosingEventHandler(this.Worker_WebService_FormClosing);
            Close();
        }

        private void textBoxPhpUser_TextChanged(object sender, EventArgs e)
        {
            if (!checkBoxForceDirectory.Checked)
            {
                textBoxUser.Text = textBoxPhpUser.Text;
            }
        }

        private void textBoxPhpGroup_TextChanged(object sender, EventArgs e)
        {
            if (!checkBoxForceDirectory.Checked)
            {
                textBoxGroup.Text = textBoxPhpGroup.Text;
            }
        }

        private void checkBoxForceDirectory_CheckedChanged(object sender, EventArgs e)
        {

            textBoxUser.ReadOnly = !checkBoxForceDirectory.Checked;
            textBoxGroup.ReadOnly = !checkBoxForceDirectory.Checked;
            textBoxTarget.ReadOnly = !checkBoxForceDirectory.Checked;

            if (!checkBoxForceDirectory.Checked)
            {
                textBoxUser.Text = textBoxPhpUser.Text;
                textBoxGroup.Text = textBoxPhpGroup.Text;
                textBoxTarget.Text = textBoxServiceRoot.Text;
            }
        }

        private void textBoxAlias_Validating(object sender, CancelEventArgs e)
        {
            textBoxAlias.Text = Helper.CleanUpText(textBoxAlias.Text).Replace('.', '-');
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            editListViewPhpSettings.DeleteSelectedItem();
        }

        private void textBoxServiceRoot_TextChanged(object sender, EventArgs e)
        {
            if (!checkBoxForceDirectory.Checked)
            {
                textBoxTarget.Text = textBoxServiceRoot.Text;
            }
        }
    }
}
