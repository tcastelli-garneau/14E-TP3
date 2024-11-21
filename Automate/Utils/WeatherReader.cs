using Automate.Abstract.Utils;
using Automate.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;

namespace Automate.Utils
{
    public class WeatherReader : IWeatherReader
    {
        public List<Weather> ReadWeather()
        {
            List<Weather> weathers = new List<Weather>();

            using (TextFieldParser parser = new TextFieldParser(Environment.tempDataPath))
            {
                SetupDelimiters(parser);
                RemoveHeaderFields(parser);
                weathers = ReadAllFields(parser);
            }

            return weathers;
        }

        private List<Weather> ReadAllFields(TextFieldParser parser)
        {
            List<Weather> weathers = new List<Weather>();

            while (!parser.EndOfData)
            {
                string[]? fields = parser.ReadFields();

                if (fields != null)
                    weathers.Add(ConvertFieldsToWeather(fields));
            }

            return weathers;
        }

        private Weather ConvertFieldsToWeather(string[] fields)
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

        private void SetupDelimiters(TextFieldParser parser)
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");
        }

        private void RemoveHeaderFields(TextFieldParser parser)
        {
            parser.ReadFields();
        }
    }
}
