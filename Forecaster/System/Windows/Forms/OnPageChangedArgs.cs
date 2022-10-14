namespace System.Windows.Forms
{
  public class OnPageChangedArgs : EventArgs
  {
    public int Page;

    public OnPageChangedArgs(int page) => this.Page = page;
  }
}
