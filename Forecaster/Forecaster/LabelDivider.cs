using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Forecaster
{
  public class LabelDivider : UserControl
  {
    private IContainer components;
    private string label = "No text...";
    private Rectangle labelRect;
    private Rectangle lineRect;
    private TextFormatFlags labelFlags = TextFormatFlags.NoClipping | TextFormatFlags.VerticalCenter;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Name = nameof (LabelDivider);
      this.Size = new Size(440, 33);
      this.ResumeLayout(false);
    }

    public string Label
    {
      get => this.label;
      set
      {
        if (!(this.label != value))
          return;
        this.label = value;
        this.GetRectangles();
        this.Invalidate();
      }
    }

    public LabelDivider()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.DoubleBuffered = true;
      this.GetRectangles();
    }

    private void GetRectangles()
    {
      int x = 6;
      int width = TextRenderer.MeasureText(this.label, this.Font).Width;
      this.labelRect = new Rectangle(new Point(x, 0), new Size(width + x, this.ClientRectangle.Height));
      this.lineRect = new Rectangle(new Point(width + x * 2, this.ClientRectangle.Height / 2), new Size(this.ClientRectangle.Width - (width + x * 3), 2));
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      TextRenderer.DrawText((IDeviceContext) e.Graphics, this.label, this.Font, this.labelRect, SystemColors.ControlText, this.labelFlags);
      e.Graphics.DrawLine(Pens.Gray, new Point(this.lineRect.Left, this.lineRect.Top), new Point(this.lineRect.Right, this.lineRect.Top));
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.GetRectangles();
      this.Invalidate();
    }
  }
}
