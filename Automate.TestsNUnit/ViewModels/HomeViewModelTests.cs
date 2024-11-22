using Automate.Abstract.Services;
using Automate.Abstract.Utils;
using Automate.Models;
using Automate.ViewModels;
using Automate.Views;
using Moq;
using System.ComponentModel;
using System.Windows;

namespace Automate.TestsNUnit.ViewModels
{
    [Apartment(ApartmentState.STA)]
    public class HomeViewModelTests
    {
        private const string alertMessage = "ATTENTION - Il y a un événement critique prévu aujourd'hui.";

        private HomeViewModel homeViewModel;
        private Mock<Window> mockWindow;
        private Mock<INavigationUtils> mockNavigationUtils;
        private Mock<ITasksServices> mockTasksServices;
        private Mock<IWeatherReader> mockWeatherReader;
        private Mock<PropertyChangedEventHandler> mockPropertyChanged;

        [SetUp]
        public void Setup()
        {
            mockWindow = new Mock<Window>();
            mockNavigationUtils = new Mock<INavigationUtils>();
            mockPropertyChanged = new Mock<PropertyChangedEventHandler>();
            mockTasksServices = new Mock<ITasksServices>();
            mockWeatherReader = new Mock<IWeatherReader>();
            mockWeatherReader.Setup(x => x.ReadWeather()).Returns(new List<Weather>() { new Weather()});

            homeViewModel = new HomeViewModel(
                mockWindow.Object, mockNavigationUtils.Object, mockTasksServices.Object, mockWeatherReader.Object);

            homeViewModel.PropertyChanged += mockPropertyChanged.Object;
        }

        [Test]
        public void GoToCalendar_NavigateToCalendar()
        {
            homeViewModel.GoToCalendar();

            mockNavigationUtils.Verify(
                x => x.NavigateToAndCloseCurrentWindow<CalendarWindow>(It.IsAny<Window>()), Times.Once());
        }

        [Test]
        public void CriticalTaskMessage_NoCriticalTask_ReturnEmptyString()
        {
            mockTasksServices.Setup(x => x.DoesTodayHasCriticalTask()).Returns(false);

            Assert.That(homeViewModel.CriticalTaskMessage, Is.EqualTo(""));
        }

        [Test]
        public void CriticalTaskMessage_HasCriticalTask_ReturnAlertMessage()
        {
            mockTasksServices.Setup(x => x.DoesTodayHasCriticalTask()).Returns(true);

            Assert.That(homeViewModel.CriticalTaskMessage, Is.EqualTo(alertMessage));
        }

        [Test]
        public void WeatherPrompt_NoCurrentWeather_ReturnEmptyString()
        {
            Assert.That(homeViewModel.WeatherPrompt, Is.EqualTo(""));
        }

        [Test]
        public void WeatherPrompt_CurrentWeatherNotNull_ReturnStringWithCurrentWeatherInfos()
        {
            Weather weather = new Weather() { Date = DateTime.Today, Humidity = 30, Luminosity = 33, Temperature = 10 };
            homeViewModel.CurrentWeather = weather;

            Assert.That(homeViewModel.WeatherPrompt.StartsWith("Météo"), Is.True);
            Assert.That(homeViewModel.WeatherPrompt.Contains($"Température : {weather.Temperature}"), Is.True);
            Assert.That(homeViewModel.WeatherPrompt.Contains($"Humidité : {weather.Humidity}"), Is.True);
            Assert.That(homeViewModel.WeatherPrompt.Contains($"Luminiosité : {weather.Luminosity}"), Is.True);
        }

        [Test]
        public void SignOut_NavigateToLogin()
        {
            homeViewModel.SignOut();

            mockNavigationUtils.Verify(
                x => x.NavigateToAndCloseCurrentWindow<LoginWindow>(It.IsAny<Window>()), Times.Once());
        }

        [Test]
        public void SignOut_AuthenticatedUserIsNull()
        {
            homeViewModel.SignOut();

            Assert.That(Automate.Utils.Environment.authenticatedUser, Is.Null);
        }

        [Test]
        public void ToggleWeatherReading_NoTimer_StartTimer()
        {
            homeViewModel.ToggleWeatherReading();

            Assert.That(() => homeViewModel.CurrentWeather, Is.Not.Null.After(1).Seconds);
        }

        [Test]
        public void ToggleWeatherReading_NoTimer_OnPropertyChangedIsInvoked()
        {
            const string propertyName = "ToggleWeatherReadingMessage";

            homeViewModel.ToggleWeatherReading();

            mockPropertyChanged.Verify(x =>
                x.Invoke(It.IsAny<object>(), It.Is<PropertyChangedEventArgs>(args => args.PropertyName == propertyName)),
                Times.Once()
            );
        }

        [Test]
        public void ToggleWeatherReading_TimerOn_StopTimer()
        {
            homeViewModel.ToggleWeatherReading();

            homeViewModel.ToggleWeatherReading();

            Assert.That(homeViewModel.CurrentWeather, Is.Null);
        }

        [Test]
        public void ToggleWeatherReading_TimerOn_OnPropertyChangedIsInvoked()
        {
            homeViewModel.ToggleWeatherReading();
            const string propertyName = "ToggleWeatherReadingMessage";

            homeViewModel.ToggleWeatherReading();

            mockPropertyChanged.Verify(x =>
                x.Invoke(It.IsAny<object>(), It.Is<PropertyChangedEventArgs>(args => args.PropertyName == propertyName)),
                Times.Exactly(2)
            );
        }
    }
}
