﻿using System;
using System.Collections.Generic;
using Engine.Services;
using Newtonsoft.Json;

namespace Engine.Models
{
    public abstract class LivingEntity : BaseNotificationClass
    {
        #region Properties

        private string _name;
        private int _dexterity;
        private int _currentHitPoints;
        private int _maximumHitPoints;
        private int _gold;
        private int _level;
        private GameItem _currentWeapon;
        private GameItem _currentConsumable;
        private Inventory _inventory;

        public string Name {
            get => _name;
            private set {
                _name = value;
                OnPropertyChanged();
            }
        }

        public int Dexterity {
            get => _dexterity;
            private set {
                _dexterity = value;
                OnPropertyChanged();
            }
        }

        public int CurrentHitPoints {
            get => _currentHitPoints;
            private set {
                _currentHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int MaximumHitPoints {
            get => _maximumHitPoints;
            protected set {
                _maximumHitPoints = value;
                OnPropertyChanged();
            }
        }

        public int Gold {
            get => _gold;
            private set {
                _gold = value;
                OnPropertyChanged();
            }
        }

        public int Level {
            get => _level;
            protected set {
                _level = value;
                OnPropertyChanged();
            }
        }

        public Inventory Inventory {
            get => _inventory;
            private set {
                _inventory = value;
                OnPropertyChanged();
            }
        }

        public GameItem CurrentWeapon {
            get => _currentWeapon;
            set {
                if(_currentWeapon != null) {
                    _currentWeapon.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentWeapon = value;

                if(_currentWeapon != null) {
                    _currentWeapon.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }

                OnPropertyChanged();
            }
        }

        public GameItem CurrentConsumable {
            get => _currentConsumable;
            set {
                if(_currentConsumable != null) {
                    _currentConsumable.Action.OnActionPerformed -= RaiseActionPerformedEvent;
                }

                _currentConsumable = value;

                if(_currentConsumable != null) {
                    _currentConsumable.Action.OnActionPerformed += RaiseActionPerformedEvent;
                }

                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public bool IsAlive => CurrentHitPoints > 0;
        [JsonIgnore]
        public bool IsDead => !IsAlive;

        #endregion

        public event EventHandler<string> OnActionPerformed;
        public event EventHandler OnKilled;

        protected LivingEntity(string name, int dexterity, int maximumHitPoints, int currentHitPoints, int gold,
                               int level = 1)
        {
            Name = name;
            Dexterity = dexterity;
            MaximumHitPoints = maximumHitPoints;
            CurrentHitPoints = currentHitPoints;
            Gold = gold;
            Level = level;

            Inventory = new Inventory();
        }

        public void UseCurrentWeaponOn(LivingEntity target) => CurrentWeapon.PerformAction(this, target);

        public void UseCurrentConsumable()
        {
            CurrentConsumable.PerformAction(this, this);
            RemoveItemFromInventory(CurrentConsumable);
        }

        public void TakeDamage(int hitPointsOfDamage)
        {
            CurrentHitPoints -= hitPointsOfDamage;

            if(IsDead) {
                CurrentHitPoints = 0;
                RaiseOnKilledEvent();
            }
        }

        public void Heal(int hitPointsToHeal)
        {
            CurrentHitPoints += hitPointsToHeal;

            if(CurrentHitPoints > MaximumHitPoints) {
                CurrentHitPoints = MaximumHitPoints;
            }
        }

        public void CompletelyHeal() => CurrentHitPoints = MaximumHitPoints;

        public void ReceiveGold(int amountOfGold) => Gold += amountOfGold;

        public void SpendGold(int amountOfGold)
        {
            if(amountOfGold > Gold) {
                throw new ArgumentOutOfRangeException($"{Name} only has {Gold} gold, and cannot spend {amountOfGold} gold");
            }
            Gold -= amountOfGold;
        }

        public void AddItemToInventory(GameItem item)
        {
            Inventory = Inventory.AddItem(item);
            DefaultItemsSelection();
        }

        public void RemoveItemFromInventory(GameItem item)
        {
            Inventory = Inventory.RemoveItem(item);
            DefaultItemsSelection();
        }

        public void RemoveItemsFromInventory(List<ItemQuantity> itemQuantities)
        {
            Inventory = Inventory.RemoveItems(itemQuantities);
        }

        #region Private functions

        private void RaiseOnKilledEvent() =>
            OnKilled?.Invoke(this, new System.EventArgs());

        private void RaiseActionPerformedEvent(object sender, string result) =>
            OnActionPerformed?.Invoke(this, result);

        private void DefaultItemsSelection()
        {
            // keep weapon combo-box populated 
            if(CurrentWeapon == null && Inventory.Weapons.Count != 0) {
                var weapons = Inventory.Weapons;
                CurrentWeapon = weapons[0];
            }

            // keep consumable combo-box populated 
            if(Inventory.HasConsumable) {
                var consumable = Inventory.Consumables;
                CurrentConsumable = consumable[0];
            }
        }

        #endregion
    }
}
