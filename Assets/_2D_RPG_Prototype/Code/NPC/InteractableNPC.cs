using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.NPC
{
    public abstract class InteractableNPC : MonoBehaviour
    {
        [SerializeField] private NPCTrigger _trigger;

        protected virtual void Awake() => 
            _trigger.OnTrigger += OnTriggered;

        protected virtual void OnDestroy() => 
            _trigger.OnTrigger -= OnTriggered;

        protected abstract void OnTriggered();
    }
}
