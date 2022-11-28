using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code
{
    public class PlayerAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private readonly int IS_MOVING = Animator.StringToHash("isMoving");
        private readonly int MOVE_X = Animator.StringToHash("moveX");
        private readonly int MOVE_Y = Animator.StringToHash("moveY");
        private readonly int IDLE_X = Animator.StringToHash("idleX");
        private readonly int IDLE_Y = Animator.StringToHash("idleY");

        private bool _isMoving;

        public void SetMovementState(bool isMoving)
        {
            _isMoving = isMoving;
            _animator.SetBool(IS_MOVING, isMoving);
        }

        public void SetMovementValue(Vector2 movementValue)
        {
            _animator.SetFloat(MOVE_X, movementValue.x);
            _animator.SetFloat(MOVE_Y, movementValue.y);

            if (_isMoving)
            {
                _animator.SetFloat(IDLE_X, movementValue.x);
                _animator.SetFloat(IDLE_Y, movementValue.y);
            }
        }
    }
}