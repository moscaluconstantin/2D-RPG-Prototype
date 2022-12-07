using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject _container;
        [SerializeField] private GameObject _statsWindow;

        private bool _isActive;

        private void Start() =>
            Close();

        private void Update()
        {
            if (!Input.GetKeyUp(KeyCode.Escape))
                return;

            Toggle();
        }

        public void OpenItems()
        {
            print("OpenItems");
            _statsWindow.SetActive(false);
        }

        public void OpenStats()
        {
            print("OpenStats");
            _statsWindow.SetActive(true);
        }

        public void Save()
        {
            print("Save");
        }

        public void Close()
        {
            print("Close");
            SetMenuState(false);
        }

        public void Quit()
        {
            print("Quit");
        }

        private void Toggle()
        {
            SetMenuState(!_isActive);

            if (_isActive)
                OpenStats();
        }

        private void SetMenuState(bool state)
        {
            _isActive = state;
            _container.SetActive(state);
        }
    }
}
