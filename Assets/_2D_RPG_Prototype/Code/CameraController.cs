using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float _lerpSpeed = .125f;
        [SerializeField] private PlayerLoader _playerLoader;

        private Transform _target;

        private void Start()
        {
            _target = _playerLoader.Player.transform;
            transform.position = _target.position;
        }

        private void FixedUpdate() =>
            transform.position = Vector3.Lerp(transform.position, _target.position, _lerpSpeed);
    }
}
