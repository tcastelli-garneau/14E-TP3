using Automate.Models;
using System.Collections.Generic;

namespace Automate.Abstract.Utils
{
    public interface IWeatherReader
    {
        List<Weather> ReadWeather();
    }
}