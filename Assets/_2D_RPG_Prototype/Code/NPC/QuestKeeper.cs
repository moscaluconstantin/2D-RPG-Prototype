using Assets._2D_RPG_Prototype.Code.Data;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public class QuestKeeper : InteractableNPC
    {
        [SerializeField] private QuestData[] _questsData;
        [SerializeField] private Dialogue _defaultDialogue;

        private DialogueManager _dialogueManager;

        protected override void Awake()
        {
            base.Awake();

            _dialogueManager = ServiceProvider.GetService<IUIService>().GetWindow<DialogueManager>();
        }

        protected override void OnTriggered()
        {
            foreach (var questData in _questsData)
            {
                if (!questData.Quest.IsActive && questData.Quest.IsCompleted)
                    continue;

                if (questData.Quest.IsActive && !questData.Quest.IsCompleted)
                {
                    if (!questData.CanBeCompleted())
                    {
                        questData.RunInProgressActions();
                        return;
                    }

                    questData.Quest.IsCompleted = true;
                }

                if (!questData.Quest.IsActive && !questData.Quest.IsCompleted)
                {
                    questData.RunOnStartActions();
                    questData.Quest.IsActive = true;
                    return;
                }

                if (questData.Quest.IsActive && questData.Quest.IsCompleted)
                {
                    questData.RunOnCompleteActions();
                    questData.Quest.IsActive = false;
                    return;
                }
            }

            _dialogueManager.Show(_defaultDialogue, true);
        }
    }
}
