using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI;
using System;
using System.Collections;
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
        private ICoroutineRunner _coroutineRunner;

        private void Awake()
        {
            _windows = new Dictionary<Type, UIWindow>();
            _coroutineRunner = ServiceProvider.GetService<ICoroutineRunner>();

            var windows = GetComponentsInChildren<UIWindow>();
            foreach (var window in windows)
                window.Initialize(this);

            ServiceProvider.AddService<IUIService>(this);
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
            _coroutineRunner.StartCoroutine(ClearOnNextFrame());

        private IEnumerator ClearOnNextFrame()
        {
            yield return null;
            _activeWindow = null;
        }
    }
}
