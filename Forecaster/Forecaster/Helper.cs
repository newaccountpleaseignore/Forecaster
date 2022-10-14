using System;
using System.Globalization;
using System.Net;
using System.Security.Cryptography;
using System.Windows.Forms;
using WeatherAPI;

namespace Forecaster
{
  internal class Helper
  {
    private const string Charset = "ABCDEFGHJKLMNPQRSTUVWXYZ13456789";

    public static string GetConditionImagePath(Conditions conditions, bool isCurrentConditon)
    {
      string icon = conditions.Icon;
      string startupPath = Application.StartupPath;
      string empty = string.Empty;
      string conditionImagePath;
      if (isCurrentConditon)
      {
        DateTime dateTime = Convert.ToDateTime(conditions.LocalTime);
        Convert.ToInt32(dateTime.ToString("HH"));
        conditionImagePath = dateTime.Hour >= 6 && dateTime.Hour <= 20 || !System.IO.File.Exists(string.Format("{0}\\Images\\{1}n.png", (object) startupPath, (object) icon)) ? string.Format("{0}\\Images\\{1}.png", (object) startupPath, (object) icon) : string.Format("{0}\\Images\\{1}n.png", (object) startupPath, (object) icon);
      }
      else
        conditionImagePath = string.Format("{0}\\Images\\{1}.png", (object) startupPath, (object) icon);
      return conditionImagePath;
    }

    public static string GetCurrentTemperature(Conditions conditions)
    {
      string currentTemperature = conditions.Temp;
      string tempUnit = Forecaster.Properties.Settings.Default.TempUnit;
      if (Forecaster.Properties.Settings.Default.OverrideTempUnit)
      {
        if (conditions.Units == "US" && tempUnit == "SI")
          currentTemperature = Helper.ConvertFahrenheitToCelsius(Convert.ToDouble(conditions.Temp)).ToString("0") + "° C";
        if (conditions.Units == "SI" && tempUnit == "US")
          currentTemperature = Helper.ConvertCelsiusToFahrenheit(Convert.ToDouble(conditions.Temp)).ToString("0") + "° F";
        if (conditions.Units == "SI" && tempUnit == "SI")
          currentTemperature = conditions.Temp + "° C";
      }
      else
        currentTemperature = !RegionInfo.CurrentRegion.IsMetric ? Helper.ConvertCelsiusToFahrenheit(Convert.ToDouble(conditions.Temp)).ToString("0") + "° F" : currentTemperature + "° C";
      return currentTemperature;
    }

    public static string GetTemperature(Conditions conditions, string temp)
    {
      string tempUnit = Forecaster.Properties.Settings.Default.TempUnit;
      if (Forecaster.Properties.Settings.Default.OverrideTempUnit)
      {
        if (conditions.Units == "US" && tempUnit == "SI")
          temp = Helper.ConvertFahrenheitToCelsius(Convert.ToDouble(temp)).ToString("0") + "° C";
        if (conditions.Units == "SI" && tempUnit == "US")
          temp = Helper.ConvertCelsiusToFahrenheit(Convert.ToDouble(temp)).ToString("0") + "° F";
        if (conditions.Units == "US" && tempUnit == "US")
          temp += "° F";
        if (conditions.Units == "SI" && tempUnit == "SI")
          temp += "° C";
      }
      else
        temp = !RegionInfo.CurrentRegion.IsMetric ? Helper.ConvertCelsiusToFahrenheit(Convert.ToDouble(temp)).ToString("0") + "° F" : temp + "° C";
      return temp;
    }

    private static double ConvertCelsiusToFahrenheit(double temp) => 1.8 * temp + 32.0;

    private static double ConvertFahrenheitToCelsius(double temp) => 5.0 / 9.0 * (temp - 32.0);

    public static bool CheckForInternetConnection()
    {
      bool flag = false;
      WebRequest webRequest = WebRequest.Create(new Uri("http://www.google.com/"));
      try
      {
        WebResponse response = webRequest.GetResponse();
        flag = true;
        response.Close();
      }
      catch
      {
      }
      return flag;
    }

    public static bool Validate(string serial, byte[] password)
    {
      serial = serial.ToUpper();
      if (serial.Length != 29)
        return false;
      for (int index = 0; index < 29; ++index)
      {
        if (index % 6 == 5 && serial[index] != '-' || index % 6 != 5 && !"ABCDEFGHJKLMNPQRSTUVWXYZ13456789".Contains(Convert.ToString(serial[index])))
          return false;
      }
      byte[] src = Helper.Decode(serial.Replace("-", ""));
      byte[] numArray = new byte[5 + password.Length];
      Buffer.BlockCopy((Array) src, 0, (Array) numArray, 0, 5);
      Buffer.BlockCopy((Array) password, 0, (Array) numArray, 5, password.Length);
      int num = 17;
      for (int index = 0; index < numArray.Length; ++index)
        num += (int) numArray[index];
      if ((num & 31) != Helper.GetPosition(serial[28]))
        return false;
      byte[] hash = SHA256.Create().ComputeHash(numArray);
      for (int index = 0; index < 10; ++index)
      {
        if ((int) src[index + 5] != (int) hash[index])
          return false;
      }
      return true;
    }

    private static byte[] Decode(string data)
    {
      byte[] numArray1 = new byte[256];
      for (int index = 0; index < "ABCDEFGHJKLMNPQRSTUVWXYZ13456789".Length; ++index)
        numArray1[(int) "ABCDEFGHJKLMNPQRSTUVWXYZ13456789"[index]] = (byte) index;
      byte[] numArray2 = new byte[data.Length * 5 / 8];
      int num1 = 0;
      int num2 = 0;
      while (num2 <= data.Length - 8)
      {
        byte[] numArray3 = numArray1;
        string str1 = data;
        int index1 = num2;
        int num3 = index1 + 1;
        int index2 = (int) str1[index1];
        byte num4 = numArray3[index2];
        byte[] numArray4 = numArray1;
        string str2 = data;
        int index3 = num3;
        int num5 = index3 + 1;
        int index4 = (int) str2[index3];
        byte num6 = numArray4[index4];
        byte[] numArray5 = numArray1;
        string str3 = data;
        int index5 = num5;
        int num7 = index5 + 1;
        int index6 = (int) str3[index5];
        byte num8 = numArray5[index6];
        byte[] numArray6 = numArray1;
        string str4 = data;
        int index7 = num7;
        int num9 = index7 + 1;
        int index8 = (int) str4[index7];
        byte num10 = numArray6[index8];
        byte[] numArray7 = numArray1;
        string str5 = data;
        int index9 = num9;
        int num11 = index9 + 1;
        int index10 = (int) str5[index9];
        byte num12 = numArray7[index10];
        byte[] numArray8 = numArray1;
        string str6 = data;
        int index11 = num11;
        int num13 = index11 + 1;
        int index12 = (int) str6[index11];
        byte num14 = numArray8[index12];
        byte[] numArray9 = numArray1;
        string str7 = data;
        int index13 = num13;
        int num15 = index13 + 1;
        int index14 = (int) str7[index13];
        byte num16 = numArray9[index14];
        byte[] numArray10 = numArray1;
        string str8 = data;
        int index15 = num15;
        num2 = index15 + 1;
        int index16 = (int) str8[index15];
        byte num17 = numArray10[index16];
        byte[] numArray11 = numArray2;
        int index17 = num1;
        int num18 = index17 + 1;
        int num19 = (int) (byte) ((int) num4 << 3 | (int) num6 >> 2);
        numArray11[index17] = (byte) num19;
        byte[] numArray12 = numArray2;
        int index18 = num18;
        int num20 = index18 + 1;
        int num21 = (int) (byte) ((int) num6 << 6 | (int) num8 << 1 | (int) num10 >> 4);
        numArray12[index18] = (byte) num21;
        byte[] numArray13 = numArray2;
        int index19 = num20;
        int num22 = index19 + 1;
        int num23 = (int) (byte) ((int) num10 << 4 | (int) num12 >> 1);
        numArray13[index19] = (byte) num23;
        byte[] numArray14 = numArray2;
        int index20 = num22;
        int num24 = index20 + 1;
        int num25 = (int) (byte) ((int) num12 << 7 | (int) num14 << 2 | (int) num16 >> 3);
        numArray14[index20] = (byte) num25;
        byte[] numArray15 = numArray2;
        int index21 = num24;
        num1 = index21 + 1;
        int num26 = (int) (byte) ((uint) num16 << 5 | (uint) num17);
        numArray15[index21] = (byte) num26;
      }
      return numArray2;
    }

    private static int GetPosition(char character)
    {
      for (int index = 0; index < 32; ++index)
      {
        if ((int) "ABCDEFGHJKLMNPQRSTUVWXYZ13456789"[index] == (int) character)
          return index;
      }
      return -1;
    }
  }
}
