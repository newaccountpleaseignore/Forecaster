using Forecaster.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;
using System.Xml;
using System.Xml.Serialization;
using WeatherAPI;

namespace Forecaster
{
  public class PremiumForm : Form
  {
    private const int WM_SETTINGCHANGE = 26;
    private const int WM_NCHITTEST = 132;
    private const int WM_SETCURSOR = 32;
    private const int WM_LBUTTONDOWN = 513;
    private const int WM_RBUTTONDOWN = 516;
    private const int WM_MBUTTONDOWN = 519;
    private const int WM_XBUTTONDOWN = 523;
    private const int HTCLIENT = 1;
    private Taskbar taskbar = new Taskbar();
    private bool updateAvailable;
    private string url = string.Empty;
    private System.Threading.Timer networkActivityTimer;
    private TimerCallback networkActivityTimer_Callback;
    private Mutex m_CtrlMutex;
    private List<string> locations = new List<string>();
    private List<Forecast> forecasts = new List<Forecast>();
    private Language language;
    private VisualStyleRenderer footerRenderer;
    private VisualStyleRenderer headerRenderer;
    private Icon sun;
    private Icon clouds;
    private Icon snow;
    private Icon rain;
    private Icon tstorm;
    private Conditions oldCondition;
    private int lastTooltip;
    private bool isConnected;
    private bool isUpdating;
    private IContainer components;
    private Panel panel_Footer_Container;
    private NotifyIcon notifyIcon1;
    private System.Windows.Forms.Timer NetworkActivityTimer;
    private LinkLabel linkLabel1;
    private ContextMenu contextMenu1;
    private MenuItem menuItem1;
    private System.Windows.Forms.ToolTip toolTip1;
    private PageIndicator pageIndicator;
    private FlowLayoutPanel flowLayoutPanel1;
    private HeaderPanel headerPanel1;
    private Panel panel1;

    [DllImport("user32.dll")]
    private static extern IntPtr DefWindowProc(
      IntPtr hWnd,
      int uMsg,
      IntPtr wParam,
      IntPtr lParam);

    [DllImport("uxtheme", CharSet = CharSet.Unicode)]
    public static extern int SetWindowTheme(
      IntPtr hWnd,
      string textSubAppName,
      string textSubIdList);

    [DllImport("dwmapi.dll")]
    private static extern int DwmIsCompositionEnabled(out bool enabled);

    public PremiumForm() => this.InitializeComponent();

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    protected override void WndProc(ref Message m)
    {
      switch (m.Msg)
      {
        case 26:
          this.CheckVisualStyle();
          this.GetWindowPostion();
          break;
        case 32:
          if (this.IsOverClientArea(m.HWnd, m.WParam, m.LParam))
            break;
          switch ((int) m.LParam >> 16)
          {
            case 513:
            case 516:
            case 519:
            case 523:
              this.Focus();
              m.Result = IntPtr.Zero;
              return;
            default:
              return;
          }
        case 132:
          if (this.IsOverClientArea(m.HWnd, m.WParam, m.LParam))
            break;
          m.Result = (IntPtr) 1;
          break;
        default:
          base.WndProc(ref m);
          break;
      }
    }

    private bool IsOverClientArea(IntPtr hwnd, IntPtr wParam, IntPtr LParam) => PremiumForm.DefWindowProc(hwnd, 132, wParam, LParam).ToInt32() == 1;

    private void Form1_Load(object sender, EventArgs e)
    {
      this.StartPosition = FormStartPosition.Manual;
      this.Hide();
      this.notifyIcon1.Icon = Resources.rain;
      this.notifyIcon1.ContextMenu = this.contextMenu1;
      this.headerPanel1.AppVersion = HeaderPanel.Version.Premium;
      PageIndicator.OnPageChanged += new PageIndicator.OnPageChangedHandler(this.PageIndicator_OnPageChanged);
      this.CheckForUpdate();
      this.CheckVisualStyle();
      this.GetWindowPostion();
      this.GetLocations();
      this.GetForecastButtons();
      this.GetTrayIcons();
      Control.CheckForIllegalCrossThreadCalls = false;
      this.m_CtrlMutex = new Mutex();
      int updateInterval = Forecaster.Properties.Settings.Default.UpdateInterval;
      this.networkActivityTimer_Callback = new TimerCallback(this.OnActivityTimer);
      this.networkActivityTimer = new System.Threading.Timer(this.networkActivityTimer_Callback, (object) null, 500, updateInterval);
    }

    private void Form1_Shown(object sender, EventArgs e) => this.Hide();

    private void Form1_Activated(object sender, EventArgs e) => this.Focus();

    private void Form1_Deactivate(object sender, EventArgs e) => this.Hide();

    private void Form1_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.networkActivityTimer.Change(-1, -1);
      Thread.Sleep(1000);
      this.networkActivityTimer.Dispose();
    }

    private void CheckForUpdate()
    {
      this.isConnected = Helper.CheckForInternetConnection();
      if (!this.isConnected)
        return;
      System.Version version = (System.Version) null;
      try
      {
        XmlTextReader xmlTextReader = new XmlTextReader("http://dl.dropbox.com/u/3687220/Forecaster/app_version.xml");
        int content = (int) xmlTextReader.MoveToContent();
        string str = "";
        if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == "program")
        {
          while (xmlTextReader.Read())
          {
            if (xmlTextReader.NodeType == XmlNodeType.Element)
              str = xmlTextReader.Name;
            else if (xmlTextReader.NodeType == XmlNodeType.Text && xmlTextReader.HasValue)
            {
              switch (str)
              {
                case "version":
                  version = new System.Version(xmlTextReader.Value);
                  continue;
                case "url":
                  this.url = xmlTextReader.Value;
                  continue;
                default:
                  continue;
              }
            }
          }
        }
        xmlTextReader.Close();
      }
      catch (Exception ex)
      {
      }
      if (Assembly.GetExecutingAssembly().GetName().Version.CompareTo(version) < 0)
        this.updateAvailable = true;
      if (!this.updateAvailable)
        return;
      this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
      this.notifyIcon1.BalloonTipTitle = "A new version is available!";
      this.notifyIcon1.BalloonTipText = "Click here to download the new version of Forecaster.";
      this.notifyIcon1.ShowBalloonTip(1000);
    }

    private void CheckVisualStyle()
    {
      bool enabled;
      PremiumForm.DwmIsCompositionEnabled(out enabled);
      if (Application.RenderWithVisualStyles)
      {
        if (enabled)
          this.FormBorderStyle = FormBorderStyle.Sizable;
        else
          this.FormBorderStyle = FormBorderStyle.None;
        if (this.headerRenderer == null)
          this.headerRenderer = new VisualStyleRenderer("TRAYNOTIFYFLYOUT", 1, 1);
        if (this.footerRenderer != null)
          return;
        this.footerRenderer = new VisualStyleRenderer("TRAYNOTIFYFLYOUT", 2, 1);
      }
      else
      {
        this.FormBorderStyle = FormBorderStyle.None;
        this.headerRenderer = (VisualStyleRenderer) null;
        this.footerRenderer = (VisualStyleRenderer) null;
      }
    }

    private void GetTranslation()
    {
      string startupPath = Application.StartupPath;
      string name = Thread.CurrentThread.CurrentCulture.Name;
      if (File.Exists("Languages\\" + name + ".xml"))
      {
        using (FileStream fileStream = new FileStream(startupPath + "\\Languages\\" + name + ".xml", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          this.language = (Language) new XmlSerializer(typeof (Language)).Deserialize((Stream) fileStream);
          fileStream.Close();
        }
      }
      else
      {
        using (FileStream fileStream = new FileStream(startupPath + "\\Languages\\default.xml", FileMode.Open, FileAccess.Read, FileShare.Read))
        {
          this.language = (Language) new XmlSerializer(typeof (Language)).Deserialize((Stream) fileStream);
          fileStream.Close();
        }
        DebugLog.WriteDebugLog("Could not find translation! Fallback to default language.");
      }
    }

    private void GetWindowPostion()
    {
      bool enabled;
      PremiumForm.DwmIsCompositionEnabled(out enabled);
      this.taskbar.GetTaskbarPosition();
      switch (this.taskbar.Position)
      {
        case TaskbarPosition.Left:
          if (Application.RenderWithVisualStyles && enabled)
          {
            this.Location = new Point(this.taskbar.Bounds.Right + 8, this.taskbar.Bounds.Bottom - this.Height - 10);
            break;
          }
          this.Location = new Point(this.taskbar.Bounds.Right, this.taskbar.Bounds.Bottom - this.Height);
          break;
        case TaskbarPosition.Top:
          if (Application.RenderWithVisualStyles && enabled)
          {
            this.Location = new Point(this.taskbar.Bounds.Right - this.Width - 10, this.taskbar.Bounds.Bottom + 8);
            break;
          }
          this.Location = new Point(this.taskbar.Bounds.Right - this.Width - 10, this.taskbar.Bounds.Bottom);
          break;
        case TaskbarPosition.Right:
          if (Application.RenderWithVisualStyles && enabled)
          {
            this.Location = new Point(this.taskbar.Bounds.Left - this.Width - 8, this.taskbar.Bounds.Bottom - this.Height - 10);
            break;
          }
          this.Location = new Point(this.taskbar.Bounds.Left - this.Width, this.taskbar.Bounds.Bottom - this.Height);
          break;
        case TaskbarPosition.Bottom:
          if (Application.RenderWithVisualStyles && enabled)
          {
            this.Location = new Point(this.taskbar.Bounds.Right - this.Width - 10, this.taskbar.Bounds.Top - this.Height - 8);
            break;
          }
          this.Location = new Point(this.taskbar.Bounds.Right - this.Width - 10, this.taskbar.Bounds.Top - this.Height);
          break;
      }
    }

    private void GetTrayIcons()
    {
      this.sun = Resources.sun;
      this.clouds = Resources.clouds;
      this.snow = Resources.snow;
      this.rain = Resources.rain;
      this.tstorm = Resources.tstorm;
      if (File.Exists(Path.Combine(Application.StartupPath, "sun.ico")))
        this.sun = new Icon(Path.Combine(Application.StartupPath, "sun.ico"));
      if (File.Exists(Path.Combine(Application.StartupPath, "clouds.ico")))
        this.clouds = new Icon(Path.Combine(Application.StartupPath, "clouds.ico"));
      if (File.Exists(Path.Combine(Application.StartupPath, "snow.ico")))
        this.snow = new Icon(Path.Combine(Application.StartupPath, "snow.ico"));
      if (File.Exists(Path.Combine(Application.StartupPath, "rain.ico")))
        this.rain = new Icon(Path.Combine(Application.StartupPath, "rain.ico"));
      if (!File.Exists(Path.Combine(Application.StartupPath, "tstorm.ico")))
        return;
      this.tstorm = new Icon(Path.Combine(Application.StartupPath, "tstorm.ico"));
    }

    private void GetLocations()
    {
      this.pageIndicator.Pages = 0;
      this.pageIndicator.CurrentPage = 0;
      this.flowLayoutPanel1.Controls.Clear();
      this.locations.Clear();
      this.locations.Add(Forecaster.Properties.Settings.Default.LocalLocation);
      ++this.pageIndicator.Pages;
      string additionalLocations = Forecaster.Properties.Settings.Default.AdditionalLocations;
      char[] chArray = new char[1]{ '|' };
      foreach (string str in additionalLocations.Split(chArray))
      {
        if (!string.IsNullOrEmpty(str))
        {
          this.locations.Add(str);
          ++this.pageIndicator.Pages;
        }
      }
    }

    private void GetForecasts()
    {
      this.isUpdating = true;
      this.SetNotifyIconText(this.notifyIcon1, "Updating forecasts...");
      this.forecasts.Clear();
      if (!Helper.CheckForInternetConnection())
      {
        this.SetNotifyIconText(this.notifyIcon1, "No internet connection found...");
        this.Reset();
      }
      else
      {
        foreach (string location in this.locations)
          this.forecasts.Add(Weather.GetConditions(location));
        this.isUpdating = false;
        this.GetCurrentConditions();
        this.GetExtendedConditions();
      }
    }

    private void GetForecastButtons()
    {
      for (int forecastIndex = 0; forecastIndex < 5; ++forecastIndex)
        this.flowLayoutPanel1.Controls.Add((Control) this.GetForecastButton(forecastIndex));
    }

    private TaskButton GetForecastButton(int forecastIndex)
    {
      TaskButton forecastButton = new TaskButton();
      forecastButton.Temperature = string.Empty;
      forecastButton.Font = this.Font;
      forecastButton.Height = this.flowLayoutPanel1.Height - 4;
      forecastButton.Image = (Bitmap) null;
      forecastButton.ImageSize = 72;
      forecastButton.Margin = new Padding(2, 0, 0, 0);
      forecastButton.Name = string.Empty;
      forecastButton.TextColor = SystemColors.WindowText;
      forecastButton.Condition = string.Empty;
      forecastButton.Direction = TaskButton.Orientation.Vertical;
      forecastButton.Width = 85;
      forecastButton.BringToFront();
      return forecastButton;
    }

    private void GetCurrentConditions()
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new PremiumForm.GetCurrentConditionsDelegate(this.GetCurrentConditions), new object[0]);
      }
      else
      {
        int currentPage = this.pageIndicator.CurrentPage;
        if (currentPage > this.forecasts.Count)
          return;
        try
        {
          if (this.forecasts[currentPage] == null)
            return;
          Conditions current = this.forecasts[currentPage].Current;
          this.headerPanel1.City = string.Empty;
          this.headerPanel1.Condition = string.Empty;
          this.headerPanel1.Temperature = string.Empty;
          this.headerPanel1.City = current.City;
          this.headerPanel1.Condition = string.Format("Wind: {0} {1} KM/h{4}Humidity: {2}{4}{3}", (object) current.WindDirection, (object) current.WindSpeed, (object) current.Humidity, (object) current.Condition, (object) Environment.NewLine);
          this.headerPanel1.Temperature = Helper.GetCurrentTemperature(current);
          string conditionImagePath = Helper.GetConditionImagePath(current, true);
          if (File.Exists(conditionImagePath) && !string.IsNullOrEmpty(conditionImagePath))
            this.headerPanel1.WeatherImage = (Bitmap) Image.FromFile(conditionImagePath);
          this.UpdateSystemTray(current);
          this.oldCondition = current;
        }
        catch
        {
        }
      }
    }

    private void GetExtendedConditions()
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new PremiumForm.GetExtendedForecastDelegate(this.GetExtendedConditions), new object[0]);
      }
      else
      {
        int currentPage = this.pageIndicator.CurrentPage;
        if (currentPage > this.forecasts.Count)
          return;
        try
        {
          if (this.forecasts[currentPage] == null)
            return;
          List<Conditions> extended = this.forecasts[currentPage].Extended;
          for (int index = 0; index < this.flowLayoutPanel1.Controls.Count; ++index)
          {
            TaskButton control = this.flowLayoutPanel1.Controls[index] as TaskButton;
            control.Visible = true;
            control.Condition = extended[index].Condition;
            control.Temperature = string.Format("High: {0}\nLow: {1}", (object) Helper.GetTemperature(extended[index], extended[index].High), (object) Helper.GetTemperature(extended[index], extended[index].Low));
            control.Day = extended[index].DayOfWeek;
            if (control.Image != null)
            {
              control.Image.Dispose();
              control.Image = (Bitmap) null;
            }
            string conditionImagePath = Helper.GetConditionImagePath(extended[index], false);
            if (File.Exists(conditionImagePath) && !string.IsNullOrEmpty(conditionImagePath))
              control.Image = (Bitmap) Image.FromFile(conditionImagePath);
          }
        }
        catch
        {
        }
      }
    }

    private void UpdateSystemTray(Conditions conditions)
    {
      switch (conditions.Icon)
      {
        case "143":
        case "176":
        case "263":
        case "266":
        case "293":
        case "296":
        case "299":
        case "302":
        case "305":
        case "308":
        case "353":
        case "356":
          this.notifyIcon1.Icon = this.rain;
          break;
        case "179":
        case "182":
        case "185":
        case "227":
        case "230":
        case "281":
        case "284":
        case "311":
        case "314":
        case "317":
        case "320":
        case "323":
        case "326":
        case "329":
        case "335":
        case "338":
        case "350":
        case "362":
        case "365":
        case "368":
        case "371":
        case "374":
        case "377":
        case "392":
        case "395":
          this.notifyIcon1.Icon = this.snow;
          break;
        case "200":
        case "359":
        case "386":
        case "389":
          this.notifyIcon1.Icon = this.tstorm;
          break;
        case "113":
        case "116":
          this.notifyIcon1.Icon = this.sun;
          break;
        case "119":
        case "122":
        case "248":
          this.notifyIcon1.Icon = this.clouds;
          break;
        default:
          this.notifyIcon1.Icon = this.rain;
          break;
      }
      this.SetNotifyIconText(this.notifyIcon1, string.Format("{0}\n{1} {2}", (object) conditions.City, (object) Helper.GetCurrentTemperature(conditions), (object) conditions.Condition));
      if (!Forecaster.Properties.Settings.Default.ShowChangedConditions || this.oldCondition == null || !this.oldCondition.City.Equals(conditions.City) || this.oldCondition.Condition.Equals(conditions.Condition))
        return;
      this.notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
      this.notifyIcon1.BalloonTipTitle = string.Format("Weather update for {0}", (object) conditions.City);
      this.notifyIcon1.BalloonTipText = string.Format("Now calling for {0}", (object) conditions.Condition);
      this.notifyIcon1.ShowBalloonTip(1000);
    }

    public void SetNotifyIconText(NotifyIcon ni, string text)
    {
      if (text.Length >= 128)
        throw new ArgumentOutOfRangeException("Text limited to 127 characters");
      Type type = typeof (NotifyIcon);
      BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.NonPublic;
      type.GetField(nameof (text), bindingAttr).SetValue((object) ni, (object) text);
      if (!(bool) type.GetField("added", bindingAttr).GetValue((object) ni))
        return;
      type.GetMethod("UpdateIcon", bindingAttr).Invoke((object) ni, new object[1]
      {
        (object) true
      });
    }

    private void Reset()
    {
      if (this.headerPanel1.WeatherImage != null)
      {
        this.headerPanel1.WeatherImage.Dispose();
        this.headerPanel1.WeatherImage = (Bitmap) null;
      }
      this.headerPanel1.City = string.Empty;
      this.headerPanel1.Condition = string.Empty;
      this.headerPanel1.Temperature = string.Empty;
      foreach (Control control in (ArrangedElementCollection) this.flowLayoutPanel1.Controls)
        control.Visible = false;
    }

    private void OnActivityTimer(object obj)
    {
      this.CheckVisualStyle();
      this.CheckForUpdate();
      this.GetWindowPostion();
      this.SuspendLayout();
      this.GetForecasts();
      this.ResumeLayout(true);
    }

    private void PageIndicator_OnPageChanged(object sender, OnPageChangedArgs fe)
    {
      if (this.headerPanel1.WeatherImage != null)
      {
        this.headerPanel1.WeatherImage.Dispose();
        this.headerPanel1.WeatherImage = (Bitmap) null;
      }
      this.headerPanel1.City = string.Empty;
      this.headerPanel1.Condition = string.Empty;
      this.headerPanel1.Temperature = string.Empty;
      this.SuspendLayout();
      this.GetCurrentConditions();
      this.GetExtendedConditions();
      this.ResumeLayout();
    }

    private void pageIndicator_MouseMove(object sender, MouseEventArgs e)
    {
      if (this.isUpdating)
        return;
      try
      {
        for (int index = 0; index < this.pageIndicator.IndicatorRectangles.Count; ++index)
        {
          if (this.pageIndicator.IndicatorRectangles[index].Contains((PointF) e.Location))
          {
            if (this.lastTooltip == index)
              break;
            this.toolTip1.Show(this.forecasts[index].Current.City, (IWin32Window) this.pageIndicator, e.X + 10, e.Y + 10, 2500);
            this.lastTooltip = index;
            break;
          }
        }
      }
      catch
      {
      }
    }

    private void pageIndicator_MouseLeave(object sender, EventArgs e) => this.toolTip1.Hide((IWin32Window) this.pageIndicator);

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      using (Settings settings = new Settings())
      {
        if (Forecaster.Properties.Settings.Default.SettingPosition != new Point(0, 0))
        {
          settings.StartPosition = FormStartPosition.Manual;
          settings.Location = Forecaster.Properties.Settings.Default.SettingPosition;
        }
        else
          settings.StartPosition = FormStartPosition.WindowsDefaultLocation;
        if (settings.ShowDialog() != DialogResult.OK)
          return;
        this.GetLocations();
        this.GetForecastButtons();
        this.GetForecasts();
        this.networkActivityTimer.Change(1000, Forecaster.Properties.Settings.Default.UpdateInterval);
      }
    }

    private void menuItem1_Click(object sender, EventArgs e) => Application.Exit();

    private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
    {
      if (e.Button != MouseButtons.Left)
        return;
      if (this.Visible)
      {
        this.Hide();
      }
      else
      {
        this.Show();
        this.Activate();
      }
    }

    private void notifyIcon1_BalloonTipClicked(object sender, EventArgs e)
    {
      if (!this.updateAvailable)
        return;
      Process.Start(this.url);
    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {
      if (!Application.RenderWithVisualStyles || this.footerRenderer == null)
        return;
      this.footerRenderer.DrawBackground((IDeviceContext) e.Graphics, this.panel_Footer_Container.ClientRectangle);
    }

    private void panel2_Paint(object sender, PaintEventArgs e)
    {
    }

    private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
    {
      if (!this.isConnected)
      {
        Rectangle clientRectangle = this.flowLayoutPanel1.ClientRectangle;
        clientRectangle.Inflate(-20, -20);
        TextRenderer.DrawText((IDeviceContext) e.Graphics, "Forecast is not available. Please check your internet connection and try again.", this.Font, clientRectangle, SystemColors.WindowText, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
      }
      if (!this.isUpdating)
        return;
      Rectangle clientRectangle1 = this.flowLayoutPanel1.ClientRectangle;
      clientRectangle1.Inflate(-20, -20);
      TextRenderer.DrawText((IDeviceContext) e.Graphics, "Forecast is fetching information...", this.Font, clientRectangle1, SystemColors.WindowText, TextFormatFlags.HorizontalCenter | TextFormatFlags.NoClipping | TextFormatFlags.VerticalCenter | TextFormatFlags.WordBreak);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (PremiumForm));
      this.notifyIcon1 = new NotifyIcon(this.components);
      this.NetworkActivityTimer = new System.Windows.Forms.Timer(this.components);
      this.panel_Footer_Container = new Panel();
      this.linkLabel1 = new LinkLabel();
      this.contextMenu1 = new ContextMenu();
      this.menuItem1 = new MenuItem();
      this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
      this.flowLayoutPanel1 = new FlowLayoutPanel();
      this.panel1 = new Panel();
      this.pageIndicator = new PageIndicator();
      this.headerPanel1 = new HeaderPanel();
      this.panel_Footer_Container.SuspendLayout();
      this.panel1.SuspendLayout();
      this.SuspendLayout();
      componentResourceManager.ApplyResources((object) this.notifyIcon1, "notifyIcon1");
      this.notifyIcon1.BalloonTipClicked += new EventHandler(this.notifyIcon1_BalloonTipClicked);
      this.notifyIcon1.MouseClick += new MouseEventHandler(this.notifyIcon1_MouseClick);
      this.NetworkActivityTimer.Interval = 1000;
      this.panel_Footer_Container.BackColor = SystemColors.Control;
      this.panel_Footer_Container.Controls.Add((Control) this.linkLabel1);
      componentResourceManager.ApplyResources((object) this.panel_Footer_Container, "panel_Footer_Container");
      this.panel_Footer_Container.Name = "panel_Footer_Container";
      this.panel_Footer_Container.Paint += new PaintEventHandler(this.panel1_Paint);
      this.linkLabel1.ActiveLinkColor = SystemColors.HotTrack;
      this.linkLabel1.BackColor = Color.Transparent;
      componentResourceManager.ApplyResources((object) this.linkLabel1, "linkLabel1");
      this.linkLabel1.LinkBehavior = LinkBehavior.HoverUnderline;
      this.linkLabel1.LinkColor = SystemColors.HotTrack;
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.TabStop = true;
      this.linkLabel1.VisitedLinkColor = SystemColors.HotTrack;
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.contextMenu1.MenuItems.AddRange(new MenuItem[1]
      {
        this.menuItem1
      });
      this.menuItem1.Index = 0;
      componentResourceManager.ApplyResources((object) this.menuItem1, "menuItem1");
      this.menuItem1.Click += new EventHandler(this.menuItem1_Click);
      this.flowLayoutPanel1.BackColor = SystemColors.Window;
      componentResourceManager.ApplyResources((object) this.flowLayoutPanel1, "flowLayoutPanel1");
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Paint += new PaintEventHandler(this.flowLayoutPanel1_Paint);
      this.panel1.Controls.Add((Control) this.flowLayoutPanel1);
      this.panel1.Controls.Add((Control) this.pageIndicator);
      componentResourceManager.ApplyResources((object) this.panel1, "panel1");
      this.panel1.Name = "panel1";
      this.pageIndicator.CurrentPage = 0;
      componentResourceManager.ApplyResources((object) this.pageIndicator, "pageIndicator");
      this.pageIndicator.IndicatorRectangles = (List<RectangleF>) componentResourceManager.GetObject("pageIndicator.IndicatorRectangles");
      this.pageIndicator.IndicatorSize = new Size(20, 20);
      this.pageIndicator.Name = "pageIndicator";
      this.pageIndicator.Pages = 0;
      this.pageIndicator.MouseLeave += new EventHandler(this.pageIndicator_MouseLeave);
      this.pageIndicator.MouseMove += new MouseEventHandler(this.pageIndicator_MouseMove);
      this.headerPanel1.AppVersion = HeaderPanel.Version.Basic;
      componentResourceManager.ApplyResources((object) this.headerPanel1, "headerPanel1");
      this.headerPanel1.City = "";
      this.headerPanel1.Condition = "";
      this.headerPanel1.Name = "headerPanel1";
      this.headerPanel1.Temperature = (string) null;
      this.headerPanel1.WeatherImage = (Bitmap) null;
      componentResourceManager.ApplyResources((object) this, "$this");
      this.AutoScaleMode = AutoScaleMode.Font;
      this.BackColor = SystemColors.Window;
      this.ControlBox = false;
      this.Controls.Add((Control) this.panel1);
      this.Controls.Add((Control) this.headerPanel1);
      this.Controls.Add((Control) this.panel_Footer_Container);
      this.DoubleBuffered = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (PremiumForm);
      this.ShowInTaskbar = false;
      this.TopMost = true;
      this.Activated += new EventHandler(this.Form1_Activated);
      this.Deactivate += new EventHandler(this.Form1_Deactivate);
      this.FormClosing += new FormClosingEventHandler(this.Form1_FormClosing);
      this.Load += new EventHandler(this.Form1_Load);
      this.Shown += new EventHandler(this.Form1_Shown);
      this.panel_Footer_Container.ResumeLayout(false);
      this.panel1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private delegate void GetCurrentConditionsDelegate();

    private delegate void GetExtendedForecastDelegate();
  }
}
