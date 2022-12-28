using Assets._2D_RPG_Prototype.Code;
using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Vector2 _boundsOffset;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PlayerAnimator _animator;

    private bool CanMove => _movementState && !_uiService.AnyWindowActive;

    private const float MIN_INPUT = .1f;

    private IUIService _uiService;
    private bool _movementState = true;
    private Vector2 _input;
    private Vector3 _boundsMin;
    private Vector3 _boundsMax;

    private void Awake() =>
        _uiService = ServiceProvider.GetService<IUIService>();

    private void Update()
    {
        if (!CanMove)
        {
            if (_input.sqrMagnitude > MIN_INPUT)
            {
                _input = Vector2.zero;
                UpdateAnimatorValues();
            }

            return;
        }

        _input = new(Input.GetAxisRaw(InputConstants.AXES_HORIZONTAL), Input.GetAxisRaw(InputConstants.AXES_VERTICAL));

        UpdateAnimatorValues();
    }

    private void FixedUpdate()
    {
        if (!CanMove)
            return;

        var movement = (Vector3)(_input.normalized * (_movementSpeed * Time.fixedDeltaTime));
        var to = GetClampedPosition(movement);
        _rigidBody.MovePosition(to);
    }

    public void SetBound(Tilemap tilemap)
    {
        _boundsMin = tilemap.localBounds.min + (Vector3)_boundsOffset;
        _boundsMax = tilemap.localBounds.max - (Vector3)_boundsOffset;
    }

    public void SetMovementState(bool movementState)
    {
        _movementState = movementState;
        _input = Vector2.zero;

        UpdateAnimatorValues();
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

    private void UpdateAnimatorValues()
    {
        _animator.SetMovementState(_input.magnitude > MIN_INPUT);
        _animator.SetMovementValue(_input);
    }
}
