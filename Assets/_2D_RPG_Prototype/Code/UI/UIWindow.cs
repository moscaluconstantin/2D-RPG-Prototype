using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI
{
    public abstract class UIWindow : MonoBehaviour
    {
        protected IUIService UIService;

        public void Initialize(IUIService uiService)
        {
            UIService = uiService;
            AddToUIService();
        }

        protected abstract void AddToUIService();

        protected bool TrySetAsActiveWindow()
        {
            if (UIService.AnyWindowActive)
                return false;

            UIService.SetActiveWindow(this);

            return true;
        }
    }
}
