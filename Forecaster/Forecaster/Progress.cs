using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Net;
using System.Windows.Forms;

namespace Forecaster
{
  public class Progress : Form
  {
    private IContainer components;
    private Label label1;
    private Label label2;
    private ProgressBar progressBar1;
    private Label label3;
    private string url = string.Empty;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.label1 = new Label();
      this.label2 = new Label();
      this.progressBar1 = new ProgressBar();
      this.label3 = new Label();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(11, 14);
      this.label1.Margin = new Padding(4, 0, 4, 0);
      this.label1.Name = "label1";
      this.label1.Size = new Size(50, 20);
      this.label1.TabIndex = 0;
      this.label1.Text = "label1";
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(12, 34);
      this.label2.Name = "label2";
      this.label2.Size = new Size(38, 15);
      this.label2.TabIndex = 1;
      this.label2.Text = "label2";
      this.progressBar1.Location = new Point(12, 89);
      this.progressBar1.Name = "progressBar1";
      this.progressBar1.Size = new Size(414, 23);
      this.progressBar1.Style = ProgressBarStyle.Continuous;
      this.progressBar1.TabIndex = 2;
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(12, 71);
      this.label3.Name = "label3";
      this.label3.Size = new Size(38, 15);
      this.label3.TabIndex = 3;
      this.label3.Text = "label3";
      this.AutoScaleDimensions = new SizeF(8f, 20f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(438, 130);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.progressBar1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Font = new Font("Segoe UI", 11.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Margin = new Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = nameof (Progress);
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.Text = nameof (Progress);
      this.Load += new EventHandler(this.Progress_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public string URL
    {
      get => this.url;
      set
      {
        if (!(this.url != value))
          return;
        this.url = value;
      }
    }

    public Progress() => this.InitializeComponent();

    private void Progress_Load(object sender, EventArgs e)
    {
      this.label1.Text = "Downloading new version...";
      this.label2.Text = "Network Activity Indicator is now downloading a new version";
      this.UpdateApplication();
    }

    private void UpdateApplication()
    {
      try
      {
        using (WebClient webClient = new WebClient())
        {
          webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(this.client_DownloadProgressChanged);
          webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(this.client_DownloadFileCompleted);
          webClient.DownloadFileAsync(new Uri(this.url), Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "Network Activity Indicator.zip"));
        }
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("Cannot download the latest version of the application. \n\nPlease check your network connection, or try again later. Error: " + ex.Message);
      }
    }

    private void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
    {
      this.label3.Text = string.Format("Downloading: {0} of {1}", (object) this.GetFormattedBytes((ulong) e.BytesReceived), (object) this.GetFormattedBytes((ulong) e.TotalBytesToReceive));
      this.progressBar1.Value = e.ProgressPercentage;
    }

    private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
    {
      this.progressBar1.Value = 100;
      this.Close();
    }

    public string GetFormattedBytes(ulong bytes)
    {
      string[] strArray = new string[4]
      {
        "GB",
        "MB",
        "KB",
        "Bytes"
      };
      ulong d2 = (ulong) Math.Pow(1024.0, (double) (strArray.Length - 1));
      foreach (string str in strArray)
      {
        if (bytes > d2)
          return string.Format("{0:##.##} {1}", (object) Decimal.Divide((Decimal) bytes, (Decimal) d2), (object) str);
        d2 /= 1024UL;
      }
      return "0 Bytes";
    }
  }
}
