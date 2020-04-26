using System;
using System.Windows;
using System.Windows.Input;
using Engine.ViewModels;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession;
        public MainWindow()
        {
            InitializeComponent();

            _gameSession = new GameSession();
            DataContext = _gameSession;
        }

        private void OnButton_MoveNorth(object sender, RoutedEventArgs e) => _gameSession.MoveNorth();

        private void OnButton_MoveWest(object sender, RoutedEventArgs e) => _gameSession.MoveWest();

        private void OnButton_MoveEast(object sender, RoutedEventArgs e) => _gameSession.MoveEast();

        private void OnButton_MoveSouth(object sender, RoutedEventArgs e) => _gameSession.MoveSouth();

        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key) {
                case Key.W:
                case Key.Up:
                    if (_gameSession.HasLocationToNorth) {
                        _gameSession.MoveNorth();
                    }
                    break;
                case Key.A:
                case Key.Left:
                    if (_gameSession.HasLocationToWest) {
                        _gameSession.MoveWest();
                    }
                    break;
                case Key.D:
                case Key.Right:
                    if (_gameSession.HasLocationToEast) {
                        _gameSession.MoveEast();
                    }
                    break;
                case Key.S:
                case Key.Down:
                    if (_gameSession.HasLocationToSouth) {
                        _gameSession.MoveSouth();
                    }
                    break;
                case Key.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }
    }
}
