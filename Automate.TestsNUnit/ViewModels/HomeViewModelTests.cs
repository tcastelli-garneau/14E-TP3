using Automate.Abstract.Services;
using Automate.Abstract.Utils;
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
        private Mock<PropertyChangedEventHandler> mockPropertyChanged;

        [SetUp]
        public void Setup()
        {
            mockWindow = new Mock<Window>();
            mockNavigationUtils = new Mock<INavigationUtils>();
            mockPropertyChanged = new Mock<PropertyChangedEventHandler>();
            mockTasksServices = new Mock<ITasksServices>();

            homeViewModel = new HomeViewModel(mockWindow.Object, mockNavigationUtils.Object, mockTasksServices.Object);

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
        public void SignOut_AuthenticatedUserIsNull()
        {
            homeViewModel.SignOut();

            Assert.That(Automate.Utils.Environment.authenticatedUser, Is.Null);
        }

        [Test]
        public void ToggleWeatherReading_NoTimer_StartTimer()
        {
            homeViewModel.ToggleWeatherReading();

            Assert.That(homeViewModel.CurrentWeather, Is.Not.Null);
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
            const string propertyName = "ToggleWeatherReadingMessage";
            homeViewModel.ToggleWeatherReading();

            homeViewModel.ToggleWeatherReading();

            mockPropertyChanged.Verify(x =>
                x.Invoke(It.IsAny<object>(), It.Is<PropertyChangedEventArgs>(args => args.PropertyName == propertyName)),
                Times.Exactly(2)
            );
        }
    }
}
