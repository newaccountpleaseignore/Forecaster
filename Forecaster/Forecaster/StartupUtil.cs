using IWshRuntimeLibrary;
using System;
using System.IO;
using System.Windows.Forms;

namespace Forecaster
{
  internal class StartupUtil
  {
    private static string GetStartupShortcutFilename() => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Startup), Application.ProductName) + ".lnk";

    public static void AddShortcutToStartupFolder()
    {
      WshShellClass wshShellClass = new WshShellClass();
      string shortcutFilename = StartupUtil.GetStartupShortcutFilename();
      if (System.IO.File.Exists(shortcutFilename))
        return;
      IWshShortcut shortcut = (IWshShortcut) wshShellClass.CreateShortcut(shortcutFilename);
      shortcut.TargetPath = Application.ExecutablePath;
      shortcut.WorkingDirectory = Application.StartupPath;
      shortcut.Description = "Launch My Application";
      shortcut.Save();
    }

    public static bool RemoveShortcutFromStartupFolder()
    {
      bool flag = false;
      string shortcutFilename = StartupUtil.GetStartupShortcutFilename();
      if (System.IO.File.Exists(shortcutFilename))
      {
        try
        {
          System.IO.File.Delete(shortcutFilename);
          flag = true;
        }
        catch (Exception ex)
        {
          int num = (int) MessageBox.Show(string.Format("Unable to remove this application from the Startup list.  Administrative privledges are required to perform this operation.\n\nDetails: SecurityException: {0}", (object) ex.Message), "Update Startup Mode", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
      }
      return flag;
    }
  }
}
