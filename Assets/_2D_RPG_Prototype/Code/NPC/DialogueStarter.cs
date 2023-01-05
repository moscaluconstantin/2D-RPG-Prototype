using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public class DialogueStarter : InteractableNPC
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private float _cooldown = 2;
        [SerializeField] private bool _hasName = true;

        private DialogueManager _dialogueManager;
        private float _completionTime = 0;

        private void Start() =>
            _dialogueManager = ServiceProvider.GetService<IUIService>().GetWindow<DialogueManager>();

        protected override void OnTriggered()
        {
            if (Time.time - _completionTime >= _cooldown)
                _dialogueManager.Show(_dialogue, _hasName, OnDialogueCompleted);
        }

        private void OnDialogueCompleted() =>
            _completionTime = Time.time;
    }
}
