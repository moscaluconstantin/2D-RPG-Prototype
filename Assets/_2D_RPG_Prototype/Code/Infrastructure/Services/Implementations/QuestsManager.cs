using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class QuestsManager : IQuestsManager
    {
        private Quest[] _quests;

        public QuestsManager()
        {
            _quests = Resources.LoadAll<Quest>(ResourcePaths.QUESTS);

            foreach (var quest in _quests)
                quest.Initialize();
        }
    }
}
