using Assets._2D_RPG_Prototype.Code;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    [Header("Components")]
    [SerializeField] private Rigidbody2D _rigidBody;
    [SerializeField] private PlayerAnimator _animator;

    private const float MIN_INPUT = .1f;

    private Vector2 input;

    private void Update()
    {
        input = new(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        _animator.SetMovementState(input.magnitude > MIN_INPUT);
        _animator.SetMovementValue(input);
    }

    private void FixedUpdate()
    {
        var movement = (Vector3)(input.normalized * (_movementSpeed * Time.fixedDeltaTime));
        _rigidBody.MovePosition(transform.position + movement);
    }
}
