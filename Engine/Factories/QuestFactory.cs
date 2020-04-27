using Engine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            // Declare the items needed to complete the quest and its reward items
            var itemsToComplete = new List<ItemQuantity>()
            {
                new ItemQuantity(9001, 5)
            };
            var rewardItems = new List<ItemQuantity>()
            {
                new ItemQuantity(1002, 1)
            };

            // Create the quest
            _quests.Add(new Quest(1,
                "Clear the herb garden",
                "Defeat the snakes in the Herbalist's garden",
                itemsToComplete,
                25, 10,
                rewardItems));
        }

        internal static Quest GetQuestByID(int id) => _quests.FirstOrDefault(quest => quest.ID == id);
    }
}
