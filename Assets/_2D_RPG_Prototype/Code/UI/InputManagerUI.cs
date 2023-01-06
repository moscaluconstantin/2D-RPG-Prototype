using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI.Menu;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI
{
    public class InputManagerUI : MonoBehaviour
    {
        [SerializeField] UIService _uiService;

        private InGameMenu _inGameMenu;
        private IScreenFader _screeFader;

        private void Start()
        {
            _inGameMenu = _uiService.GetWindow<InGameMenu>();
            _screeFader = ServiceProvider.GetService<IScreenFader>();
        }

        private void Update()
        {
            if (_screeFader.IsFading)
                return;

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (_uiService.ActiveWindow is IControlableWindow window)
                    window.Hide();
                else
                    _inGameMenu.Show();

                return;
            }

            if (Input.GetKeyUp(KeyCode.I))
            {
                if (_uiService.AnyWindowActive && _uiService.ActiveWindow != _inGameMenu)
                    return;

                if (!_uiService.AnyWindowActive)
                    _inGameMenu.Show();

                _inGameMenu.OpenContent(Enums.MenuContentType.Inventory);
            }
        }
    }
}
