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

        public event Action completed;

        public int Id => _id;
        public string Name => _name;

        public bool IsCompleted
        {
            get => _isCompleted;
            set
            {
                _isCompleted = value;

                if (_isCompleted)
                    completed?.Invoke();
            }
        }

        private bool _isCompleted;

        public void Initialize()
        {
            //load saved state
            _isCompleted = false;
        }

        public virtual string GetDescription() =>
            _description.ToLower();
    }
}
