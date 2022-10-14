using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

internal class HeaderPanel : DrawingArea
{
  [Description("Icon displayed on button")]
  [Category("Appearance")]
  private HeaderPanel.Version appVersion;
  [Description("Icon displayed on button")]
  [Category("Appearance")]
  private string city;
  [Description("Icon displayed on button")]
  [Category("Appearance")]
  private string condition;
  [Description("Icon displayed on button")]
  [Category("Appearance")]
  private string temperature;
  [Category("Appearance")]
  [Description("Icon displayed on button")]
  private Bitmap weatherImage;
  private Rectangle weatherRectangle;
  private Rectangle cityRectangle;
  private Rectangle conditionRectangle;
  private Rectangle temperatureRectangle;

  public HeaderPanel.Version AppVersion
  {
    get => this.appVersion;
    set
    {
      if (this.appVersion == value)
        return;
      this.appVersion = value;
      this.GetRectangles();
      this.Invalidate();
    }
  }

  public string City
  {
    get => this.city;
    set
    {
      if (!(this.city != value))
        return;
      this.city = value;
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

  public Bitmap WeatherImage
  {
    get => this.weatherImage;
    set
    {
      if (this.weatherImage == value)
        return;
      this.weatherImage = value;
      this.Invalidate();
    }
  }

  private void GetRectangles()
  {
    if (this.appVersion == HeaderPanel.Version.Basic)
    {
      this.weatherRectangle = new Rectangle(new Point(100, -52), new Size(256, 256));
      this.cityRectangle = new Rectangle(new Point(10, 10), new Size(264, 40));
      this.conditionRectangle = new Rectangle(new Point(14, 48), new Size(264, 18));
      this.temperatureRectangle = new Rectangle(new Point(10, 65), new Size(270, 30));
    }
    else
    {
      if (this.appVersion != HeaderPanel.Version.Premium)
        return;
      this.weatherRectangle = new Rectangle(new Point(-75, -75), new Size(256, 256));
      this.cityRectangle = new Rectangle(new Point(10, this.ClientRectangle.Height - 50), new Size(this.ClientRectangle.Width - 10, 40));
      this.conditionRectangle = new Rectangle(new Point(14, this.ClientRectangle.Height - 110), new Size(this.ClientRectangle.Width, 60));
      this.temperatureRectangle = new Rectangle(new Point(0, 10), new Size(this.ClientRectangle.Width, 30));
    }
  }

  protected override void OnCreateControl()
  {
    base.OnCreateControl();
    this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.DoubleBuffered = true;
    this.GetRectangles();
  }

  protected override void OnDraw()
  {
    this.DrawText(this.condition, "Segeo UI", 9.25f, FontStyle.Regular, Color.Gray, this.conditionRectangle, TextFormatFlags.Bottom);
    this.DrawText(this.city, "Segeo UI", 22.25f, FontStyle.Regular, Color.DodgerBlue, this.cityRectangle, TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter);
    if (this.appVersion == HeaderPanel.Version.Premium)
    {
      using (LinearGradientBrush linearGradientBrush = new LinearGradientBrush(new Point(this.ClientRectangle.Width / 2, 0), new Point(this.ClientRectangle.Width, 0), Color.Transparent, SystemColors.Window))
        this.graphics.FillRectangle((Brush) linearGradientBrush, new Rectangle(new Point(this.ClientRectangle.Width / 2, 0), new Size(this.ClientRectangle.Width / 2, this.ClientRectangle.Height)));
    }
    if (this.weatherImage != null)
      this.graphics.DrawImage((Image) this.weatherImage, this.weatherRectangle);
    if (this.appVersion == HeaderPanel.Version.Basic)
    {
      this.DrawText(this.temperature, "Segeo UI", 22.25f, FontStyle.Regular, SystemColors.WindowText, this.temperatureRectangle, TextFormatFlags.SingleLine | TextFormatFlags.VerticalCenter | TextFormatFlags.WordEllipsis);
    }
    else
    {
      if (this.appVersion != HeaderPanel.Version.Premium)
        return;
      this.DrawText(this.temperature, "Segeo UI", 18.25f, FontStyle.Regular, SystemColors.WindowText, this.temperatureRectangle, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
    }
  }

  public enum Version
  {
    Basic,
    Premium,
  }
}
