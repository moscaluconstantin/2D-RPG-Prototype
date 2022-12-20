using Assets._2D_RPG_Prototype.Code.Enums;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using System.Collections;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class ScreenFader : MonoBehaviour, IScreenFader
    {
        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private CanvasGroup _curtain;

        private ICoroutineRunner _coroutineRunner;
        private Coroutine _currentFade;

        public void Initialize(ICoroutineRunner coroutineRunner) =>
            _coroutineRunner = coroutineRunner;

        public void FadeInstant(FadeType fadeType) =>
            _curtain.alpha = fadeType == FadeType.FadeIn ? 1 : 0;

        public Coroutine Fade(FadeType fadeType)
        {
            if (_currentFade != null)
                _coroutineRunner.StopCoroutine(_currentFade);

            _currentFade = _coroutineRunner.StartCoroutine(FadeCoroutine(fadeType));
            return _currentFade;
        }

        private IEnumerator FadeCoroutine(FadeType fadeType)
        {
            float duration = _fadeDuration;

            while (duration > 0f)
            {
                duration -= Time.deltaTime;
                var lerpValue = Mathf.InverseLerp(_fadeDuration, 0, duration);

                _curtain.alpha = fadeType == FadeType.FadeIn ? lerpValue : 1 - lerpValue;

                yield return null;
            }

            _currentFade = null;
        }
    }
}
