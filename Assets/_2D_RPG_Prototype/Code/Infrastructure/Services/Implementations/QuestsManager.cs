using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class QuestsManager : IQuestsManager
    {
        private ISaveLoadService _saveLoadService;
        private Quest[] _quests;

        public QuestsManager(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _quests = Resources.LoadAll<Quest>(ResourcePaths.QUESTS);

            LoadAll();

            _saveLoadService.OnSave += SaveAll;
        }

        private void LoadAll()
        {
            foreach (var quest in _quests)
                quest.Load();
        }

        private void SaveAll()
        {
            foreach (var quest in _quests)
                quest.Save();
        }
    }
}
