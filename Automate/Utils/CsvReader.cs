using Automate.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace Automate.Utils
{
    public static class CsvReader
    {
        public static List<Weather> ReadWeather()
        {
            List<Weather> weathers = new List<Weather>();

            using (TextFieldParser parser = new TextFieldParser(Environment.tempDataPath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                parser.ReadFields();

                while (!parser.EndOfData)
                {
                    string[]? fields = parser.ReadFields();
                    if (fields == null)
                        break;

                    weathers.Add(ConvertFieldsToWeather(fields));
                }
            }

            return weathers;
        }

        private static Weather ConvertFieldsToWeather(string[] fields)
        {
            Weather weather = new Weather()
            {
                Date = DateTime.Parse(fields[0]),
                Temperature = Convert.ToInt32(fields[1]),
                Humidity = Convert.ToInt32(fields[2]),
                Luminosity = Convert.ToInt32(fields[3])
            };

            return weather;
        }
    }
}
