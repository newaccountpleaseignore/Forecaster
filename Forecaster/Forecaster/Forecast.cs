using System.Collections.Generic;
using WeatherAPI;

namespace Forecaster
{
  public class Forecast
  {
    private Conditions current;
    private List<Conditions> extended;

    public Conditions Current
    {
      get => this.current;
      set => this.current = value;
    }

    public List<Conditions> Extended
    {
      get => this.extended;
      set => this.extended = value;
    }
  }
}
