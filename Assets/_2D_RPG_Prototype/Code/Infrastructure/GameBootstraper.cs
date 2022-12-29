using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure
{
    public class GameBootstraper : MonoBehaviour
    {
        [SerializeField] private CoroutineRunner _coroutineRunner;
        [SerializeField] private ScreenFader _screenFader;
        [SerializeField] private SceneLoader _sceneLoader;
        [SerializeField] private InventoryService _inventory;

        private void Start()
        {
            InitServices();

            _coroutineRunner.StartCoroutine(Startup());
        }

        private void InitServices()
        {
            _screenFader.Initialize(_coroutineRunner);
            _sceneLoader.Initialize(_coroutineRunner, _screenFader);

            ServiceProvider.AddService<ICoroutineRunner>(_coroutineRunner);
            ServiceProvider.AddService<IScreenFader>(_screenFader);
            ServiceProvider.AddService<ISceneLoader>(_sceneLoader);
            ServiceProvider.AddService<IInventoryService>(_inventory);
            ServiceProvider.AddService<ISaveLoadService>(new SaveLoadService());
            ServiceProvider.AddService<IStatsManager>(new StatsManager(_inventory));
            ServiceProvider.AddService<IQuestsManager>(new QuestsManager());
        }

        private IEnumerator Startup()
        {
            yield return SceneManager.LoadSceneAsync("UI", LoadSceneMode.Additive);
            yield return _sceneLoader.LoadScene("Countryside");
        }
    }
}
