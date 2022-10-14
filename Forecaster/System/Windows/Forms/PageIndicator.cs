using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace System.Windows.Forms
{
  public class PageIndicator : UserControl
  {
    private IContainer components;
    private int pages = 1;
    private int currentPage;
    private Size indicatorSize = new Size(16, 16);
    private List<RectangleF> indicatorRectangles = new List<RectangleF>();

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
      this.Name = nameof (PageIndicator);
      this.Size = new Size(268, 35);
      this.Load += new EventHandler(this.PageIndicator_Load);
      this.ResumeLayout(false);
    }

    public static event PageIndicator.OnPageChangedHandler OnPageChanged;

    public int Pages
    {
      get => this.pages;
      set
      {
        if (this.pages == value)
          return;
        this.pages = value;
        this.GetRectangles();
        this.Invalidate();
      }
    }

    public int CurrentPage
    {
      get => this.currentPage;
      set
      {
        if (this.currentPage == value)
          return;
        this.currentPage = value;
        this.Invalidate();
      }
    }

    public Size IndicatorSize
    {
      get => this.indicatorSize;
      set
      {
        if (!(this.indicatorSize != value))
          return;
        this.indicatorSize = value;
        this.Invalidate();
      }
    }

    public List<RectangleF> IndicatorRectangles
    {
      get => this.indicatorRectangles;
      set
      {
      }
    }

    public PageIndicator()
    {
      this.InitializeComponent();
      this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
      this.SetStyle(ControlStyles.UserPaint, true);
      this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
      this.DoubleBuffered = true;
    }

    private void PageIndicator_Load(object sender, EventArgs e) => this.GetRectangles();

    private void GetRectangles()
    {
      this.indicatorRectangles.Clear();
      RectangleF rectangleF1 = new RectangleF(new PointF((float) ((double) (this.Width / 2) - (double) (this.indicatorSize.Width * this.pages) / 2.0 - 1.0), (float) (this.Height / 2 - this.indicatorSize.Height / 2)), new SizeF((float) (this.pages * this.indicatorSize.Width), (float) this.indicatorSize.Height));
      for (int index = 0; index < this.pages; ++index)
      {
        RectangleF rectangleF2 = new RectangleF(new PointF(rectangleF1.X + (float) (index * this.indicatorSize.Width), rectangleF1.Y), (SizeF) this.indicatorSize);
        rectangleF2.Inflate(-4f, -4f);
        rectangleF2.Offset(2f, 2f);
        this.indicatorRectangles.Add(rectangleF2);
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      for (int index = 0; index < this.indicatorRectangles.Count; ++index)
      {
        if (index == this.currentPage)
          e.Graphics.FillRectangle(Brushes.DodgerBlue, this.indicatorRectangles[index]);
        else if (this.indicatorRectangles[index].Contains((PointF) this.PointToClient(Cursor.Position)))
          e.Graphics.FillRectangle(Brushes.Gray, this.indicatorRectangles[index]);
        else
          e.Graphics.FillRectangle(Brushes.LightGray, this.indicatorRectangles[index]);
      }
    }

    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      this.Invalidate();
    }

    protected override void OnMouseClick(MouseEventArgs e)
    {
      base.OnMouseClick(e);
      if (e.Button == MouseButtons.XButton2)
      {
        ++this.currentPage;
        if (this.currentPage > this.pages - 1)
          this.currentPage = 0;
      }
      else if (e.Button == MouseButtons.XButton1)
      {
        --this.currentPage;
        if (this.currentPage < 0)
          this.currentPage = this.pages - 1;
      }
      else if (e.Button == MouseButtons.Left)
      {
        for (int index = 0; index < this.indicatorRectangles.Count; ++index)
        {
          if (this.indicatorRectangles[index].Contains((PointF) e.Location))
          {
            this.currentPage = index;
            break;
          }
        }
      }
      this.Invalidate();
      PageIndicator.OnPageChanged((object) this, new OnPageChangedArgs(this.currentPage));
    }

    protected override void OnSizeChanged(EventArgs e)
    {
      base.OnSizeChanged(e);
      this.GetRectangles();
      this.Invalidate();
    }

    public delegate void OnPageChangedHandler(object sender, OnPageChangedArgs fe);
  }
}
