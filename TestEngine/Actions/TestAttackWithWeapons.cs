using System;
using Engine.Actions;
using Engine.Factories;
using Engine.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.Actions
{
    [TestClass]
    public class TestAttackWithWeapons
    {
        [TestMethod]
        public void Test_Constructor_GoodParameters()
        {
            var pointyStick = ItemFactory.CreateGameItem(1001);

            var attackWithWeapon = new AttackWithWeapon(pointyStick, 1, 5);

            Assert.IsNotNull(attackWithWeapon);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_ItemIsNotAWeapon()
        {
            var granolaBar = ItemFactory.CreateGameItem(2001);

            // A granola bar is not a weapon.
            // So, the constructor should throw an exception.
            var attackWithWeapon = new AttackWithWeapon(granolaBar, 1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_MinimumDamageLessThanZero()
        {
            var pointyStick = ItemFactory.CreateGameItem(1001);

            // This minimum damage value is less than 0.
            // So, the constructor should throw an exception.
            var attackWithWeapon = new AttackWithWeapon(pointyStick, -1, 5);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_MaximumDamageLessThanMinimumDamage()
        {
            var pointyStick = ItemFactory.CreateGameItem(1001);

            // This maximum damage is less than the minimum damage.
            // So, the constructor should throw an exception.
            var attackWithWeapon = new AttackWithWeapon(pointyStick, 2, 1);
        }
    }
}
