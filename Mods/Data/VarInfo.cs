using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeatificaBytes.Synology.Mods.Data
{
    public class VarInfo
    {
        static public void GetSynoEnvVariables(List<Tuple<string, string>> variables)
        {
            //todo: replace this by a resource file to be displayed in a listview as done with 'HelpSynoWizard.csv'
            variables.Add(new Tuple<string, string>("SYNOPKG_PKGNAME", "Package identifier which is defined in INFO"));
            variables.Add(new Tuple<string, string>("SYNOPKG_PKGVER", "Package version which is defined in INFO"));
            variables.Add(new Tuple<string, string>("SYNOPKG_PKGDEST", "Target directory where the package is stored"));
            variables.Add(new Tuple<string, string>("SYNOPKG_PKGDEST_VOL", "Target volume where the package is stored [DSM >= 4.2]"));
            variables.Add(new Tuple<string, string>("SYNOPKG_PKGPORT", "adminport port which is defined in INFO.This port will be occupied by this package with its management interface"));
            variables.Add(new Tuple<string, string>("SYNOPKG_PKG_STATUS", "Package status presented by these values: INSTALL, UPGRADE, UNINSTALL, START, STOP or empty [DSM >= 4.0]"));
            variables.Add(new Tuple<string, string>("SYNOPKG_PKGINST_TEMP_DIR", "The temporary directory where the package are extracted when installing or upgrading it"));
            variables.Add(new Tuple<string, string>("SYNOPKG_PKG_PROGRESS_PATH", "A temporary file path for a script to showing the progress in installing and upgrading a package [DSM >= 5.2]"));
            variables.Add(new Tuple<string, string>("SYNOPKG_OLD_PKGVER", "Existing package version which is defined in INFO (only in preupgrade script)"));
            variables.Add(new Tuple<string, string>("SYNOPKG_USERNAME", "The user name who installs, upgrades, uninstalls, starts or stops the package.If the value is empty, the action is triggered by DSM, not by the end user [DSM >= 5.2"));
            variables.Add(new Tuple<string, string>("SYNOPKG_TEMP_SPKFILE", "The location of package spk file is temporarily stored in DS when the package is installing/upgrading [DSM >= 4.2]"));
            variables.Add(new Tuple<string, string>("SYNOPKG_TEMP_LOGFILE", "A temporary file path for a script to log information or error messages. This log is displayed automatically at the end of the installation."));
            variables.Add(new Tuple<string, string>("SYNOPKG_TEMP_UPGRADE_FOLDER", "The temporary directory when the package is upgrading.You can move the files from the previous version of the package to it in preupgrade script and move them back in postupgrade [DSM >= 6.0]"));
            variables.Add(new Tuple<string, string>("SYNOPKG_DSM_ARCH", "End user’s DSM CPU architecture"));
            variables.Add(new Tuple<string, string>("SYNOPKG_DSM_LANGUAGE", "End user's DSM language"));
            variables.Add(new Tuple<string, string>("SYNOPKG_DSM_VERSION_MAJOR", "End user’s major number of DSM version which is formatted as [DSM major number].[DSM minor number]-[DSM build number]"));
            variables.Add(new Tuple<string, string>("SYNOPKG_DSM_VERSION_MINOR", "End user’s minor number of DSM version which is formatted as [DSM major number].[DSM minor number]-[DSM build number]"));
            variables.Add(new Tuple<string, string>("SYNOPKG_DSM_VERSION_BUILD", "End user’s DSM build number of DSM version which is formatted as [DSM major number].[DSM minor number]-[DSM build number]"));
            variables.Add(new Tuple<string, string>("", ""));
            variables.Add(new Tuple<string, string>("See more details via the Help link", ""));
        }

    }
}
