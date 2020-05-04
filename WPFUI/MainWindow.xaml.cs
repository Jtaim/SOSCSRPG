﻿using Engine.EventArgs;
using Engine.Models;
using Engine.ViewModels;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;

namespace WPFUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameSession _gameSession = new GameSession();
        public MainWindow()
        {
            InitializeComponent();

            _gameSession.OnMessageRaised += OnGameMessageRaised;

            DataContext = _gameSession;
        }

        private void OnButton_MoveNorth(object sender, RoutedEventArgs e)
            => _gameSession.MoveNorth();

        private void OnButton_MoveWest(object sender, RoutedEventArgs e)
            => _gameSession.MoveWest();

        private void OnButton_MoveEast(object sender, RoutedEventArgs e)
            => _gameSession.MoveEast();

        private void OnButton_MoveSouth(object sender, RoutedEventArgs e)
            => _gameSession.MoveSouth();

        private void OnClick_AttackMonster(object sender, RoutedEventArgs e)
            => _gameSession.AttackCurrentMonster();

        private void OnClick_UseCurrentConsumable(object sender, RoutedEventArgs e)
            => _gameSession.UseCurrentConsumable();

        private void OnGameMessageRaised(object sender, GameMessageEventArgs e)
        {
            GameMessages.Document.Blocks.Add(new Paragraph(new Run(e.Message)));
            GameMessages.ScrollToEnd();
        }

        private void OnClick_DsiplayTradeScreen(object sender, RoutedEventArgs e)
        {
            var tradeScreen = new TradeScreen {
                Owner = this,
                DataContext = _gameSession
            };
            tradeScreen.ShowDialog();
        }

        private void OnClick_Craft(object sender, RoutedEventArgs e)
        {
            Recipe recipe = ((FrameworkElement)sender).DataContext as Recipe;
            _gameSession.CraftItemUsing(recipe);
        }

        private void Window_OnKeyDown(object sender, KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch(e.Key) {
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
