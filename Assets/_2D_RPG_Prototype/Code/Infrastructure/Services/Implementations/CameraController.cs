using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets._2D_RPG_Prototype.Code
{
    public class CameraController : MonoBehaviour, ICameraController
    {
        [SerializeField] private float _lerpSpeed = .125f;

        [Header("Components")]
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerLoader _playerLoader;

        private Transform _target;
        private Vector3 _boundsMin;
        private Vector3 _boundsMax;

        private void Awake() =>
            ServiceProvider.AddService<ICameraController>(this);

        private void Start()
        {
            SetBounds(_playerLoader.Tilemap);
            Follow(_playerLoader.Player.transform);
        }

        private void FixedUpdate()
        {
            var lerpedPosition = Vector3.Lerp(transform.position, _target.position, _lerpSpeed);
            transform.position = Clamp(lerpedPosition);
        }

        public void Follow(Transform target)
        {
            _target = target;
            transform.position = Clamp(_target.position);
        }

        private void SetBounds(Tilemap tilemap)
        {
            var offset = new Vector3()
            {
                x = _camera.orthographicSize * _camera.aspect,
                y = _camera.orthographicSize,
                z = 0
            };

            _boundsMin = tilemap.localBounds.min + offset;
            _boundsMax = tilemap.localBounds.max - offset;
        }

        private Vector3 Clamp(Vector3 position) => new()
        {
            x = Mathf.Clamp(position.x, _boundsMin.x, _boundsMax.x),
            y = Mathf.Clamp(position.y, _boundsMin.y, _boundsMax.y),
            z = position.z
        };
    }
}
