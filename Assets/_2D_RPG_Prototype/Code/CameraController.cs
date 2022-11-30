using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets._2D_RPG_Prototype.Code
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = .125f;

        [Header("Components")]
        [SerializeField] private Camera _camera;
        [SerializeField] private PlayerLoader _playerLoader;

        private Transform _target;
        private Vector3 _boundsMin;
        private Vector3 _boundsMax;

        private void Start()
        {
            _target = _playerLoader.Player.transform;

            SetBounds(_playerLoader.Tilemap);

            transform.position = _target.position;
        }

        private void FixedUpdate()
        {
            var lerpedPosition = Vector3.Lerp(transform.position, _target.position, _lerpSpeed);
            transform.position = new()
            {
                x = Mathf.Clamp(lerpedPosition.x, _boundsMin.x, _boundsMax.x),
                y = Mathf.Clamp(lerpedPosition.y, _boundsMin.y, _boundsMax.y),
                z = lerpedPosition.z
            };
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
    }
}
