using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public class DialogueStarter : MonoBehaviour
    {
        [SerializeField] private Dialogue _dialogue;

        private PlayerMovement _playermovement;
        private bool _started = false;

        private void Update()
        {
            if(_started) 
                return;

            if (_playermovement == null)
                return;

            if (Input.GetButtonUp("Fire1"))
            {
                _started = true;
                ServiceProvider.GetService<IUIService>().DialogueManager.Show(_dialogue);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerMovement playerMovement))
                _playermovement = playerMovement;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _playermovement = null;
            _started = false;
        }
    }
}
