using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerMovementHandler : MonoBehaviour
    {
        [Header("Hover Settings (Spring)")]
        [SerializeField] private float targetHeight = 1.6f;
        [SerializeField] private float springStiffness = 100f;
        [SerializeField] private float springDamping = 1f;
        [Space]
        [SerializeField] private LayerMask groundLayer;

        [Header("Jump Settings")]
        [SerializeField] private float jumpForce = 6f;
        [SerializeField] private float jumpCooldown = 1f;
        [SerializeField] private float fallMultiplier = 25f;

        [Header("Ground Movement Settings")]
        [SerializeField] private float moveSpeed = 50f;
        [SerializeField] private float maxSpeed = 15f;
        [SerializeField] private float drag = 10f;
        [SerializeField] private float rotationSensitivity = 2f;

        [Header("Air Control Settings")]
        [SerializeField] private float airMoveSpeed = 15f;
        [SerializeField] private float airMaxSpeed = 10f;
        [SerializeField] private float airDrag = 2.5f;
        [SerializeField] private float airRotationSensitivity = 1f;

        public bool IsGrounded { get; private set; }
        public bool CanJump => Time.time >= _nextJumpTime;

        private float _nextJumpTime;

        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.useGravity = true;
            _rb.constraints = RigidbodyConstraints.FreezeRotation;
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics.Raycast(transform.position, Vector3.down, out _, targetHeight + 0.2f, groundLayer);

            _rb.linearDamping = IsGrounded ? drag : airDrag;
        }

        public void ApplySpringHover()
        {
            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, targetHeight + 2f, groundLayer))
            {
                float compression = targetHeight - hit.distance;

                if (compression > 0)
                {
                    float springForce = compression * springStiffness - _rb.linearVelocity.y * springDamping;

                    _rb.AddForce(Vector3.up * springForce, ForceMode.Acceleration);
                }
            }
        }

        public void Move(Vector3 direction)
        {
            if (direction.sqrMagnitude < 0.01f) return;

            float currentMoveSpeed = IsGrounded ? moveSpeed : airMoveSpeed;
            float currentMaxSpeed = IsGrounded ? maxSpeed : airMaxSpeed;

            Vector3 vel = _rb.linearVelocity;
            Vector3 horizontalVel = new Vector3(vel.x, 0, vel.z);

            float speedInInputDirection = Vector3.Dot(horizontalVel, direction.normalized);

            if (speedInInputDirection < currentMaxSpeed)
                _rb.AddForce(direction * currentMoveSpeed, ForceMode.Acceleration);

            if (IsGrounded)
            {
                vel = _rb.linearVelocity;
                horizontalVel = new Vector3(vel.x, 0, vel.z);

                if (horizontalVel.magnitude > maxSpeed)
                {
                    horizontalVel = horizontalVel.normalized * maxSpeed;
                    _rb.linearVelocity = new Vector3(horizontalVel.x, vel.y, horizontalVel.z);
                }
            }
        }

        public void RotateBody(float mouseX)
        {
            float currentSensitivity = IsGrounded ? rotationSensitivity : airRotationSensitivity;

            transform.Rotate(Vector3.up * (mouseX * currentSensitivity));
        }

        public void Jump()
        {
            _nextJumpTime = Time.time + jumpCooldown;

            if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, targetHeight + 1.5f, groundLayer))
                transform.position = hit.point + Vector3.up * (targetHeight + 0.21f);

            Vector3 vel = _rb.linearVelocity;

            _rb.linearVelocity = new Vector3(vel.x, 0, vel.z);
            _rb.linearDamping = airDrag;
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }


        public void ApplyDescentGravity()
        {
            bool inCompressionZone = false;

            if (Physics.Raycast(transform.position, Vector3.down, out _, targetHeight, groundLayer))
                inCompressionZone = true;

            if (_rb.linearVelocity.y < 0 && !inCompressionZone)
                _rb.AddForce(Vector3.down * fallMultiplier, ForceMode.Acceleration);
        }
    }
}