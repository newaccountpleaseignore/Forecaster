using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WeatherAPI;

namespace Forecaster
{
  public class Settings : Form
  {
    private IContainer components;
    private TabControl tabControl1;
    private TabPage tabPage1;
    private TabPage tabPage2;
    private Button button_Apply;
    private Button button_Cancel;
    private Button button_OK;
    private Button button4;
    private Label label2;
    private Label label1;
    private PictureBox pictureBox1;
    private Label label5;
    private LabelDivider labelDivider1;
    private ListBox listBox1;
    private Label label4;
    private Label label3;
    private ComboBox comboBox1;
    private CheckBox checkBox1;
    private Label label6;
    private Button button_Add;
    private Button button_Remove;
    private TabPage tabPage3;
    private CheckBox checkBox_OverrideTempUnits;
    private LabelDivider labelDivider3;
    private LabelDivider labelDivider2;
    private CheckBox checkBox_AutoStartup;
    private ComboBox comboBox_TempUnits;
    private Label label7;
    private Label label9;
    private Label label_Valid;
    private Button button_Validate;
    private HintTextBox hintTextBox1;
    private LabelDivider labelDivider4;
    private Label label8;
    private string oldLocalLocation = Forecaster.Properties.Settings.Default.LocalLocation;
    private int oldUpdateInterval = Forecaster.Properties.Settings.Default.UpdateInterval;
    private string oldAdditonLocations = Forecaster.Properties.Settings.Default.AdditionalLocations;
    private string oldTempUnits = Forecaster.Properties.Settings.Default.TempUnit;
    private bool oldAutoStartup = Forecaster.Properties.Settings.Default.AutoStartup;

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.tabControl1 = new TabControl();
      this.tabPage1 = new TabPage();
      this.label7 = new Label();
      this.comboBox1 = new ComboBox();
      this.checkBox1 = new CheckBox();
      this.label6 = new Label();
      this.label4 = new Label();
      this.label3 = new Label();
      this.labelDivider1 = new LabelDivider();
      this.button4 = new Button();
      this.label2 = new Label();
      this.label1 = new Label();
      this.pictureBox1 = new PictureBox();
      this.tabPage2 = new TabPage();
      this.label8 = new Label();
      this.button_Add = new Button();
      this.button_Remove = new Button();
      this.listBox1 = new ListBox();
      this.label5 = new Label();
      this.tabPage3 = new TabPage();
      this.label9 = new Label();
      this.label_Valid = new Label();
      this.button_Validate = new Button();
      this.hintTextBox1 = new HintTextBox();
      this.labelDivider4 = new LabelDivider();
      this.checkBox_AutoStartup = new CheckBox();
      this.comboBox_TempUnits = new ComboBox();
      this.checkBox_OverrideTempUnits = new CheckBox();
      this.labelDivider3 = new LabelDivider();
      this.labelDivider2 = new LabelDivider();
      this.button_Apply = new Button();
      this.button_Cancel = new Button();
      this.button_OK = new Button();
      this.tabControl1.SuspendLayout();
      this.tabPage1.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.tabPage2.SuspendLayout();
      this.tabPage3.SuspendLayout();
      this.SuspendLayout();
      this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.tabControl1.Controls.Add((Control) this.tabPage1);
      this.tabControl1.Controls.Add((Control) this.tabPage2);
      this.tabControl1.Controls.Add((Control) this.tabPage3);
      this.tabControl1.Location = new Point(7, 7);
      this.tabControl1.Margin = new Padding(1);
      this.tabControl1.Name = "tabControl1";
      this.tabControl1.SelectedIndex = 0;
      this.tabControl1.Size = new Size(435, 400);
      this.tabControl1.TabIndex = 0;
      this.tabPage1.Controls.Add((Control) this.label7);
      this.tabPage1.Controls.Add((Control) this.comboBox1);
      this.tabPage1.Controls.Add((Control) this.checkBox1);
      this.tabPage1.Controls.Add((Control) this.label6);
      this.tabPage1.Controls.Add((Control) this.label4);
      this.tabPage1.Controls.Add((Control) this.label3);
      this.tabPage1.Controls.Add((Control) this.labelDivider1);
      this.tabPage1.Controls.Add((Control) this.button4);
      this.tabPage1.Controls.Add((Control) this.label2);
      this.tabPage1.Controls.Add((Control) this.label1);
      this.tabPage1.Controls.Add((Control) this.pictureBox1);
      this.tabPage1.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.tabPage1.Location = new Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new Padding(3);
      this.tabPage1.Size = new Size(427, 374);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "Local Forecast";
      this.tabPage1.UseVisualStyleBackColor = true;
      this.label7.AutoSize = true;
      this.label7.Location = new Point(222, 344);
      this.label7.Name = "label7";
      this.label7.Size = new Size(189, 15);
      this.label7.TabIndex = 13;
      this.label7.Text = "Powered by World Weather Online";
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FlatStyle = FlatStyle.System;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Items.AddRange(new object[4]
      {
        (object) "Every 15 minutes",
        (object) "Every 30 minutes",
        (object) "Every hour",
        (object) "Every 6 hours"
      });
      this.comboBox1.Location = new Point(250, 208);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(161, 23);
      this.comboBox1.TabIndex = 11;
      this.checkBox1.AutoSize = true;
      this.checkBox1.Location = new Point(30, 285);
      this.checkBox1.Name = "checkBox1";
      this.checkBox1.Size = new Size(253, 19);
      this.checkBox1.TabIndex = 10;
      this.checkBox1.Text = "Notify me when current conditions change";
      this.checkBox1.UseVisualStyleBackColor = true;
      this.label6.Location = new Point(27, 242);
      this.label6.Name = "label6";
      this.label6.Size = new Size(384, 37);
      this.label6.TabIndex = 9;
      this.label6.Text = "Adjust the time Forecaster waits before attempting to update the weather information. An internet connection is required.";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(188, 89);
      this.label4.Name = "label4";
      this.label4.Size = new Size(60, 15);
      this.label4.TabIndex = 8;
      this.label4.Text = "Humidity:";
      this.label3.AutoSize = true;
      this.label3.Location = new Point(188, 74);
      this.label3.Name = "label3";
      this.label3.Size = new Size(38, 15);
      this.label3.TabIndex = 7;
      this.label3.Text = "Wind:";
      this.labelDivider1.Label = "Update frequency";
      this.labelDivider1.Location = new Point(11, 178);
      this.labelDivider1.Name = "labelDivider1";
      this.labelDivider1.Size = new Size(400, 24);
      this.labelDivider1.TabIndex = 6;
      this.button4.Location = new Point(250, 138);
      this.button4.Name = "button4";
      this.button4.Size = new Size(161, 23);
      this.button4.TabIndex = 5;
      this.button4.Text = "Change location...";
      this.button4.UseVisualStyleBackColor = true;
      this.button4.MouseClick += new MouseEventHandler(this.button4_MouseClick);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(188, 59);
      this.label2.Name = "label2";
      this.label2.Size = new Size(68, 15);
      this.label2.TabIndex = 2;
      this.label2.Text = "Conditions:";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(188, 32);
      this.label1.Name = "label1";
      this.label1.Size = new Size(56, 15);
      this.label1.TabIndex = 1;
      this.label1.Text = "Location:";
      this.pictureBox1.Location = new Point(6, 6);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(176, 176);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 0;
      this.pictureBox1.TabStop = false;
      this.tabPage2.Controls.Add((Control) this.label8);
      this.tabPage2.Controls.Add((Control) this.button_Add);
      this.tabPage2.Controls.Add((Control) this.button_Remove);
      this.tabPage2.Controls.Add((Control) this.listBox1);
      this.tabPage2.Controls.Add((Control) this.label5);
      this.tabPage2.Location = new Point(4, 22);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new Padding(3);
      this.tabPage2.Size = new Size(427, 374);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "Additional Forecasts";
      this.tabPage2.UseVisualStyleBackColor = true;
      this.label8.Location = new Point(20, 330);
      this.label8.Name = "label8";
      this.label8.Size = new Size(387, 27);
      this.label8.TabIndex = 4;
      this.label8.Text = "Only 1 additional location is available in the basic version. If you wish to add more, please upgrade to the premium version.";
      this.button_Add.Location = new Point(251, 292);
      this.button_Add.Name = "button_Add";
      this.button_Add.Size = new Size(75, 23);
      this.button_Add.TabIndex = 3;
      this.button_Add.Text = "Add";
      this.button_Add.UseVisualStyleBackColor = true;
      this.button_Add.MouseClick += new MouseEventHandler(this.button_Add_MouseClick);
      this.button_Remove.Location = new Point(332, 292);
      this.button_Remove.Name = "button_Remove";
      this.button_Remove.Size = new Size(75, 23);
      this.button_Remove.TabIndex = 2;
      this.button_Remove.Text = "Remove";
      this.button_Remove.UseVisualStyleBackColor = true;
      this.button_Remove.MouseClick += new MouseEventHandler(this.button_Remove_MouseClick);
      this.listBox1.FormattingEnabled = true;
      this.listBox1.Location = new Point(20, 61);
      this.listBox1.Name = "listBox1";
      this.listBox1.SelectionMode = SelectionMode.MultiExtended;
      this.listBox1.Size = new Size(387, 225);
      this.listBox1.TabIndex = 1;
      this.label5.Location = new Point(20, 21);
      this.label5.Name = "label5";
      this.label5.Size = new Size(387, 34);
      this.label5.TabIndex = 0;
      this.label5.Text = "Additional forecasts can display the weather in other locations. You can view them by clicking on the system tray icon.";
      this.tabPage3.Controls.Add((Control) this.label9);
      this.tabPage3.Controls.Add((Control) this.label_Valid);
      this.tabPage3.Controls.Add((Control) this.button_Validate);
      this.tabPage3.Controls.Add((Control) this.hintTextBox1);
      this.tabPage3.Controls.Add((Control) this.labelDivider4);
      this.tabPage3.Controls.Add((Control) this.checkBox_AutoStartup);
      this.tabPage3.Controls.Add((Control) this.comboBox_TempUnits);
      this.tabPage3.Controls.Add((Control) this.checkBox_OverrideTempUnits);
      this.tabPage3.Controls.Add((Control) this.labelDivider3);
      this.tabPage3.Controls.Add((Control) this.labelDivider2);
      this.tabPage3.Location = new Point(4, 22);
      this.tabPage3.Name = "tabPage3";
      this.tabPage3.Padding = new Padding(3);
      this.tabPage3.Size = new Size(427, 374);
      this.tabPage3.TabIndex = 2;
      this.tabPage3.Text = nameof (Settings);
      this.tabPage3.UseVisualStyleBackColor = true;
      this.label9.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label9.Location = new Point(27, (int) byte.MaxValue);
      this.label9.Name = "label9";
      this.label9.Size = new Size(377, 38);
      this.label9.TabIndex = 9;
      this.label9.Text = "Enter the serial code below to unlock the premium version of Forecaster";
      this.label_Valid.Location = new Point(27, 329);
      this.label_Valid.Name = "label_Valid";
      this.label_Valid.Size = new Size(296, 29);
      this.label_Valid.TabIndex = 8;
      this.button_Validate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button_Validate.Location = new Point(329, 324);
      this.button_Validate.Name = "button_Validate";
      this.button_Validate.Size = new Size(75, 23);
      this.button_Validate.TabIndex = 7;
      this.button_Validate.Text = "Validate";
      this.button_Validate.UseVisualStyleBackColor = true;
      this.button_Validate.MouseClick += new MouseEventHandler(this.button_Validate_MouseClick);
      this.hintTextBox1.HintColor = Color.LightGray;
      this.hintTextBox1.HintText = "XXXXX-XXXXX-XXXXX-XXXXX-XXXXX";
      this.hintTextBox1.Location = new Point(27, 296);
      this.hintTextBox1.Name = "hintTextBox1";
      this.hintTextBox1.Size = new Size(377, 22);
      this.hintTextBox1.TabIndex = 6;
      this.labelDivider4.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelDivider4.Label = "Serial code validation";
      this.labelDivider4.Location = new Point(10, 220);
      this.labelDivider4.Name = "labelDivider4";
      this.labelDivider4.Size = new Size(400, 24);
      this.labelDivider4.TabIndex = 5;
      this.checkBox_AutoStartup.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.checkBox_AutoStartup.Location = new Point(30, 48);
      this.checkBox_AutoStartup.Name = "checkBox_AutoStartup";
      this.checkBox_AutoStartup.Size = new Size(374, 34);
      this.checkBox_AutoStartup.TabIndex = 4;
      this.checkBox_AutoStartup.Text = "Automatically start Forecaster when Windows starts";
      this.checkBox_AutoStartup.UseVisualStyleBackColor = true;
      this.comboBox_TempUnits.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox_TempUnits.Enabled = false;
      this.comboBox_TempUnits.FormattingEnabled = true;
      this.comboBox_TempUnits.Items.AddRange(new object[2]
      {
        (object) "Celsius",
        (object) "Fahrenheit"
      });
      this.comboBox_TempUnits.Location = new Point(250, 129);
      this.comboBox_TempUnits.Name = "comboBox_TempUnits";
      this.comboBox_TempUnits.Size = new Size(161, 21);
      this.comboBox_TempUnits.TabIndex = 3;
      this.checkBox_OverrideTempUnits.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.checkBox_OverrideTempUnits.Location = new Point(30, 169);
      this.checkBox_OverrideTempUnits.Name = "checkBox_OverrideTempUnits";
      this.checkBox_OverrideTempUnits.Size = new Size(374, 34);
      this.checkBox_OverrideTempUnits.TabIndex = 2;
      this.checkBox_OverrideTempUnits.Text = "Allow me to choose the units of measurement instead of the default one used by the system";
      this.checkBox_OverrideTempUnits.UseVisualStyleBackColor = true;
      this.checkBox_OverrideTempUnits.CheckedChanged += new EventHandler(this.checkBox_OverrideTempUnits_CheckedChanged);
      this.labelDivider3.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelDivider3.Label = "Override units of measurement";
      this.labelDivider3.Location = new Point(10, 100);
      this.labelDivider3.Name = "labelDivider3";
      this.labelDivider3.Size = new Size(400, 24);
      this.labelDivider3.TabIndex = 1;
      this.labelDivider2.Font = new Font("Segoe UI", 9f);
      this.labelDivider2.Label = "Start with Windows";
      this.labelDivider2.Location = new Point(10, 20);
      this.labelDivider2.Name = "labelDivider2";
      this.labelDivider2.Size = new Size(400, 24);
      this.labelDivider2.TabIndex = 0;
      this.button_Apply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button_Apply.Location = new Point(367, 413);
      this.button_Apply.Name = "button_Apply";
      this.button_Apply.Size = new Size(75, 23);
      this.button_Apply.TabIndex = 1;
      this.button_Apply.Text = "Apply";
      this.button_Apply.UseVisualStyleBackColor = true;
      this.button_Apply.MouseClick += new MouseEventHandler(this.button_Apply_MouseClick);
      this.button_Cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button_Cancel.DialogResult = DialogResult.Cancel;
      this.button_Cancel.Location = new Point(286, 413);
      this.button_Cancel.Name = "button_Cancel";
      this.button_Cancel.Size = new Size(75, 23);
      this.button_Cancel.TabIndex = 2;
      this.button_Cancel.Text = "Cancel";
      this.button_Cancel.UseVisualStyleBackColor = true;
      this.button_Cancel.MouseClick += new MouseEventHandler(this.button_Cancel_MouseClick);
      this.button_OK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button_OK.DialogResult = DialogResult.OK;
      this.button_OK.Location = new Point(205, 413);
      this.button_OK.Name = "button_OK";
      this.button_OK.Size = new Size(75, 23);
      this.button_OK.TabIndex = 3;
      this.button_OK.Text = "OK";
      this.button_OK.UseVisualStyleBackColor = true;
      this.button_OK.MouseClick += new MouseEventHandler(this.button_OK_MouseClick);
      this.AutoScaleDimensions = new SizeF(96f, 96f);
      this.AutoScaleMode = AutoScaleMode.Dpi;
      this.ClientSize = new Size(448, 444);
      this.Controls.Add((Control) this.button_OK);
      this.Controls.Add((Control) this.button_Cancel);
      this.Controls.Add((Control) this.button_Apply);
      this.Controls.Add((Control) this.tabControl1);
      this.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Name = nameof (Settings);
      this.Text = "Weather settings";
      this.FormClosing += new FormClosingEventHandler(this.Settings_FormClosing);
      this.Load += new EventHandler(this.Settings_Load);
      this.tabControl1.ResumeLayout(false);
      this.tabPage1.ResumeLayout(false);
      this.tabPage1.PerformLayout();
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.tabPage2.ResumeLayout(false);
      this.tabPage3.ResumeLayout(false);
      this.tabPage3.PerformLayout();
      this.ResumeLayout(false);
    }

    public Settings() => this.InitializeComponent();

    private void Settings_Load(object sender, EventArgs e) => this.LoadSettings();

    private void Settings_FormClosing(object sender, FormClosingEventArgs e) => Forecaster.Properties.Settings.Default.SettingPosition = this.Location;

    private void button4_MouseClick(object sender, MouseEventArgs e)
    {
      using (SelectLocation selectLocation = new SelectLocation())
      {
        selectLocation.StartPosition = FormStartPosition.Manual;
        if (Forecaster.Properties.Settings.Default.SelectLocationPosition != new Point(0, 0))
          selectLocation.Location = Forecaster.Properties.Settings.Default.SelectLocationPosition;
        if (selectLocation.ShowDialog() != DialogResult.OK)
          return;
        this.GetCurrentConditions(Forecaster.Properties.Settings.Default.LocalLocation);
      }
    }

    private void button_Add_MouseClick(object sender, MouseEventArgs e)
    {
      using (AddLocation addLocation = new AddLocation())
      {
        addLocation.StartPosition = FormStartPosition.Manual;
        if (Forecaster.Properties.Settings.Default.SelectLocationPosition != new Point(0, 0))
          addLocation.Location = Forecaster.Properties.Settings.Default.SelectLocationPosition;
        if (addLocation.ShowDialog() != DialogResult.OK)
          return;
        this.GetAdditionalLocation();
      }
    }

    private void button_Remove_MouseClick(object sender, MouseEventArgs e)
    {
      int selectedIndex = this.listBox1.SelectedIndex;
      if (this.listBox1.SelectedItems != null)
      {
        for (int index = this.listBox1.SelectedItems.Count - 1; index > -1; --index)
          this.listBox1.Items.Remove(this.listBox1.SelectedItems[index]);
      }
      int num1 = selectedIndex;
      int num2 = num1 - 1;
      if (num1 > 0)
      {
        ListBox listBox1 = this.listBox1;
        int num3 = num2;
        int num4 = num3 - 1;
        listBox1.SelectedIndex = num3;
      }
      else if (this.listBox1.Items.Count > 0)
        this.listBox1.SelectedIndex = 0;
      string str1 = string.Empty;
      if (this.listBox1.Items.Count > 0)
      {
        foreach (string str2 in this.listBox1.Items)
        {
          if (!string.IsNullOrEmpty(str2))
            str1 = str1 + str2 + "|";
        }
        str1.Substring(0, str1.Length - 1);
      }
      Forecaster.Properties.Settings.Default.AdditionalLocations = str1;
      this.GetAdditionalLocation();
    }

    private void checkBox_OverrideTempUnits_CheckedChanged(object sender, EventArgs e) => this.comboBox_TempUnits.Enabled = this.checkBox_OverrideTempUnits.Checked;

    private void button_Validate_MouseClick(object sender, MouseEventArgs e)
    {
      if (!Helper.Validate(this.hintTextBox1.Text, new UTF8Encoding().GetBytes("W@JAL(*&HHHK@*PP1182!*#")))
        return;
      this.label_Valid.Text = "Serial code is valid! Please restart Forecaster to use the premium version.";
    }

    private void button_OK_MouseClick(object sender, MouseEventArgs e)
    {
      this.SaveSettings();
      this.Reset();
      this.Close();
    }

    private void button_Cancel_MouseClick(object sender, MouseEventArgs e)
    {
      Forecaster.Properties.Settings.Default.LocalLocation = this.oldLocalLocation;
      Forecaster.Properties.Settings.Default.UpdateInterval = this.oldUpdateInterval;
      Forecaster.Properties.Settings.Default.AdditionalLocations = this.oldAdditonLocations;
      Forecaster.Properties.Settings.Default.AutoStartup = this.oldAutoStartup;
      Forecaster.Properties.Settings.Default.Save();
      this.Reset();
      this.Close();
    }

    private void button_Apply_MouseClick(object sender, MouseEventArgs e) => this.SaveSettings();

    private void LoadSettings()
    {
      this.GetCurrentConditions(Forecaster.Properties.Settings.Default.LocalLocation);
      switch (Forecaster.Properties.Settings.Default.UpdateInterval)
      {
        case 900000:
          this.comboBox1.SelectedItem = (object) "Every 15 minutes";
          break;
        case 1800000:
          this.comboBox1.SelectedItem = (object) "Every 30 minutes";
          break;
        case 3600000:
          this.comboBox1.SelectedItem = (object) "Every hour";
          break;
        case 21600000:
          this.comboBox1.SelectedItem = (object) "Every 6 hours";
          break;
      }
      this.checkBox1.Checked = Forecaster.Properties.Settings.Default.ShowChangedConditions;
      this.GetAdditionalLocation();
      this.checkBox_AutoStartup.Checked = Forecaster.Properties.Settings.Default.AutoStartup;
      switch (Forecaster.Properties.Settings.Default.TempUnit)
      {
        case "SI":
          this.comboBox_TempUnits.SelectedItem = (object) "Celsius";
          break;
        case "US":
          this.comboBox_TempUnits.SelectedItem = (object) "Fahrenheit";
          break;
      }
      this.checkBox_OverrideTempUnits.Checked = Forecaster.Properties.Settings.Default.OverrideTempUnit;
      this.hintTextBox1.Text = Forecaster.Properties.Settings.Default.SerialCode;
    }

    private void SaveSettings()
    {
      switch ((string) this.comboBox1.SelectedItem)
      {
        case "Every 15 minutes":
          Forecaster.Properties.Settings.Default.UpdateInterval = 900000;
          break;
        case "Every 30 minutes":
          Forecaster.Properties.Settings.Default.UpdateInterval = 1800000;
          break;
        case "Every hour":
          Forecaster.Properties.Settings.Default.UpdateInterval = 3600000;
          break;
        case "Every 6 hours":
          Forecaster.Properties.Settings.Default.UpdateInterval = 21600000;
          break;
      }
      Forecaster.Properties.Settings.Default.ShowChangedConditions = this.checkBox1.Checked;
      string str1 = string.Empty;
      if (this.listBox1.Items.Count > 0)
      {
        foreach (string str2 in this.listBox1.Items)
        {
          if (!string.IsNullOrEmpty(str2))
            str1 = str1 + str2 + "|";
        }
        str1.Substring(0, str1.Length - 1);
      }
      Forecaster.Properties.Settings.Default.AdditionalLocations = str1;
      Forecaster.Properties.Settings.Default.AutoStartup = this.checkBox_AutoStartup.Checked;
      if (this.checkBox_AutoStartup.Checked)
        StartupUtil.AddShortcutToStartupFolder();
      else
        StartupUtil.RemoveShortcutFromStartupFolder();
      switch ((string) this.comboBox_TempUnits.SelectedItem)
      {
        case "Celsius":
          Forecaster.Properties.Settings.Default.TempUnit = "SI";
          break;
        case "Fahrenheit":
          Forecaster.Properties.Settings.Default.TempUnit = "US";
          break;
      }
      Forecaster.Properties.Settings.Default.OverrideTempUnit = this.checkBox_OverrideTempUnits.Checked;
      Forecaster.Properties.Settings.Default.SerialCode = this.hintTextBox1.Text;
      Forecaster.Properties.Settings.Default.Save();
    }

    private void GetCurrentConditions(string location)
    {
      if (this.InvokeRequired)
      {
        this.Invoke((Delegate) new Settings.GetCurrentConditionsDelegate(this.GetCurrentConditions), (object) location);
      }
      else
      {
        this.Reset();
        if (!Helper.CheckForInternetConnection())
          return;
        Conditions current = Weather.GetConditions(location).Current;
        this.label1.Text = string.Format("Location: {0}", (object) current.City);
        this.label2.Text = string.Format("Conditions: {0} {1}", (object) Helper.GetTemperature(current, current.Temp), (object) current.Condition);
        this.label3.Text = string.Format("Wind: {0} {1} km/h", (object) current.WindDirection, (object) current.WindSpeed);
        this.label4.Text = string.Format("Humidity: {0}%", (object) current.Humidity);
        string conditionImagePath = Helper.GetConditionImagePath(current, true);
        if (!File.Exists(conditionImagePath) || string.IsNullOrEmpty(conditionImagePath))
          return;
        this.pictureBox1.Image = Image.FromFile(conditionImagePath);
      }
    }

    private void GetAdditionalLocation()
    {
      this.listBox1.Items.Clear();
      string additionalLocations = Forecaster.Properties.Settings.Default.AdditionalLocations;
      char[] chArray = new char[1]{ '|' };
      foreach (string str in additionalLocations.Split(chArray))
      {
        if (!string.IsNullOrEmpty(str))
          this.listBox1.Items.Add((object) str);
      }
    }

    private bool IsLocationValid(string location) => Weather.CheckLocation(location);

    private void Reset()
    {
      if (this.pictureBox1.Image != null)
      {
        this.pictureBox1.Image.Dispose();
        this.pictureBox1.Image = (Image) null;
      }
      this.label1.Text = "Location:";
      this.label2.Text = "Condition:";
      this.label3.Text = "Wind:";
      this.label4.Text = "Humidity:";
    }

    private delegate void GetCurrentConditionsDelegate(string location);
  }
}
