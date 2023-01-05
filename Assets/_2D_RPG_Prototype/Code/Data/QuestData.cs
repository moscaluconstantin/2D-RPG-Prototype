using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestActions;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestCheckActions;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public struct QuestData
    {
        [SerializeField] private Dialogue? _onStartDialogue;
        [SerializeField] private Dialogue? _onCompleteDialogue;
        [SerializeField] private QuestAction[] _onStartActions;
        [SerializeField] private QuestAction[] _onCompleteActions;
        [SerializeField] private QuestCheckAction[] _completionCheckActions;
        [SerializeField] private Quest _quest;
    }
}
