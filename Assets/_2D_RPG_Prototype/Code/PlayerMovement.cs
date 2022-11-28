using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Rigidbody2D _rigidBody;

    private Vector2 input;

    private void Update()
    {
        input = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    private void FixedUpdate()
    {
        var movement = (Vector3)(input.normalized * (_movementSpeed * Time.fixedDeltaTime));
        _rigidBody.MovePosition(transform.position + movement);
    }
}
