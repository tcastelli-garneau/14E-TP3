using Automate.Utils.WeatherUtils;

namespace Automate.TestsNUnit.Utils.WeatherUtils
{
    public class WeatherTipsTests
    {
        const int MIN_TEMPERATURE_GOAL = 21;
        const int MAX_TEMPERATURE_GOAL = 27;
        const int MIN_HUMIDITY_GOAL = 60;
        const int MAX_HUMIDITY_GOAL = 85;
        const int MIN_LUX_GOAL = 25000;
        const int MAX_LUX_GOAL = 45000;

        [Test]
        public void GetTemperatureTips_HeatingOnAndWindowsClosedAndTemperatureTooHot_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(true, false, MAX_TEMPERATURE_GOAL + 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop élevée. Veuillez éteindre le chauffage et/ou ouvrir les fenêtres."));
        }

        [Test]
        public void GetTemperatureTips_HeatingOnAndWindowsOpenAndTemperatureTooHot_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(true, true, MAX_TEMPERATURE_GOAL + 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop élevée. Veuillez éteindre le chauffage."));
        }

        [Test]
        public void GetTemperatureTips_HeatingOffAndWindowsClosedAndTemperatureTooHot_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(false, false, MAX_TEMPERATURE_GOAL + 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop élevée. Veuillez ouvrir les fenêtres."));
        }

        [Test]
        public void GetTemperatureTips_HeatingOffAndWindowsOpenAndTemperatureTooHot_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(false, true, MAX_TEMPERATURE_GOAL + 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop élevée. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetTemperatureTips_HeatingOffAndWindowsOpenAndTemperatureTooCold_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(false, true, MIN_TEMPERATURE_GOAL - 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop basse. Veuillez allumer le chauffage et/ou fermer les fenêtres."));
        }

        [Test]
        public void GetTemperatureTips_HeatingOffAndWindowsClosedAndTemperatureTooCold_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(false, false, MIN_TEMPERATURE_GOAL - 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop basse. Veuillez allumer le chauffage."));
        }

        [Test]
        public void GetTemperatureTips_HeatingOnAndWindowsOpenAndTemperatureTooCold_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(true, true, MIN_TEMPERATURE_GOAL - 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop basse. Veuillez fermer les fenêtres."));
        }

        [Test]
        public void GetTemperatureTips_HeatingOnAndWindowsClosedAndTemperatureTooCold_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(true, false, MIN_TEMPERATURE_GOAL - 1);

            Assert.That(result, Is.EqualTo("La température de la serre est trop basse. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetTemperatureTips_TemperatureIsGood_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetTemperatureTips(true, false, MIN_TEMPERATURE_GOAL + 1);

            Assert.That(result, Is.EqualTo("La température de la serre est correcte."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOffAndHumidityTooHigh_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MAX_HUMIDITY_GOAL + 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage et activer le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOnAndHumidityTooHigh_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, true, MAX_HUMIDITY_GOAL + 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOffAndHumidityTooHigh_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, false, MAX_HUMIDITY_GOAL + 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez activer le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOnAndHumidityTooHigh_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, true, MAX_HUMIDITY_GOAL + 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOnAndHumidityTooLow_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, true, MIN_HUMIDITY_GOAL - 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage et désactiver le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOffAndHumidityTooLow_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, false, MIN_HUMIDITY_GOAL - 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOnAndHumidityTooLow_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, true, MIN_HUMIDITY_GOAL - 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez désactiver le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOffAndHumidityTooLow_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MIN_HUMIDITY_GOAL - 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetHumidityTips_HumidityIsCorrect_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MIN_HUMIDITY_GOAL + 1);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est correct."));
        }
    }
}
