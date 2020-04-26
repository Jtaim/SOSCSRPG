﻿using System;
using Engine.Models;

namespace Engine.ViewModels
{
    public class GameSession
    {
        Player CurrentPlayer { get; set; }

        public GameSession()
        {
            CurrentPlayer = new Player {
                Name = "James",
                Gold = 1000000
            };
        }

    }
}
