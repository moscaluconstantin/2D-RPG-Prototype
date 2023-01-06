using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using UnityEditor;
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

        [Header("Gizmos")]
        [SerializeField] private Color _gizmosColor;
        [SerializeField] private Vector2 _gizmosBoxSize;
        [SerializeField] private Color _gizmosEntryPointColor;
        [SerializeField] private float _gizmosEntryPointRadius;

        private ISaveLoadService _saveLoad;
        private ISceneLoader _sceneLoader;
        private ICameraController _cameraController;

        private void Start()
        {
            _saveLoad = ServiceProvider.GetService<ISaveLoadService>();
            _sceneLoader = ServiceProvider.GetService<ISceneLoader>();
            _cameraController = ServiceProvider.GetService<ICameraController>();
            TryPlacePlayer();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent<PlayerMovement>(out var playerMovement))
            {
                playerMovement.SetMovementState(false);

                _saveLoad.PlayerData.SceneName = _targetSceneName;
                _saveLoad.PlayerData.TransitionPointKey = _key;
                _sceneLoader.LoadScene(_targetSceneName);
            }
        }

        private void TryPlacePlayer()
        {
            if (_saveLoad.PlayerData.TransitionPointKey != _key)
                return;

            _playerLoader.Player.transform.position = _entryPoint.position;
            _cameraController.Follow(_playerLoader.Player.transform);
        }

        private void OnDrawGizmos()
        {
#if UNITY_EDITOR
            Handles.DrawSolidRectangleWithOutline(new Rect((Vector2)transform.position - _gizmosBoxSize / 2, _gizmosBoxSize), _gizmosColor, _gizmosColor);

            Handles.color = _gizmosEntryPointColor;
            Handles.DrawSolidDisc(_entryPoint.position, Vector3.forward, _gizmosEntryPointRadius);
#endif
        }
    }
}
