using Forecaster;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Xml;

namespace WeatherAPI
{
  internal class Weather
  {
    public static Forecast GetConditions(string location)
    {
      Forecast conditions1 = new Forecast();
      using (StreamReader txtReader1 = new StreamReader(WebRequest.Create(string.Format("http://free.worldweatheronline.com/feed/weather.ashx?q={0}&format=xml&num_of_days=5&includeLocation=yes&key=", (object) location)).GetResponse().GetResponseStream(), Encoding.Default, true))
      {
        XmlDocument xmlDocument1 = new XmlDocument();
        xmlDocument1.Load((TextReader) txtReader1);
        try
        {
          Conditions conditions2 = new Conditions();
          if (Weather.IsZipCode(location))
          {
            string innerText1 = xmlDocument1.SelectSingleNode("/data/nearest_area/areaName").InnerText;
            string innerText2 = xmlDocument1.SelectSingleNode("/data/nearest_area/region").InnerText;
            conditions2.City = string.Format("{0}, {1}", (object) innerText1, (object) innerText2);
          }
          else
            conditions2.City = location;
          conditions2.Condition = xmlDocument1.SelectSingleNode("/data/current_condition/weatherDesc").InnerText;
          conditions2.Humidity = xmlDocument1.SelectSingleNode("/data/current_condition/humidity").InnerText;
          conditions2.Icon = xmlDocument1.SelectSingleNode("/data/current_condition/weatherCode").InnerText;
          conditions2.Temp = xmlDocument1.SelectSingleNode("/data/current_condition/temp_C").InnerText;
          conditions2.Time = xmlDocument1.SelectSingleNode("/data/current_condition/observation_time").InnerText;
          conditions2.WindDirection = xmlDocument1.SelectSingleNode("/data/current_condition/winddir16Point").InnerText;
          conditions2.WindSpeed = xmlDocument1.SelectSingleNode("/data/current_condition/windspeedKmph").InnerText;
          List<Conditions> conditionsList = new List<Conditions>();
          foreach (XmlNode selectNode in xmlDocument1.SelectNodes("/data/weather"))
          {
            Conditions conditions3 = new Conditions();
            conditions3.Condition = selectNode.SelectSingleNode("weatherDesc").InnerText;
            DateTime dateTime = Convert.ToDateTime(selectNode.SelectSingleNode("date").InnerText);
            conditions3.DayOfWeek = dateTime.ToString("ddd");
            conditions3.High = selectNode.SelectSingleNode("tempMaxC").InnerText;
            conditions3.Icon = selectNode.SelectSingleNode("weatherCode").InnerText;
            conditions3.Low = selectNode.SelectSingleNode("tempMinC").InnerText;
            conditionsList.Add(conditions3);
          }
          using (StreamReader txtReader2 = new StreamReader(WebRequest.Create(string.Format("http://www.worldweatheronline.com/feed/tz.ashx?q={0}&format=xml&key=", (object) location)).GetResponse().GetResponseStream(), Encoding.Default, true))
          {
            XmlDocument xmlDocument2 = new XmlDocument();
            xmlDocument1.Load((TextReader) txtReader2);
            conditions2.UTC = xmlDocument1.SelectSingleNode("/data/time_zone/utcOffset").InnerText;
            conditions2.LocalTime = xmlDocument1.SelectSingleNode("/data/time_zone/localtime").InnerText;
          }
          conditions1.Current = conditions2;
          conditions1.Extended = conditionsList;
        }
        catch
        {
          conditions1 = (Forecast) null;
          Console.WriteLine("Problem fetching information");
        }
        txtReader1.Close();
        txtReader1.Dispose();
      }
      return conditions1;
    }

    public static bool CheckLocation(string location)
    {
      bool flag;
      using (StreamReader txtReader = new StreamReader(WebRequest.Create(string.Format("http://free.worldweatheronline.com/feed/weather.ashx?q={0}&format=xml&key=", (object) location)).GetResponse().GetResponseStream(), Encoding.Default, true))
      {
        XmlDocument xmlDocument = new XmlDocument();
        xmlDocument.Load((TextReader) txtReader);
        try
        {
          string innerText = xmlDocument.SelectSingleNode("/data/error/msg").InnerText;
          flag = false;
        }
        catch
        {
          flag = true;
        }
        txtReader.Close();
        txtReader.Dispose();
      }
      return flag;
    }

    private static string GetLanguageCode() => Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

    private static bool IsZipCode(string location) => int.TryParse(location, out int _);
  }
}
