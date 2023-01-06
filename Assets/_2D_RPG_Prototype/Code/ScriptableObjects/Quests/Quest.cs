using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = ResourcesMenu.QUESTS + "Quest", order = 0)]
    public class Quest : ScriptableObject
    {
        [SerializeField] private int _id;
        [SerializeField] private string _name;
        [SerializeField] private string _description;

        public event Action OnComplete;
        public event Action OnStateChanged;

        private string SaveKey => $"{SaveKeys.QUEST}_{_id}";

        public int Id => _id;
        public string Name => _name;
        public bool IsActive
        {
            get => _isActiv;
            set
            {
                _isActiv = value;
                OnStateChanged?.Invoke();
            }
        }
        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;

                if (_isCompleted)
                    OnComplete?.Invoke();

                OnStateChanged?.Invoke();
            }
        }

        private bool _isActiv;
        private bool _isCompleted;

        public void Load()
        {
            var savedData = SaveLoadService.Load(SaveKey, default(QuestState));

            _isActiv = savedData.ActiveState;
            _isCompleted = savedData.CompletedState;
        }

        public void Save()
        {
            var dataToSave = new QuestState(_isActiv, _isCompleted);
            SaveLoadService.Save(SaveKey, dataToSave);
        }

        public virtual string GetDescription() =>
            _description.ToLower();
    }
}
