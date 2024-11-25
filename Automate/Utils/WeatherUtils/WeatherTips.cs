namespace Automate.Utils.WeatherUtils
{
    public class WeatherTips
    {
        // source : https://drygair.com/fr/blog-fr/quelles-sont-les-conditions-ideales-pour-les-tomates-de-serre/
        const int MIN_TEMPERATURE_GOAL = 21;
        const int MAX_TEMPERATURE_GOAL = 27;
        const int MIN_HUMIDITY_GOAL = 60;
        const int MAX_HUMIDITY_GOAL = 85;

        // source : https://fr.gardeniadream.com/21340327-light-day-for-tomato-seedlings-how-to-highlight-and-how-much
        const int MIN_LUX_GOAL = 25000;
        const int MAX_LUX_GOAL = 45000;

        public string GetTemperatureTips(bool isHeatingOn, bool isWindowOpen, int currentTemperature)
        {
            if (isHeatingOn && !isWindowOpen && currentTemperature > MAX_TEMPERATURE_GOAL)
                return "La température de la serre est trop élevée. Veuillez éteindre le chauffage et/ou ouvrir les fenêtres.";

            if (isHeatingOn && isWindowOpen && currentTemperature > MAX_TEMPERATURE_GOAL)
                return "La température de la serre est trop élevée. Veuillez éteindre le chauffage.";

            if (!isHeatingOn && !isWindowOpen && currentTemperature > MAX_TEMPERATURE_GOAL)
                return "La température de la serre est trop élevée. Veuillez ouvrir les fenêtres.";

            if (!isHeatingOn && isWindowOpen && currentTemperature < MIN_TEMPERATURE_GOAL)
                return "La température de la serre est trop basse. Veuillez allumer le chauffage et/ou fermer les fenêtres.";

            if (!isHeatingOn && !isWindowOpen && currentTemperature < MIN_TEMPERATURE_GOAL)
                return "La température de la serre est trop basse. Veuillez allumer le chauffage.";

            if (isHeatingOn && isWindowOpen && currentTemperature < MIN_TEMPERATURE_GOAL)
                return "La température de la serre est trop basse. Veuillez fermer les fenêtres.";

            if (currentTemperature > MAX_TEMPERATURE_GOAL)
                return "La température de la serre est trop élevée. Aucune action supplémentaire recommandée.";

            if (currentTemperature < MIN_TEMPERATURE_GOAL)
                return "La température de la serre est trop basse. Aucune action supplémentaire recommandée.";

            return "La température de la serre est correcte.";
        }

        public string GetHumidityTips(bool isWateringOn, bool isVentilationOn, int currentHumidity)
        {
            if (isWateringOn && !isVentilationOn && currentHumidity > MAX_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage et activer le système de ventilation.";

            if (isWateringOn && isVentilationOn && currentHumidity > MAX_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage.";

            if (!isWateringOn && !isVentilationOn && currentHumidity > MAX_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop élevé. Veuillez activer le système de ventilation.";

            if (!isWateringOn && isVentilationOn && currentHumidity < MIN_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage et désactiver le système de ventilation.";

            if (!isWateringOn && !isVentilationOn && currentHumidity < MIN_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage.";

            if (isWateringOn && isVentilationOn && currentHumidity < MIN_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop bas. Veuillez désactiver le système de ventilation.";

            if (currentHumidity > MAX_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop élevé. Aucune action supplémentaire recommandée.";

            if (currentHumidity < MAX_HUMIDITY_GOAL)
                return "Le taux d'humidité est trop bas. Aucune action supplémentaire recommandée.";

            return "Le taux d'humidité est correct.";
        }
    }
}
