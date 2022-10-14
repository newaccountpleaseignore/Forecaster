using System;
using System.Text;
using System.Windows.Forms;

namespace Forecaster
{
  internal static class Program
  {
    [STAThread]
    private static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      UTF8Encoding utF8Encoding = new UTF8Encoding();
      string serialCode = Forecaster.Properties.Settings.Default.SerialCode;
      if (string.IsNullOrEmpty(serialCode) || !Helper.Validate(serialCode, utF8Encoding.GetBytes("W@JAL(*&HHHK@*PP1182!*#")))
        Application.Run((Form) new BasicForm());
      else
        Application.Run((Form) new PremiumForm());
    }
  }
}
