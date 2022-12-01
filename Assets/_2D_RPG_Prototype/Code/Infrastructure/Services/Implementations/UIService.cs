using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class UIService : MonoBehaviour, IUIService
    {
        [SerializeField] DialogueManager _dialogManager;

        public DialogueManager DialogManager => _dialogManager;

        private void Awake() =>
            ServiceProvider.AddService<IUIService>(this);
    }
}
