﻿using Automate.Utils;
using Automate.Utils.WeatherUtils;
using Automate.ViewModels;
using System.Windows;

namespace Automate.Views
{
    /// <summary>
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        public HomeWindow()
        {
            InitializeComponent();
            DataContext = new HomeViewModel(this, new NavigationUtils(), Environment.tasksServices, new WeatherReader());
        }
    }
}

