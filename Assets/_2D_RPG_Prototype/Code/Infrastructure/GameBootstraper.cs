using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure
{
    public class GameBootstraper : MonoBehaviour
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private ScreenFader _screenFader;
        [SerializeField] private SceneLoader _sceneLoader;

        private void Start()
        {
            InitServices();

            _sceneLoader.LoadScene("Countryside");
        }

        private void InitServices()
        {
            _screenFader.Initialize(_coroutineRunner);
            _sceneLoader.Initialize(_coroutineRunner, _screenFader);

            ServiceProvider.CoroutineRunner = _coroutineRunner;
            ServiceProvider.ScreenFader = _screenFader;
            ServiceProvider.SceneLoader = _sceneLoader;
            ServiceProvider.SaveLoadService = new SaveLoadService();
        }
    }
}
