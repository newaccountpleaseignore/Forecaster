using System.Drawing;
using System.Drawing.Text;

namespace System.Windows.Forms
{
  public class HintTextBox : TextBox
  {
    private Font hintFont;
    private bool entered;
    private string hintText;
    private Color hintColor;

    public Color HintColor
    {
      get => this.hintColor;
      set => this.hintColor = value;
    }

    public string HintText
    {
      get => this.hintText;
      set => this.hintText = value;
    }

    public HintTextBox()
    {
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.DoubleBuffered = true;
      this.hintColor = Color.LightGray;
      this.hintText = nameof (HintTextBox);
      this.hintFont = new Font("Segeo UI", 8.25f, FontStyle.Regular);
    }

    protected override void WndProc(ref Message m)
    {
      base.WndProc(ref m);
      int num = 15;
      if (m.Msg != num)
        return;
      using (Graphics dc = Graphics.FromHwnd(this.Handle))
      {
        dc.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
        if (this.entered || this.Text.Length >= 1)
          return;
        dc.Clear(this.BackColor);
        TextRenderer.DrawText((IDeviceContext) dc, this.hintText, this.hintFont, this.ClientRectangle, this.HintColor, TextFormatFlags.VerticalCenter);
      }
    }

    protected override void OnLeave(EventArgs e)
    {
      this.entered = false;
      base.OnLeave(e);
    }

    protected override void OnEnter(EventArgs e)
    {
      this.entered = true;
      if (this.Text.Length < 1)
      {
        using (Graphics graphics = Graphics.FromHwnd(this.Handle))
          graphics.Clear(this.BackColor);
      }
      base.OnEnter(e);
    }
  }
}
