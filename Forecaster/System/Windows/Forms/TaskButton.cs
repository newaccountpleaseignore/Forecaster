using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
  public class TaskButton : UserControl
  {
    private IContainer components;
    private ToolTip toolTip;
    private bool isHover;
    private VisualStyleRenderer renderer_Hot;
    private TextFormatFlags dayFlags;
    private TextFormatFlags conditionFlags;
    private TextFormatFlags temperatureFlags;
    private Rectangle conditionRect;
    private Rectangle temperatureRect;
    private Rectangle imageRect;
    private Rectangle dayRect;
    private TaskButton.Orientation direction;
    private string condition = string.Empty;
    private string temperature = string.Empty;
    private string day = string.Empty;
    private Color textColor = SystemColors.WindowText;
    private string tooltipText = string.Empty;
    [Description("Icon displayed on button")]
    [Category("Appearance")]
    private Bitmap image;
    private int imageSize = 32;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.toolTip = new ToolTip(this.components);
      this.SuspendLayout();
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.DoubleBuffered = true;
      this.Name = nameof (TaskButton);
      this.Size = new Size(309, 47);
      this.ResumeLayout(false);
    }

    public TaskButton.Orientation Direction
    {
      get => this.direction;
      set
      {
        if (this.direction == value)
          return;
        this.direction = value;
        this.GetRectangles();
        this.GetTextFormatFlags();
        this.Invalidate();
      }
    }

    public string Condition
    {
      get => this.condition;
      set
      {
        if (!(this.condition != value))
          return;
        this.condition = value;
        this.Invalidate();
      }
    }

    public string Temperature
    {
      get => this.temperature;
      set
      {
        if (!(this.temperature != value))
          return;
        this.temperature = value;
        this.Invalidate();
      }
    }

    public string Day
    {
      get => this.day;
      set
      {
        if (!(this.day != value))
          return;
        this.day = value;
        this.Invalidate();
      }
    }

    public Color TextColor
    {
      get => this.textColor;
      set
      {
        if (!(this.textColor != value))
          return;
        this.textColor = value;
        this.Invalidate();
      }
    }

    public string TooltipText
    {
      get => this.tooltipText;
      set
      {
        if (!(this.tooltipText != value))
          return;
        this.tooltipText = value;
        this.toolTip.ToolTipTitle = this.tooltipText;
      }
    }

    public Bitmap Image
    {
      get => this.image;
      set
      {
        if (this.image == value)
          return;
        this.image = value;
        this.Invalidate();
      }
    }

    public int ImageSize
    {
      get => this.imageSize;
      set
      {
        if (this.imageSize == value)
          return;
        this.imageSize = value;
        this.GetRectangles();
        this.Invalidate();
      }
    }

    public TaskButton()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.DoubleBuffered = true;
      this.GetRectangles();
      this.GetTextFormatFlags();
      this.GetImages();
    }

    private void GetTextFormatFlags()
    {
      if (this.direction == TaskButton.Orientation.Horizontal)
      {
        this.dayFlags = TextFormatFlags.NoClipping | TextFormatFlags.Right | TextFormatFlags.VerticalCenter;
        this.conditionFlags = TextFormatFlags.Bottom | TextFormatFlags.EndEllipsis | TextFormatFlags.NoClipping;
        this.temperatureFlags = TextFormatFlags.NoClipping;
      }
      else
      {
        if (this.direction != TaskButton.Orientation.Vertical)
          return;
        this.dayFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping | TextFormatFlags.VerticalCenter;
        this.conditionFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping | TextFormatFlags.WordBreak;
        this.temperatureFlags = TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping | TextFormatFlags.VerticalCenter;
      }
    }

    private void GetRectangles()
    {
      if (this.direction == TaskButton.Orientation.Horizontal)
      {
        int num = 4;
        int x1 = num;
        int width1 = 75;
        int x2 = width1;
        int x3 = num + width1 + this.imageSize;
        int y = 0;
        int width2 = this.ClientRectangle.Width - x3 - num;
        this.dayRect = new Rectangle(new Point(x1, 0), new Size(width1, this.ClientRectangle.Height));
        this.conditionRect = new Rectangle(new Point(x3, y), new Size(width2, this.ClientRectangle.Height / 2));
        this.temperatureRect = new Rectangle(new Point(x3, this.ClientRectangle.Height / 2), new Size(width2, this.ClientRectangle.Height / 2));
        this.imageRect = new Rectangle(new Point(x2, this.ClientRectangle.Height / 2 - this.imageSize / 2), new Size(this.imageSize, this.imageSize));
      }
      else
      {
        if (this.direction != TaskButton.Orientation.Vertical)
          return;
        int num = 4;
        int y1 = num;
        int y2 = 40;
        int x = 0;
        int y3 = 40 + this.imageSize + num;
        int width = this.ClientRectangle.Width;
        this.dayRect = new Rectangle(new Point(0, y1), new Size(this.ClientRectangle.Width, 26));
        this.conditionRect = new Rectangle(new Point(x, y3), new Size(width, 40));
        this.temperatureRect = new Rectangle(new Point(x, y3 + 30), new Size(width, 40));
        this.imageRect = new Rectangle(new Point(this.ClientRectangle.Width / 2 - this.imageSize / 2, y2), new Size(this.imageSize, this.imageSize));
      }
    }

    private void GetImages()
    {
      if (!Application.RenderWithVisualStyles)
        return;
      this.renderer_Hot = new VisualStyleRenderer("Explorer::ListView", 1, 2);
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.GetRectangles();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      if (Application.RenderWithVisualStyles && this.renderer_Hot == null)
        this.GetImages();
      if (this.isHover && Application.RenderWithVisualStyles)
        this.renderer_Hot.DrawBackground((IDeviceContext) e.Graphics, this.ClientRectangle);
      e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
      e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
      using (Font font = new Font(this.Font.FontFamily, 18.25f, FontStyle.Regular))
        TextRenderer.DrawText((IDeviceContext) e.Graphics, this.day, font, this.dayRect, SystemColors.GrayText, this.dayFlags);
      TextRenderer.DrawText((IDeviceContext) e.Graphics, this.condition, this.Font, this.conditionRect, SystemColors.WindowText, this.conditionFlags);
      TextRenderer.DrawText((IDeviceContext) e.Graphics, this.temperature, this.Font, this.temperatureRect, SystemColors.GrayText, this.temperatureFlags);
      if (this.image == null)
        return;
      e.Graphics.DrawImage((System.Drawing.Image) this.image, this.imageRect);
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
    }

    protected override void OnMouseEnter(EventArgs e)
    {
      this.isHover = true;
      this.Invalidate();
    }

    protected override void OnMouseLeave(EventArgs e)
    {
      this.isHover = false;
      this.Invalidate();
    }

    public enum Orientation
    {
      Horizontal,
      Vertical,
    }
  }
}
