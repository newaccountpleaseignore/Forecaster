using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Forecaster
{
  internal class Taskbar
  {
    private APPBARDATA data;
    public IntPtr shelltraywnd;

    public TaskbarPosition Position { get; private set; }

    public Rectangle Bounds { get; private set; }

    public Taskbar()
    {
      this.GetHandles();
      this.data = new APPBARDATA();
      this.data.cbSize = (uint) Marshal.SizeOf(typeof (APPBARDATA));
      this.data.hWnd = this.shelltraywnd;
      this.Position = !(Shell32.SHAppBarMessage(ABM.GetTaskbarPos, ref this.data) == IntPtr.Zero) ? (TaskbarPosition) this.data.uEdge : throw new InvalidOperationException();
      this.Bounds = Rectangle.FromLTRB(this.data.rc.Left, this.data.rc.Top, this.data.rc.Right, this.data.rc.Bottom);
      this.data.cbSize = (uint) Marshal.SizeOf(typeof (APPBARDATA));
      Shell32.SHAppBarMessage(ABM.GetState, ref this.data).ToInt32();
    }

    public void GetHandles() => this.shelltraywnd = User32.FindWindowEx(IntPtr.Zero, IntPtr.Zero, "Shell_TrayWnd", (string) null);

    public void GetTaskbarPosition()
    {
      try
      {
        Shell32.SHAppBarMessage(ABM.GetTaskbarPos, ref this.data);
        this.Position = (TaskbarPosition) this.data.uEdge;
        this.Bounds = Rectangle.FromLTRB(this.data.rc.Left, this.data.rc.Top, this.data.rc.Right, this.data.rc.Bottom);
      }
      catch
      {
        this.Position = TaskbarPosition.Unknown;
      }
    }
  }
}
