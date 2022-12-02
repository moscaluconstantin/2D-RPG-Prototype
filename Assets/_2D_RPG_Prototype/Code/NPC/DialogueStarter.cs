using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public class DialogueStarter : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;
        [SerializeField] private float _cooldown = 2;
        [SerializeField] private bool _hasName = true;

        public bool CanStart => !_started &&
            _playermovement != null &&
            Time.time - _completionTime >= _cooldown;

        private DialogueManager _dialogueManager;
        private PlayerMovement _playermovement;
        private bool _started = false;
        private float _completionTime = 0;

        private void Start() =>
            _dialogueManager = ServiceProvider.GetService<IUIService>().DialogueManager;

        private void Update()
        {
            if (!CanStart || !Input.GetButtonUp("Fire1"))
                return;

            _started = true;
            _playermovement.SetMovementState(false);
            _dialogueManager.Show(_dialogue, _hasName, OnDialogueCompleted);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerMovement playerMovement))
                _playermovement = playerMovement;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerMovement _))
                _playermovement = null;
        }

        private void OnDialogueCompleted()
        {
            _completionTime = Time.time;
            _started = false;
            _playermovement.SetMovementState(true);
        }
    }
}
