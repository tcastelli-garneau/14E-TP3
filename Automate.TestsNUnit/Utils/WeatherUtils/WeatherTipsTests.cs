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
    }
}
