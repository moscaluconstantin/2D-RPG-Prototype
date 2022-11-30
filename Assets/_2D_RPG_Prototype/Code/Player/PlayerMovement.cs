using Assets._2D_RPG_Prototype.Code;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector2 _boundsOffset;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PlayerAnimator _animator;

    private const float MIN_INPUT = .1f;

    private Vector2 input;
    private Vector3 _boundsMin;
    private Vector3 _boundsMax;

    private void Update()
    {
        input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _animator.SetMovementState(input.magnitude > MIN_INPUT);
        _animator.SetMovementValue(input);
    }

    private void FixedUpdate()
    {
        var movement = (Vector3)(input.normalized * (_movementSpeed * Time.fixedDeltaTime));
        var to = GetClampedPosition(movement);
        _rigidBody.MovePosition(to);
    }

    public void SetBound(Tilemap tilemap)
    {
        _boundsMin = tilemap.localBounds.min + (Vector3)_boundsOffset;
        _boundsMax = tilemap.localBounds.max - (Vector3)_boundsOffset;
    }

    private Vector3 GetClampedPosition(Vector3 movement)
    {
        var nextPosition = transform.position + movement;

        return new()
        {
            x = Mathf.Clamp(nextPosition.x, _boundsMin.x, _boundsMax.x),
            y = Mathf.Clamp(nextPosition.y, _boundsMin.y, _boundsMax.y),
            z = nextPosition.z
        };
    }
}
