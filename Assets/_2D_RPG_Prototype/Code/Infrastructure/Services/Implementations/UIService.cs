using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using Assets._2D_RPG_Prototype.Code.UI.ItemsShop;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class UIService : MonoBehaviour, IUIService
    {
        [SerializeField] DialogueManager _dialogManager;
        [SerializeField] Shop _shop;

        public DialogueManager DialogueManager => _dialogManager;
        public Shop Shop => _shop;

        private void Awake() =>
            ServiceProvider.AddService<IUIService>(this);
    }
}
