using Engine.Models;
using Engine.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestEngine.Services
{
    [TestClass]
    public class TestCombatService
    {
        [TestMethod]
        public void Test_FirstAttacker()
        {
            // Player and monster with dexterity 12
            var player = new Player("", "", 0, 12, 0, 18, 0);
            var monster = new Monster(0, "", "", 12, 0, null, 0, 0);

            var result = CombatService.FirstAttacker(player, monster);
        }
    }
}
