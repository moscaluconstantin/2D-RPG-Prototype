using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.ScriptableObjects.Quests.QuestActions
{
    [CreateAssetMenu(fileName = ResourcesMenu.FILE_NAME, menuName = ResourcesMenu.QUEST_ACTIONS + "Start Dialogue", order = 0)]
    public class StartDialogueAction : QuestAction
    {
        [SerializeField] private Dialogue _dialogue;

        public override void Execute() =>
            ServiceProvider.GetService<IUIService>()
            .GetWindow<DialogueManager>()
            .Show(_dialogue, true);
    }
}
