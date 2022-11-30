using Assets._2D_RPG_Prototype.Code.Enums;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class SceneLoader : MonoBehaviour, ISceneLoader
    {
        private ICoroutineRunner _coroutineRunner;
        private IScreenFader _screenFader;

        private string _activeSceneName = "";
        private bool _isLoading = false;

        public void Initialize(ICoroutineRunner coroutineRunner, IScreenFader screenFader)
        {
            _coroutineRunner = coroutineRunner;
            _screenFader = screenFader;
        }

        public void LoadScene(string sceneName)
        {
            if (_isLoading)
                return;

            _isLoading = true;

            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(sceneName));
        }

        private IEnumerator LoadSceneCoroutine(string sceneName)
        {
            if (string.IsNullOrEmpty(_activeSceneName))
            {
                _screenFader.FadeInstant(FadeType.FadeIn);
            }
            else
            {
                yield return _screenFader.Fade(FadeType.FadeIn);
                yield return SceneManager.UnloadSceneAsync(_activeSceneName);
            }

            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
            _activeSceneName = sceneName;

            yield return _screenFader.Fade(FadeType.FadeOut);

            _isLoading = false;
        }
    }
}
