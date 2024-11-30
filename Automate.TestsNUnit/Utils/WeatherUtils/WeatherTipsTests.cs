using Automate.Utils.WeatherUtils;

namespace Automate.TestsNUnit.Utils.WeatherUtils
{
    public class WeatherTipsTests
    {
        const int MIN_TEMPERATURE_GOAL = 21;
        const int MAX_TEMPERATURE_GOAL = 27;

        const int MIN_HUMIDITY_GOAL_DAY = 80;
        const int MAX_HUMIDITY_GOAL_DAY = 85;
        const int MIN_HUMIDITY_GOAL_NIGHT = 65;
        const int MAX_HUMIDITY_GOAL_NIGHT = 75;

        const int MIN_LUX_GOAL = 25000;
        const int MAX_LUX_GOAL = 45000;

        DateTime dayTime = new DateTime(2024, 11, 26, 10, 0, 0);
        DateTime nightTime = new DateTime(2024, 11, 26, 1, 0, 0);

        [Test]
        public void FakeTestForPipeline()
        {
            Assert.IsFalse(true);
        }

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
        public void GetHumidityTips_WateringOnAndVentilationOffAndHumidityTooHighDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MAX_HUMIDITY_GOAL_DAY + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage et activer le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOnAndHumidityTooHighDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, true, MAX_HUMIDITY_GOAL_DAY + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOffAndHumidityTooHighDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, false, MAX_HUMIDITY_GOAL_DAY + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez activer le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOnAndHumidityTooHighDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, true, MAX_HUMIDITY_GOAL_DAY + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOnAndHumidityTooLowDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, true, MIN_HUMIDITY_GOAL_DAY - 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage et désactiver le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOffAndHumidityTooLowDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, false, MIN_HUMIDITY_GOAL_DAY - 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOnAndHumidityTooLowDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, true, MIN_HUMIDITY_GOAL_DAY - 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez désactiver le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOffAndHumidityTooLowDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MIN_HUMIDITY_GOAL_DAY - 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetHumidityTips_HumidityIsCorrectDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MIN_HUMIDITY_GOAL_DAY + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est correct."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOffAndHumidityTooHighDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MAX_HUMIDITY_GOAL_NIGHT + 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage et activer le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOnAndHumidityTooHighDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, true, MAX_HUMIDITY_GOAL_NIGHT + 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez désactiver le système d'arrosage."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOffAndHumidityTooHighDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, false, MAX_HUMIDITY_GOAL_NIGHT + 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Veuillez activer le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOnAndHumidityTooHighDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, true, MAX_HUMIDITY_GOAL_NIGHT + 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop élevé. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOnAndHumidityTooLowDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, true, MIN_HUMIDITY_GOAL_NIGHT - 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage et désactiver le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOffAndVentilationOffAndHumidityTooLowDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(false, false, MIN_HUMIDITY_GOAL_NIGHT - 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez activer le système d'arrosage."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOnAndHumidityTooLowDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, true, MIN_HUMIDITY_GOAL_NIGHT - 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Veuillez désactiver le système de ventilation."));
        }

        [Test]
        public void GetHumidityTips_WateringOnAndVentilationOffAndHumidityTooLowDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MIN_HUMIDITY_GOAL_NIGHT - 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est trop bas. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetHumidityTips_HumidityIsCorrectDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetHumidityTips(true, false, MIN_HUMIDITY_GOAL_NIGHT + 1, nightTime);

            Assert.That(result, Is.EqualTo("Le taux d'humidité est correct."));
        }

        [Test]
        public void GetLuminiosityTips_LightsOnAndLuminiosityTooHighDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetLuminiosityTips(true, MAX_LUX_GOAL + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le niveau de luminiosité est trop élevé. Veuillez éteindre les lumières."));
        }

        [Test]
        public void GetLuminiosityTips_LightsOffAndLuminiosityTooHighDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetLuminiosityTips(false, MAX_LUX_GOAL + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le niveau de luminiosité est trop élevé. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetLuminiosityTips_LightsOffAndLuminiosityTooLowDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetLuminiosityTips(false, MIN_LUX_GOAL - 1, dayTime);

            Assert.That(result, Is.EqualTo("Le niveau de luminiosité est trop bas. Veuillez allumer les lumières."));
        }

        [Test]
        public void GetLuminiosityTips_LightsOnAndLuminiosityTooLowDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetLuminiosityTips(true, MIN_LUX_GOAL - 1, dayTime);

            Assert.That(result, Is.EqualTo("Le niveau de luminiosité est trop bas. Aucune action supplémentaire recommandée."));
        }

        [Test]
        public void GetLuminiosityTips_LuminiosityIsCorrectDuringDay_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetLuminiosityTips(true, MIN_LUX_GOAL + 1, dayTime);

            Assert.That(result, Is.EqualTo("Le niveau de luminiosité est correct."));
        }

        [Test]
        public void GetLuminiosityTips_LightsOnDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetLuminiosityTips(true, MIN_LUX_GOAL + 1, nightTime);

            Assert.That(result, Is.EqualTo("Veuillez éteindre les lumières entre minuit et 5h00 du matin."));
        }

        [Test]
        public void GetLuminiosityTips_LightsOffDuringNight_ReturnCorrectMessage()
        {
            string result = WeatherTips.GetLuminiosityTips(false, MIN_LUX_GOAL + 1, nightTime);

            Assert.That(result, Is.EqualTo("Le niveau de luminiosité est correct."));
        }
    }
}
