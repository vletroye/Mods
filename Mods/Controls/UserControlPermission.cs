using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BeatificaBytes.Synology.Mods.Controls
{
    public partial class UserControlPermission : UserControl
    {
        public UserControlPermission()
        {
            InitializeComponent();
        }

        public int Permissions
        {
            get
            {
                int permissions = 0;
                int userR = 0, userW = 0, userX = 0;
                int groupR = 0, groupW = 0, groupX = 0;
                int othersR = 0, othersW = 0, othersX = 0;

                userR = checkedListBoxUser.GetItemCheckState(0) == CheckState.Checked ? 4 : 0;
                userW = checkedListBoxUser.GetItemCheckState(1) == CheckState.Checked ? 2 : 0;
                userX = checkedListBoxUser.GetItemCheckState(2) == CheckState.Checked ? 1 : 0;

                groupR = checkedListBoxGroup.GetItemCheckState(0) == CheckState.Checked ? 4 : 0;
                groupW = checkedListBoxGroup.GetItemCheckState(1) == CheckState.Checked ? 2 : 0;
                groupX = checkedListBoxGroup.GetItemCheckState(2) == CheckState.Checked ? 1 : 0;

                othersR = checkedListBoxOthers.GetItemCheckState(0) == CheckState.Checked ? 4 : 0;
                othersW = checkedListBoxOthers.GetItemCheckState(1) == CheckState.Checked ? 2 : 0;
                othersX = checkedListBoxOthers.GetItemCheckState(2) == CheckState.Checked ? 1 : 0;

                permissions = (othersR + othersW + othersX) + 10 * (groupR + groupW + groupX) + 100 * (userR + userW + userX);

                return permissions;
            }
            set
            {
                int permissions = value;
                int user = permissions / 100;
                int group = (permissions - user * 100) / 10;
                int others = permissions - user * 100 - group * 10;

                checkedListBoxUser.SetItemChecked(0, IsRead(user));
                checkedListBoxUser.SetItemChecked(1, IsWrite(user));
                checkedListBoxUser.SetItemChecked(2, IsExecute(user));

                checkedListBoxGroup.SetItemChecked(0, IsRead(group));
                checkedListBoxGroup.SetItemChecked(1, IsWrite(group));
                checkedListBoxGroup.SetItemChecked(2, IsExecute(group));

                checkedListBoxOthers.SetItemChecked(0, IsRead(others));
                checkedListBoxOthers.SetItemChecked(1, IsWrite(others));
                checkedListBoxOthers.SetItemChecked(2, IsExecute(others));
            }
        }

        /// <summary>
        /// 4, 5, 6, 7
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsRead(int value)
        {
            return Array.Exists(new int[] { 4, 5, 6, 7 }, element => element == value);
        }
        /// <summary>
        /// 2, 3, 6, 7
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsWrite(int value)
        {
            return Array.Exists(new int[] { 2, 3, 6, 7 }, element => element == value);
        }
        /// <summary>
        /// 1, 3, 5, 7
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool IsExecute(int value)
        {
            return Array.Exists(new int[] { 1, 3, 5, 7 }, element => element == value);
        }
    }
}
