using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests;
using UnityEngine;
using System.Linq;

namespace Assets._2D_RPG_Prototype.Code
{
    public class ToggleByQuestState : MonoBehaviour
    {
        [SerializeField] private Quest _quest;
        [SerializeField] private QuestState[] _statesToCheck;
        [SerializeField] private bool _initialState;

        private bool CanChangeState => _statesToCheck.Any(x =>
        x.ActiveState == _quest.IsActive &&
        x.CompletedState == _quest.IsCompleted);

        private void Start()
        {
            gameObject.SetActive(_initialState);

            _quest.OnStateChanged += TryChangeState;

            TryChangeState();
        }

        private void OnDestroy() =>
            _quest.OnStateChanged -= TryChangeState;

        private void TryChangeState()
        {
            if (!CanChangeState)
                return;

            _quest.OnStateChanged -= TryChangeState;
            gameObject.SetActive(!_initialState);
        }
    }
}
