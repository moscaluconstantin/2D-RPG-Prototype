using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using Assets._2D_RPG_Prototype.Code.UI.ItemsShop;
using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI
{
    public class DialogueManager : UIWindow
    {
        [SerializeField] private int _animationSpeed = 10;

        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private GameObject _container;
        [SerializeField] private GameObject _nameBox;

        private DialogueLine _currentLine => _dialogue.Lines[_lineIndex];

        private ICoroutineRunner _coroutineRunner;
        private StringBuilder _stringBuilder;
        private Coroutine _coroutine;
        private Dialogue _dialogue;
        private Action _onComplete;
        private int _lineIndex;
        private bool _started;

        private void Awake() =>
            _stringBuilder = new StringBuilder();

        private void Start()
        {
            _coroutineRunner = ServiceProvider.GetService<ICoroutineRunner>();
            Hide();
        }

        private void Update()
        {

            if (!_started)
                return;

            if (!Input.GetButtonUp(InputConstants.FIRE_1))
                return;

            if (_coroutine != null)
            {
                _coroutineRunner.StopCoroutine(_coroutine);
                _dialogueText.SetText(_currentLine.Text);
                _coroutine = null;
                return;
            }

            _lineIndex++;

            if (_lineIndex >= _dialogue.Lines.Length)
            {
                _onComplete?.Invoke();
                Hide();
                return;
            }

            ShowCurrentLine();
        }

        public void Show(Dialogue dialogue, bool showName, Action onComplete = null)
        {

            if (!TrySetAsActiveWindow())
                return;

            Clear();

            _nameBox.SetActive(showName);
            _dialogue = dialogue;
            _onComplete = onComplete;

            _started = true;
            _container.SetActive(true);
            ShowCurrentLine();
        }

        private void Hide()
        {
            UIService.ClearActiveWindow();

            _container.SetActive(false);
            _started = false;
            _onComplete = null;

            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);
        }

        protected override void AddToUIService() =>
            UIService.AddWindow<DialogueManager>(this);

        private void Clear()
        {
            _nameText.text = "";
            _dialogueText.text = "";

            _stringBuilder.Clear();
            _coroutine = null;
            _lineIndex = 0;
        }

        private IEnumerator AnimateLine()
        {
            string owner = _currentLine.Owner;

            if (!string.IsNullOrEmpty(owner))
                _nameText.SetText(owner);

            var totalDuration = (float)_currentLine.Text.Length / _animationSpeed;
            var delay = new WaitForSeconds(totalDuration / (_currentLine.Text.Length - 1));

            _stringBuilder.Clear();

            for (int i = 0; i < _currentLine.Text.Length; i++)
            {
                char currentChar = _currentLine.Text[i];
                string textToShow = $"{currentChar}";

                if (currentChar == '<')
                {
                    int lastCharIndex = _currentLine.Text.IndexOf('>', i);
                    textToShow = _currentLine.Text.Substring(i, lastCharIndex - i + 1);
                    i = lastCharIndex;
                }

                _stringBuilder.Append(textToShow);
                _dialogueText.SetText(_stringBuilder.ToString());

                yield return delay;
            }

            _coroutine = null;
        }

        private void ShowCurrentLine() =>
            _coroutine = _coroutineRunner.StartCoroutine(AnimateLine());
    }
}
