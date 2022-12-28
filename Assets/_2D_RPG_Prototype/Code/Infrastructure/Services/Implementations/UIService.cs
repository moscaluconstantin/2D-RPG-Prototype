using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class UIService : MonoBehaviour, IUIService
    {
        public bool AnyWindowActive => _activeWindow != null;
        public UIWindow ActiveWindow => _activeWindow;

        private Dictionary<Type, UIWindow> _windows;
        private UIWindow _activeWindow;

        private void Awake()
        {
            _windows = new Dictionary<Type, UIWindow>();
            ServiceProvider.AddService<IUIService>(this);

            var windows = GetComponentsInChildren<UIWindow>();
            foreach (var window in windows)
                window.Initialize(this);
        }

        public void AddWindow<T>(UIWindow window) where T : UIWindow
        {
            if (_windows.ContainsKey(typeof(T)))
            {
                _windows[typeof(T)] = window;
                return;
            }

            _windows.Add(typeof(T), window);
        }

        public T GetWindow<T>() where T : UIWindow
        {
            if (_windows.TryGetValue(typeof(T), out var window))
                return (T)window;

            return default;
        }

        public void SetActiveWindow(UIWindow uiWindow) =>
            _activeWindow = uiWindow;

        public void ClearActiveWindow() =>
            _activeWindow = null;
    }
}
