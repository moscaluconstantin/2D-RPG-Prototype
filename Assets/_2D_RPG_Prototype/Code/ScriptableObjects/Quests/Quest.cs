using Assets._2D_RPG_Prototype.Code.Constants;
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

        public void Initialize()
        {
            //load saved state
            _isActiv = false;
            _isCompleted = false;
        }

        public virtual string GetDescription() =>
            _description.ToLower();
    }
}
