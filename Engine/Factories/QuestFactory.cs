using System.Collections.Generic;
using System.Linq;
using Engine.Models;

namespace Engine.Factories
{
    internal static class QuestFactory
    {
        private static readonly List<Quest> _quests = new List<Quest>();

        static QuestFactory()
        {
            // Create the quest
            _quests.Add(new Quest(1,
                "Clear the herb garden",
                "Defeat the snakes in the Herbalist's garden",
                new List<ItemQuantity>() { new ItemQuantity(9001, 5) },
                25, 10,
                new List<ItemQuantity>() { new ItemQuantity(1002, 1) }));
        }

        internal static Quest GetQuestByID(int id) => _quests.FirstOrDefault(quest => quest.ID == id);
    }
}
