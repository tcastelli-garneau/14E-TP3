using Automate.Abstract.Services;
using Automate.Abstract.Utils;
using Automate.Models;
using Automate.Services.Commands;
using Automate.Utils;
using Automate.Views;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace Automate.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private readonly INavigationUtils navigationUtils;
        private readonly ITasksServices tasksServices;
        private Window window;

        public string CriticalTaskMessage
        {
            get
            {
                if (tasksServices.DoesTodayHasCriticalTask())
                    return "ATTENTION - Il y a un événement critique prévu aujourd'hui.";

                return "";
            }
        }
        public string ToggleWeatherReadingMessage
        {
            get
            {
                if (readWeatherTimer == null)
                    return "Lire la météo";

                return "Arrêter la lecture de la météo";
            }
        }

        public List<Weather> Weathers { get; set; }
        public Weather? CurrentWeather { get; set; }

        public ICommand GoToCalendarCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand ToggleWeatherReadingCommand { get; }

        private int currentWeatherIndex = -1;
        private Timer? readWeatherTimer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public HomeViewModel(Window openedWindow, INavigationUtils navigationUtils, ITasksServices tasksServices)
        {
            window = openedWindow;
            this.navigationUtils = navigationUtils;
            this.tasksServices = tasksServices;

            GoToCalendarCommand = new RelayCommand(GoToCalendar);
            SignOutCommand = new RelayCommand(SignOut);
            ToggleWeatherReadingCommand = new RelayCommand(ToggleWeatherReading);

            Weathers = CsvReader.ReadWeather();
        }

        public void GoToCalendar()
        {
            navigationUtils.NavigateToAndCloseCurrentWindow<CalendarWindow>(window);
        }

        public void SignOut()
        {
            Environment.authenticatedUser = null;
            navigationUtils.NavigateToAndCloseCurrentWindow<LoginWindow>(window);
        }

        public void ToggleWeatherReading()
        {
            const int delay = 0;
            const int timeInterval = 10000;

            if (readWeatherTimer == null)
                readWeatherTimer = new Timer(GetNextWeather, null, delay, timeInterval);
            else
                ResetWeatherReading();

            OnPropertyChanged(nameof(ToggleWeatherReadingMessage));
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetWeatherReading()
        {
            readWeatherTimer!.Dispose();
            readWeatherTimer = null;
            currentWeatherIndex = -1;
            CurrentWeather = null;
        }

        private void GetNextWeather(object? stateInfo)
        {
            currentWeatherIndex++;

            if (currentWeatherIndex == Weathers.Count)
                readWeatherTimer!.Dispose();
            else
                CurrentWeather = Weathers[currentWeatherIndex];
        }
    }
}
