using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.UI.Dialog;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private int _animationSpeed = 10;

        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _dialogueText;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private GameObject _container;

        private DialogueLine _currentLine => _dialogue.Lines[_lineIndex];

        private ICoroutineRunner _coroutineRunner;
        private StringBuilder _stringBuilder;
        private Coroutine _coroutine;
        private Dialogue _dialogue;
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

            if (!Input.GetButtonUp("Fire1"))
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
                Hide();
                return;
            }

            ShowCurrentLine();
        }

        public void Show(Dialogue dialogue)
        {
            Clear();

            this._dialogue = dialogue;

            _started = true;
            _container.SetActive(true);
            ShowCurrentLine();
        }

        public void Hide()
        {
            _container.SetActive(false);
            _started = false;

            if (_coroutine != null)
                _coroutineRunner.StopCoroutine(_coroutine);
        }

        private void Clear()
        {
            _nameText.text = "";
            _dialogueText.text = "";

            _stringBuilder.Clear();
            _coroutine= null;
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
                _stringBuilder.Append(_currentLine.Text[i]);
                _dialogueText.SetText(_stringBuilder.ToString());

                yield return delay;
            }

            _coroutine = null;
        }

        private void ShowCurrentLine() =>
            _coroutine = _coroutineRunner.StartCoroutine(AnimateLine());
    }
}
