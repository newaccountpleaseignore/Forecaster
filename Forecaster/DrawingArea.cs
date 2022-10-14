using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;

public abstract class DrawingArea : Panel
{
  protected Graphics graphics;

  protected override CreateParams CreateParams
  {
    get
    {
      CreateParams createParams = base.CreateParams;
      createParams.ExStyle |= 32;
      return createParams;
    }
  }

  protected override void OnPaint(PaintEventArgs e)
  {
    this.graphics = e.Graphics;
    this.graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
    this.graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
    this.graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
    this.graphics.SmoothingMode = SmoothingMode.AntiAlias;
    this.graphics.CompositingQuality = CompositingQuality.HighQuality;
    this.OnDraw();
  }

  protected abstract void OnDraw();

  protected void DrawText(
    string text,
    string fontFamily,
    float fontSize,
    FontStyle style,
    Color color,
    Rectangle rectangle,
    TextFormatFlags flags)
  {
    DrawingArea.DrawText(this.graphics, text, fontFamily, fontSize, style, color, rectangle, flags);
  }

  public static void DrawText(
    Graphics graphics,
    string text,
    string fontFamily,
    float fontSize,
    FontStyle style,
    Color color,
    Rectangle rectangle,
    TextFormatFlags flags)
  {
    using (Font font = new Font(fontFamily, fontSize, style))
      TextRenderer.DrawText((IDeviceContext) graphics, text, font, rectangle, color, flags);
  }
}
