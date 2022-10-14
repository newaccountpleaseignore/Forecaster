using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Forecaster.Properties
{
  [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
  [CompilerGenerated]
  [DebuggerNonUserCode]
  internal class Resources
  {
    private static ResourceManager resourceMan;
    private static CultureInfo resourceCulture;

    internal Resources()
    {
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static ResourceManager ResourceManager
    {
      get
      {
        if (object.ReferenceEquals((object) Forecaster.Properties.Resources.resourceMan, (object) null))
          Forecaster.Properties.Resources.resourceMan = new ResourceManager("Forecaster.Properties.Resources", typeof (Forecaster.Properties.Resources).Assembly);
        return Forecaster.Properties.Resources.resourceMan;
      }
    }

    [EditorBrowsable(EditorBrowsableState.Advanced)]
    internal static CultureInfo Culture
    {
      get => Forecaster.Properties.Resources.resourceCulture;
      set => Forecaster.Properties.Resources.resourceCulture = value;
    }

    internal static Icon clouds => (Icon) Forecaster.Properties.Resources.ResourceManager.GetObject(nameof (clouds), Forecaster.Properties.Resources.resourceCulture);

    internal static Icon rain => (Icon) Forecaster.Properties.Resources.ResourceManager.GetObject(nameof (rain), Forecaster.Properties.Resources.resourceCulture);

    internal static Icon snow => (Icon) Forecaster.Properties.Resources.ResourceManager.GetObject(nameof (snow), Forecaster.Properties.Resources.resourceCulture);

    internal static Icon sun => (Icon) Forecaster.Properties.Resources.ResourceManager.GetObject(nameof (sun), Forecaster.Properties.Resources.resourceCulture);

    internal static Icon tstorm => (Icon) Forecaster.Properties.Resources.ResourceManager.GetObject(nameof (tstorm), Forecaster.Properties.Resources.resourceCulture);
  }
}
