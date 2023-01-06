using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestActions;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestCheckActions;
using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public struct QuestData
    {
        [SerializeField] private QuestAction[] _onStartActions;
        [SerializeField] private QuestAction[] _inProgressActions;
        [SerializeField] private QuestAction[] _onCompleteActions;
        [SerializeField] private QuestCheckAction[] _completionCheckActions;
        [SerializeField] private Quest _quest;

        public QuestAction[] OnStartActions => _onStartActions;
        public QuestAction[] InProgressActions => _inProgressActions;
        public QuestAction[] OnCompleteActions => _onCompleteActions;
        public QuestCheckAction[] CompletionCheckActions => _completionCheckActions;
        public Quest Quest => _quest;

        public void RunOnStartActions()
        {
            foreach (var action in _onStartActions)
                action.Execute();
        }

        public void RunInProgressActions()
        {
            foreach (var action in _inProgressActions)
                action.Execute();
        }

        public void RunOnCompleteActions()
        {
            foreach (var action in _onCompleteActions)
                action.Execute();
        }

        public bool CanBeCompleted()
        {
            if (_completionCheckActions == null || _completionCheckActions.Length == 0)
                return false;

            foreach (var checkAction in _completionCheckActions)
            {
                if (!checkAction.Check())
                    return false;
            }

            return true;
        }
    }
}
