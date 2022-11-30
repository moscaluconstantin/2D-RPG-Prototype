using Assets._2D_RPG_Prototype.Code.Infrastructure;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code
{
    public class SceneTransitionPoint : MonoBehaviour
    {
        [SerializeField] private string _key;
        [SerializeField] private string _targetSceneName;

        [Header("Components")]
        [SerializeField] private Transform _entryPoint;
        [SerializeField] private PlayerLoader _playerLoader;

        private void Start()
        {
            if (ServiceProvider.SaveLoadService.PlayerData.TransitionPointKey != _key)
                return;

            _playerLoader.Player.transform.position = _entryPoint.position;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                ServiceProvider.SaveLoadService.PlayerData.TransitionPointKey = _key;
                ServiceProvider.SceneLoader.LoadScene(_targetSceneName);
            }
        }
    }
}
