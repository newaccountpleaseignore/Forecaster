using System;
using System.IO;
using System.Threading;

namespace Forecaster
{
  internal class DebugLog
  {
    public static void SetupDebugLog()
    {
      using (TextWriter text = (TextWriter) File.CreateText("debug.txt"))
      {
        text.WriteLine(Environment.OSVersion.VersionString);
        text.WriteLine(".Net Framework: " + (object) Environment.Version);
        text.WriteLine("Language ID: " + Thread.CurrentThread.CurrentCulture.Name);
        text.WriteLine("===============================");
        text.Flush();
        text.Close();
      }
    }

    public static void WriteDebugLog(string error)
    {
      using (TextWriter text = (TextWriter) File.CreateText("debug.txt"))
      {
        text.WriteLine(string.Format("{0}: {1}", (object) DateTime.Now.ToString(), (object) error));
        text.Flush();
        text.Close();
      }
    }
  }
}
