using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Forecaster.Properties
{
  [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "10.0.0.0")]
  [CompilerGenerated]
  internal sealed class Settings : ApplicationSettingsBase
  {
    private static Settings defaultInstance = (Settings) SettingsBase.Synchronized((SettingsBase) new Settings());

    public static Settings Default => Settings.defaultInstance;

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("")]
    public string AdditionalLocations
    {
      get => (string) this[nameof (AdditionalLocations)];
      set => this[nameof (AdditionalLocations)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("Werribee, Australia")]
    public string LocalLocation
    {
      get => (string) this[nameof (LocalLocation)];
      set => this[nameof (LocalLocation)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("900000")]
    [DebuggerNonUserCode]
    public int UpdateInterval
    {
      get => (int) this[nameof (UpdateInterval)];
      set => this[nameof (UpdateInterval)] = (object) value;
    }

    [UserScopedSetting]
    [DebuggerNonUserCode]
    [DefaultSettingValue("0, 0")]
    public Point SelectLocationPosition
    {
      get => (Point) this[nameof (SelectLocationPosition)];
      set => this[nameof (SelectLocationPosition)] = (object) value;
    }

    [DefaultSettingValue("0, 0")]
    [DebuggerNonUserCode]
    [UserScopedSetting]
    public Point SettingPosition
    {
      get => (Point) this[nameof (SettingPosition)];
      set => this[nameof (SettingPosition)] = (object) value;
    }

    [DebuggerNonUserCode]
    [DefaultSettingValue("False")]
    [UserScopedSetting]
    public bool ShowChangedConditions
    {
      get => (bool) this[nameof (ShowChangedConditions)];
      set => this[nameof (ShowChangedConditions)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("SI")]
    [DebuggerNonUserCode]
    public string TempUnit
    {
      get => (string) this[nameof (TempUnit)];
      set => this[nameof (TempUnit)] = (object) value;
    }

    [UserScopedSetting]
    [DefaultSettingValue("False")]
    [DebuggerNonUserCode]
    public bool OverrideTempUnit
    {
      get => (bool) this[nameof (OverrideTempUnit)];
      set => this[nameof (OverrideTempUnit)] = (object) value;
    }

    [DefaultSettingValue("False")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public bool AutoStartup
    {
      get => (bool) this[nameof (AutoStartup)];
      set => this[nameof (AutoStartup)] = (object) value;
    }

    [DefaultSettingValue("")]
    [UserScopedSetting]
    [DebuggerNonUserCode]
    public string SerialCode
    {
      get => (string) this[nameof (SerialCode)];
      set => this[nameof (SerialCode)] = (object) value;
    }
  }
}
