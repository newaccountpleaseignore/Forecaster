using System;

namespace WeatherAPI
{
  public class Conditions
  {
    private string city = "No Data";
    private string time = DateTime.Now.ToString();
    private string units = "SI";
    private string dayOfWeek = "No Data";
    private string condition = "No Data";
    private string temp = "No Data";
    private string humidity = "No Data";
    private string windSpeed = "No Data";
    private string windDirection = "No Data";
    private string high = "No Data";
    private string low = "No Data";
    private string icon = string.Empty;
    private string utc = "0";
    private string localTime = string.Empty;

    public string City
    {
      get => this.city;
      set => this.city = value;
    }

    public string Time
    {
      get => this.time;
      set => this.time = value;
    }

    public string Units
    {
      get => this.units;
      set => this.units = value;
    }

    public string Condition
    {
      get => this.condition;
      set => this.condition = value;
    }

    public string Temp
    {
      get => this.temp;
      set => this.temp = value;
    }

    public string Humidity
    {
      get => this.humidity;
      set => this.humidity = value;
    }

    public string WindSpeed
    {
      get => this.windSpeed;
      set => this.windSpeed = value;
    }

    public string WindDirection
    {
      get => this.windDirection;
      set => this.windDirection = value;
    }

    public string DayOfWeek
    {
      get => this.dayOfWeek;
      set => this.dayOfWeek = value;
    }

    public string High
    {
      get => this.high;
      set => this.high = value;
    }

    public string Low
    {
      get => this.low;
      set => this.low = value;
    }

    public string Icon
    {
      get => this.icon;
      set => this.icon = value;
    }

    public string UTC
    {
      get => this.utc;
      set => this.utc = value;
    }

    public string LocalTime
    {
      get => this.localTime;
      set => this.localTime = value;
    }
  }
}
