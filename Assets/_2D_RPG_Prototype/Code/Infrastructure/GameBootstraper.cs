using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
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

            ServiceProvider.AddService<ICoroutineRunner>(_coroutineRunner);
            ServiceProvider.AddService<IScreenFader>(_screenFader);
            ServiceProvider.AddService<ISceneLoader>(_sceneLoader);
            ServiceProvider.AddService<ISaveLoadService>(new SaveLoadService());
        }
    }
}
