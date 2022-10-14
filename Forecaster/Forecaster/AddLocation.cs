using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WeatherAPI;

namespace Forecaster
{
  public class AddLocation : Form
  {
    private IContainer components;
    private Label label1;
    private Label label2;
    private Button button_OK;
    private Button button_Cancel;
    private HintTextBox hintTextBox1;
    private Label label3;

    public AddLocation() => this.InitializeComponent();

    private void SelectLocation_Load(object sender, EventArgs e)
    {
    }

    private void SelectLocation_FormClosing(object sender, FormClosingEventArgs e) => Forecaster.Properties.Settings.Default.SelectLocationPosition = this.Location;

    private void textBox1_KeyUp(object sender, KeyEventArgs e)
    {
      if (e.KeyCode != Keys.Return)
        return;
      this.DialogResult = DialogResult.OK;
      this.button_OK_MouseClick((object) null, (MouseEventArgs) null);
    }

    private void button_OK_MouseClick(object sender, MouseEventArgs e)
    {
      if (!string.IsNullOrEmpty(this.hintTextBox1.Text) && Weather.CheckLocation(this.hintTextBox1.Text))
        Forecaster.Properties.Settings.Default.AdditionalLocations = Forecaster.Properties.Settings.Default.AdditionalLocations + "|" + this.hintTextBox1.Text;
      this.Close();
    }

    private void button_Cancel_MouseClick(object sender, MouseEventArgs e) => this.Close();

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
      this.button_OK = new Button();
      this.button_Cancel = new Button();
      this.hintTextBox1 = new HintTextBox();
      this.label3 = new Label();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Location = new Point(13, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(134, 15);
      this.label1.TabIndex = 0;
      this.label1.Text = "Add additional location:";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(13, 47);
      this.label2.Name = "label2";
      this.label2.Size = new Size(56, 15);
      this.label2.TabIndex = 1;
      this.label2.Text = "Location:";
      this.button_OK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button_OK.DialogResult = DialogResult.OK;
      this.button_OK.Location = new Point(270, 144);
      this.button_OK.Name = "button_OK";
      this.button_OK.Size = new Size(75, 23);
      this.button_OK.TabIndex = 6;
      this.button_OK.Text = "OK";
      this.button_OK.UseVisualStyleBackColor = true;
      this.button_OK.MouseClick += new MouseEventHandler(this.button_OK_MouseClick);
      this.button_Cancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.button_Cancel.DialogResult = DialogResult.Cancel;
      this.button_Cancel.Location = new Point(351, 144);
      this.button_Cancel.Name = "button_Cancel";
      this.button_Cancel.Size = new Size(75, 23);
      this.button_Cancel.TabIndex = 5;
      this.button_Cancel.Text = "Cancel";
      this.button_Cancel.UseVisualStyleBackColor = true;
      this.button_Cancel.MouseClick += new MouseEventHandler(this.button_Cancel_MouseClick);
      this.hintTextBox1.HintColor = Color.DarkGray;
      this.hintTextBox1.HintText = "City, Country or ZIP Code";
      this.hintTextBox1.Location = new Point(16, 68);
      this.hintTextBox1.Name = "hintTextBox1";
      this.hintTextBox1.Size = new Size(406, 23);
      this.hintTextBox1.TabIndex = 8;
      this.label3.Location = new Point(13, 97);
      this.label3.Name = "label3";
      this.label3.Size = new Size(409, 22);
      this.label3.TabIndex = 3;
      this.label3.Text = "Enter the City, Country or ZIP code of the location you'd like to add.";
      this.AutoScaleDimensions = new SizeF(7f, 15f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.ClientSize = new Size(438, 179);
      this.Controls.Add((Control) this.hintTextBox1);
      this.Controls.Add((Control) this.button_OK);
      this.Controls.Add((Control) this.button_Cancel);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Font = new Font("Segoe UI", 9f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.FormBorderStyle = FormBorderStyle.FixedDialog;
      this.Name = nameof (AddLocation);
      this.Text = "Add Additional Location";
      this.FormClosing += new FormClosingEventHandler(this.SelectLocation_FormClosing);
      this.Load += new EventHandler(this.SelectLocation_Load);
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
