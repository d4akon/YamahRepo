using System.Windows;
using System.Collections.Generic;
using Yamah.Core;
using Yamah.Models;

namespace Yamah.ViewModels
{
    internal class MenuViewModel : ObservableObject
    {
        public User? User { get; set; } = LoginViewModel.User;
        /* Comands */
        public RelayCommand MoveWindowCommand { get; set; }

        public RelayCommand ShutdownWindowCommand { get; set; }

        public RelayCommand MinimizeWindowCommand { get; set; }

        public RelayCommand MaximizeWindowCommand { get; set; }

        private int _currentView;

        public int CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel()
        {
            Application.Current.MainWindow.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;

            MoveWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.DragMove(); });

            ShutdownWindowCommand = new RelayCommand(o => { Application.Current.Shutdown(); });

            MinimizeWindowCommand = new RelayCommand(o => { Application.Current.MainWindow.WindowState = WindowState.Minimized; });

            MaximizeWindowCommand = new RelayCommand(o =>
            {
                if (Application.Current.MainWindow.WindowState == WindowState.Maximized)
                    Application.Current.MainWindow.WindowState = WindowState.Normal;
                else
                    Application.Current.MainWindow.WindowState = WindowState.Maximized;
            });
        }
    }
}