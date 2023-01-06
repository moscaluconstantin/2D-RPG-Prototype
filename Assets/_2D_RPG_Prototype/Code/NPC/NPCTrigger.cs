using Assets._2D_RPG_Prototype.Code.Constants;
using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public class NPCTrigger : MonoBehaviour
    {
        public event Action OnTrigger;

        private bool _playerInRange = false;

        private void Update()
        {
            if (_playerInRange && Input.GetButtonUp(InputConstants.FIRE_1))
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
