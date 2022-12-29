using Assets._2D_RPG_Prototype.Code.Enums;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.Menu
{
    public class InGameMenu : UIWindow, IControlableWindow
    {
        [SerializeField] private MenuContentType _defaultContentType;

        [Header("Contents")]
        [SerializeField] private GameObject _charactersWindow;
        [SerializeField] private GameObject _statsWindow;
        [SerializeField] private GameObject _inventoryWindow;

        [Header("Compinents")]
        [SerializeField] private GameObject _container;
        [SerializeField] private Transform _contentButtonsContainer;

        private MenuContentButton[] _contentButtons;
        private Dictionary<MenuContentType, MenuContentButton> _buttonsDictionary;
        private Dictionary<MenuContentType, GameObject> _windowsDictionary;
        private MenuContentType _selectedContentType;

        private void Start()
        {
            InitContentButtons();
            InitContentWindows();

            Close();
        }

        private void OnDestroy()
        {
            foreach (var button in _contentButtons)
                button.OnClicked -= OpenContent;
        }

        public void Show()
        {
            if (!TrySetAsActiveWindow())
                return;

            _container.SetActive(true);
            OpenContent(_defaultContentType);
        }

        public void OpenContent(MenuContentType contentType)
        {
            if (_selectedContentType == contentType)
                return;

            if (_buttonsDictionary.TryGetValue(_selectedContentType, out var selectedButton))
                selectedButton.Deselect();
            _buttonsDictionary[contentType].Select();

            if (_windowsDictionary.TryGetValue(_selectedContentType, out var selectedWindow))
                selectedWindow.SetActive(false);
            _windowsDictionary[contentType].SetActive(true);

            _selectedContentType = contentType;
        }

        public void Save()
        {
            print("Save");
        }

        public void Close() =>
            Hide();

        public void Quit() =>
            Application.Quit();

        void IControlableWindow.Hide() =>
            Hide();

        protected override void AddToUIService() =>
            UIService.AddWindow<InGameMenu>(this);

        private void InitContentButtons()
        {
            _contentButtons = _contentButtonsContainer.GetComponentsInChildren<MenuContentButton>();
            _buttonsDictionary = _contentButtons.ToDictionary(x => x.ContentType, x => x);

            foreach (var button in _contentButtons)
            {
                button.OnClicked += OpenContent;
                button.Deselect();
            }
        }

        private void InitContentWindows()
        {
            _windowsDictionary = new()
            {
                [MenuContentType.Characters] = _charactersWindow,
                [MenuContentType.Stats] = _statsWindow,
                [MenuContentType.Inventory] = _inventoryWindow,
            };

            foreach (var item in _windowsDictionary)
                item.Value.SetActive(false);
        }

        private void Hide()
        {
            UIService.ClearActiveWindow();
            _container.SetActive(false);
        }
    }
}
