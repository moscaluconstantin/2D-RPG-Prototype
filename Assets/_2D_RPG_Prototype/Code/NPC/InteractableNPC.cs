using Assets._2D_RPG_Prototype.Code.Constants;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public abstract class InteractableNPC : MonoBehaviour
    {
        protected bool Triggered => PlayerInRange && Input.GetButtonUp(InputConstants.FIRE_1);

        protected PlayerMovement Player;
        protected bool PlayerInRange = false;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerMovement playerMovement))
            {
                Player = playerMovement;
                PlayerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent(out PlayerMovement _))
            {
                Player = null;
                PlayerInRange = false;
            }
        }
    }
}
