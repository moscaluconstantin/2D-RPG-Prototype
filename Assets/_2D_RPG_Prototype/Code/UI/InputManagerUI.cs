using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using Assets._2D_RPG_Prototype.Code.UI.Menu;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI
{
    public class InputManagerUI : MonoBehaviour
    {
        [SerializeField] UIService _uiService;

        private InGameMenu _inGameMenu;

        private void Start() =>
            _inGameMenu = _uiService.GetWindow<InGameMenu>();

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (_uiService.ActiveWindow is IControlableWindow window)
                    window.Hide();
                else
                    _inGameMenu.Show();

                return;
            }

        }
    }
}
