using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public class NPCTrigger : MonoBehaviour
    {
        public event Action OnTrigger;

        private bool _playerInRange = false;
        private IUIService _uiService;

        private void Awake() =>
            _uiService = ServiceProvider.GetService<IUIService>();

        private void Update()
        {
            if (_playerInRange && Input.GetButtonUp(InputConstants.FIRE_1) && !_uiService.AnyWindowActive)
                OnTrigger?.Invoke();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerMovement _))
                _playerInRange = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerMovement _))
                _playerInRange = false;
        }
    }
}
