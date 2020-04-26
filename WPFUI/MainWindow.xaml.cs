using Engine.ViewModels;
using System.Windows;
using System.Windows.Input;

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
                    _gameSession.MoveNorth();
                    break;
                case Key.A:
                case Key.Left:
                    _gameSession.MoveWest();
                    break;
                case Key.D:
                case Key.Right:
                    _gameSession.MoveEast();
                    break;
                case Key.S:
                case Key.Down:
                    _gameSession.MoveSouth();
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
