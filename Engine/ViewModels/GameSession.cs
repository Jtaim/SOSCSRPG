﻿using Engine.EventArgs;
using Engine.Factories;
using Engine.Models;
using System;
using System.Linq;

namespace Engine.ViewModels
{
    /// <summary>
    /// Model View - View Model 
    /// </summary>
    public class GameSession : BaseNotificationClass
    {
        public event EventHandler<GameMessageEventArgs> OnMessageRaised;

        #region Properties

        private Location _currentLocation;
        private Monster _currentMonster;

        public World CurrentWorld { get; set; }
        public Player CurrentPlayer { get; set; }
        public Location CurrentLocation
        {
            get => _currentLocation;
            set
            {
                _currentLocation = value;

                OnPropertyChanged(nameof(CurrentLocation));
                OnPropertyChanged(nameof(HasLocationToNorth));
                OnPropertyChanged(nameof(HasLocationToEast));
                OnPropertyChanged(nameof(HasLocationToWest));
                OnPropertyChanged(nameof(HasLocationToSouth));

                GivePlayerQuestAtLocation();
                GetMonsterAtLocation();
            }
        }

        public Monster CurrentMonster
        {
            get => _currentMonster;
            set
            {
                _currentMonster = value;

                OnPropertyChanged(nameof(CurrentMonster));
                OnPropertyChanged(nameof(HasMonster));

                if (CurrentMonster != null) {
                    RaiseMessage("");
                    RaiseMessage($"You see a {CurrentMonster.Name} here!");
                }
            }
        }

        public Weapon CurrentWeapon { get; set; }

        public bool HasLocationToNorth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1) != null;

        public bool HasLocationToEast => CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate) != null;

        public bool HasLocationToSouth => CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1) != null;

        public bool HasLocationToWest => CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate) != null;

        public bool HasMonster => CurrentMonster != null;

        #endregion

        public GameSession()
        {
            CurrentPlayer = new Player {
                Name = "James",
                CharacterClass = "Fighter",
                HitPoints = 10,
                Gold = 1000000,
                ExperiencePoints = 0,
                Level = 1
            };

            CurrentWorld = WorldFactory.CreateWorld();

            CurrentLocation = CurrentWorld.LocationAt(0, 0);
        }

        public void MoveNorth()
        {
            if (HasLocationToNorth) {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate + 1);
            }
        }

        public void MoveEast()
        {
            if (HasLocationToEast) {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate + 1, CurrentLocation.YCoordinate);
            }
        }

        public void MoveSouth()
        {
            if (HasLocationToSouth) {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate, CurrentLocation.YCoordinate - 1);
            }
        }

        public void MoveWest()
        {
            if (HasLocationToWest) {
                CurrentLocation = CurrentWorld.LocationAt(CurrentLocation.XCoordinate - 1, CurrentLocation.YCoordinate);
            }
        }

        private void GivePlayerQuestAtLocation()
        {
            foreach (var quest in CurrentLocation.QuestAvailableHere) {
                if (!CurrentPlayer.Quests.Any(q => q.PlayerQuest.ID == quest.ID)) {
                    CurrentPlayer.Quests.Add(new QuestStatus(quest));
                }
            }
        }

        private void GetMonsterAtLocation() => CurrentMonster = CurrentLocation.GetMonster();

        private void RaiseMessage(string message) => OnMessageRaised?.Invoke(this, new GameMessageEventArgs(message));
    }
}
