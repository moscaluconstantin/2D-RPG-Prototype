using Assets._2D_RPG_Prototype.Code.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.Menu
{
    public class InGameMenu : MonoBehaviour
    {
        [SerializeField] private MenuContentType _defaultContentType;

        [Header("Compinents")]
        [SerializeField] private GameObject _container;
        [SerializeField] private Transform _contentButtonsContainer;

        private MenuContentButton[] _contentButtons;
        private Dictionary<MenuContentType, MenuContentButton> _buttonsDictionary;
        private MenuContentType _selectedContentType;
        private bool _isActive;

        private void Start()
        {
            _contentButtons = _contentButtonsContainer.GetComponentsInChildren<MenuContentButton>();
            _buttonsDictionary = _contentButtons.ToDictionary(x => x.ContentType, x => x);

            foreach (var button in _contentButtons)
            {
                button.OnClicked += OpenContent;
                button.Deselect();
            }

            OpenContent(_defaultContentType);
            Close();
        }

        private void OnDestroy()
        {
            foreach (var button in _contentButtons)
                button.OnClicked -= OpenContent;
        }

        private void Update()
        {
            if (!Input.GetKeyUp(KeyCode.Escape))
                return;

            Toggle();
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

        private void Toggle() =>
            SetMenuState(!_isActive);

        private void SetMenuState(bool state)
        {
            _isActive = state;
            _container.SetActive(state);
        }

        private void OpenContent(MenuContentType contentType)
        {
            if (_selectedContentType == contentType)
                return;

            if (_buttonsDictionary.TryGetValue(_selectedContentType, out var selectedButton))
                selectedButton.Deselect();

            _buttonsDictionary[contentType].Select();
            _selectedContentType = contentType;
        }
    }
}
