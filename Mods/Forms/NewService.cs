using ImageMagick;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods.Forms
{
    public partial class NewService : Form
    {

        private HelpInfo help = new HelpInfo(new Uri("https://help.synology.com/developer-guide/integrate_dsm/config.html"), "Details about Application Config");
        public NewService()
        {
            InitializeComponent();

            PrepareFieldsEvents();
        }

        private void NewService_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            var info = new ProcessStartInfo(help.Url.AbsoluteUri);
            info.UseShellExecute = true;
            Process.Start(info);
        }

        internal AppDataType GetServiceType
        {
            get
            {
                AppDataType selection = AppDataType.Undefined;

                if (radioButtonUrl.Checked)
                    selection = AppDataType.Url;
                if (radioButtonScript.Checked)
                    selection = AppDataType.Script;
                if (radioButtonWeb.Checked)
                    selection = AppDataType.WebApp;

                return selection;

            }
        }

        private void PrepareFieldsEvents()
        {
            foreach (var control in this.groupBoxService.Controls)
            {
                var item = control as Control;
                if (item != null)
                {
                    item.MouseEnter += new System.EventHandler(this.OnMouseEnter);
                    item.Enter += new System.EventHandler(this.OnMouseEnter);
                    item.MouseLeave += new System.EventHandler(this.OnMouseLeave);
                }
            }
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            var zone = sender as Control;
            if (zone != null)
            {
                var text = toolTipNewService.GetToolTip(zone);
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
    }
}
