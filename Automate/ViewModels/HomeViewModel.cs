﻿using Automate.Abstract.Services;
using Automate.Abstract.Utils;
using Automate.Models;
using Automate.Services.Commands;
using Automate.Utils;
using Automate.Utils.WeatherUtils;
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
        private readonly IWeatherReader weatherReader;
        private Window window;

        private bool areLightsOpen;
        public bool AreLightsOpen
        {
            get => areLightsOpen;
            set
            {
                areLightsOpen = value;
                OnPropertyChanged(nameof(AreLightsOpen));

                UpdateLuminiosityTips();
                OnPropertyChanged(nameof(LuminiosityTips));
            }
        }
        private bool isHeatOpen;
        public bool IsHeatOpen
        {
            get => isHeatOpen;
            set
            {
                isHeatOpen = value;
                OnPropertyChanged(nameof(IsHeatOpen));

                UpdateTemperatureTips();
                OnPropertyChanged(nameof(TemperatureTips));
            }
        }
        private bool areWindowsOpen;
        public bool AreWindowsOpen
        {
            get => areWindowsOpen;
            set
            {
                areWindowsOpen = value;
                OnPropertyChanged(nameof(AreWindowsOpen));

                UpdateTemperatureTips();
                OnPropertyChanged(nameof(TemperatureTips));
            }
        }
        private bool isVentilationOpen;
        public bool IsVentilationOpen
        {
            get => isVentilationOpen;
            set
            {
                isVentilationOpen = value;
                OnPropertyChanged(nameof(IsVentilationOpen));

                UpdateHumidityTips();
                OnPropertyChanged(nameof(HumidityTips));
            }
        }
        private bool isWateringOpen;
        public bool IsWateringOpen
        {
            get => isWateringOpen;
            set
            {
                isWateringOpen = value;
                OnPropertyChanged(nameof(IsWateringOpen));

                UpdateHumidityTips();
                OnPropertyChanged(nameof(HumidityTips));
            }
        }
        private string criticalTaskMessage = "";
        public string CriticalTaskMessage
        {
            get
            {
                if (tasksServices.DoesTodayHasCriticalTask())
                    return "ATTENTION - Il y a un événement critique prévu aujourd'hui.";

                return "";
            }
            set { criticalTaskMessage = value; }
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
        private string weatherPrompt = "";
        public string WeatherPrompt
        {
            get
            {
                if (CurrentWeather == null)
                    return "";

                return $"Météo :\n" +
                    $"Date : {CurrentWeather.Date.DayOfWeek} {CurrentWeather.Date.ToString("d MMMM yyyy")}, {CurrentWeather.Date.ToString("HH:mm")}\n" +
                    $"Température : {CurrentWeather.Temperature}°C\n" +
                    $"Humidité : {CurrentWeather.Humidity}%\n" +
                    $"Luminiosité : {CurrentWeather.Luminosity} lux";
            }
            set { weatherPrompt = value; }
        }

        private string temperatureTips = "";
        public string TemperatureTips
        {
            get
            {
                UpdateTemperatureTips();
                return temperatureTips;
            }
            set { temperatureTips = value; }
        }
        private string humidityTips = "";
        public string HumidityTips
        {
            get
            {
                UpdateHumidityTips();
                return humidityTips;
            }
            set { humidityTips = value; }
        }
        private string luminiosityTips = "";
        public string LuminiosityTips
        {
            get
            {
                UpdateLuminiosityTips();
                return luminiosityTips;
            }
            set { luminiosityTips = value; }
        }

        public List<Weather> Weathers { get; set; }
        public Weather? CurrentWeather { get; set; }

        public ICommand GoToCalendarCommand { get; }
        public ICommand SignOutCommand { get; }
        public ICommand ToggleWeatherReadingCommand { get; }

        private int currentWeatherIndex = -1;
        private Timer? readWeatherTimer;

        public event PropertyChangedEventHandler? PropertyChanged;

        public HomeViewModel(
            Window openedWindow, INavigationUtils navigationUtils, ITasksServices tasksServices, IWeatherReader weatherReader)
        {
            window = openedWindow;
            this.navigationUtils = navigationUtils;
            this.tasksServices = tasksServices;
            this.weatherReader = weatherReader;

            GoToCalendarCommand = new RelayCommand(GoToCalendar);
            SignOutCommand = new RelayCommand(SignOut);
            ToggleWeatherReadingCommand = new RelayCommand(ToggleWeatherReading);

            Weathers = this.weatherReader.ReadWeather();
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

            OnCurrentWeatherChange();
        }

        private void GetNextWeather(object? stateInfo)
        {
            currentWeatherIndex++;

            if (currentWeatherIndex == Weathers.Count)
                readWeatherTimer!.Dispose();
            else
                CurrentWeather = Weathers[currentWeatherIndex];

            OnCurrentWeatherChange();
        }

        private void OnCurrentWeatherChange()
        {
            OnPropertyChanged(nameof(WeatherPrompt));
            OnPropertyChanged(nameof(TemperatureTips));
            OnPropertyChanged(nameof(HumidityTips));
            OnPropertyChanged(nameof(LuminiosityTips));
        }

        private void UpdateHumidityTips()
        {
            if (CurrentWeather == null)
                humidityTips = "";
            else
                humidityTips = WeatherTips.GetHumidityTips(isWateringOpen, isVentilationOpen, CurrentWeather.Humidity, CurrentWeather.Date);
        }

        private void UpdateTemperatureTips()
        {
            if (CurrentWeather == null)
                temperatureTips = "";
            else
                temperatureTips = WeatherTips.GetTemperatureTips(isHeatOpen, areWindowsOpen, CurrentWeather.Temperature);
        }

        private void UpdateLuminiosityTips()
        {
            if (CurrentWeather == null)
                luminiosityTips = "";
            else
                luminiosityTips = WeatherTips.GetLuminiosityTips(areLightsOpen, CurrentWeather.Luminosity, CurrentWeather.Date);
        }
    }
}
